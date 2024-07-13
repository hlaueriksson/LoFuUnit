using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LoFuUnit
{
    /// <summary>
    /// Base class for test fixtures.
    /// </summary>
    public abstract class LoFuTest
    {
        internal void Assert(object testFixture, MethodBase testMethod)
        {
            ValidateMethod(testMethod);

            var testFunctions = testMethod.ReflectedType?
                                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                    .Where(x =>
                                        x.GetCustomAttributes<CompilerGeneratedAttribute>().Any() &&
                                        x.ReturnType == typeof(void) &&
                                        x.GetParameters().Length == 0 &&
                                        x.Name.StartsWith(testMethod.WrappedName(), StringComparison.Ordinal))
                                    .OrderBy(x => x.MetadataToken)
                                    .ToList() ?? Enumerable.Empty<MethodInfo>().ToList();

            Log(testMethod.GetFormattedName());

            foreach (var testFunction in testFunctions)
            {
                Log("\t" + testFunction.GetFormattedFunctionName(testMethod));
                testFunction.Invoke(testFixture, []);
            }
        }

        internal async Task AssertAsync(object testFixture, MethodBase testMethod)
        {
            ValidateAsyncMethod(testMethod);
            ValidateMethod(testMethod);

            var testFunctions = testMethod.ReflectedType?
                                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                    .Where(x =>
                                        x.GetCustomAttributes<CompilerGeneratedAttribute>().Any() &&
                                        (x.ReturnType == typeof(void) || x.ReturnType == typeof(Task)) &&
                                        x.GetParameters().Length == 0 &&
                                        x.Name.StartsWith(testMethod.WrappedName(), StringComparison.Ordinal))
                                    .OrderBy(x => x.MetadataToken)
                                    .ToList() ?? Enumerable.Empty<MethodInfo>().ToList();

            Log(testMethod.GetFormattedName());

            foreach (var testFunction in testFunctions)
            {
                Log("\t" + testFunction.GetFormattedFunctionName(testMethod));

                if (IsAsyncMethod(testFunction))
                {
                    if (testFunction.Invoke(testFixture, []) is not Task task) throw new InconclusiveLoFuTestException($"Invocation of test function '{testFunction.GetFunctionName(testMethod)}' failed. The asynchronous local function does not have a valid return type. Asynchronous test functions must return a Task, and cannot return void or Task<TResult>.");

                    await task.ConfigureAwait(false);
                }
                else
                {
                    testFunction.Invoke(testFixture, []);
                }
            }

            static bool IsAsyncMethod(MethodInfo testFunction)
            {
                return testFunction.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
            }
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this method.
        /// </summary>
        /// <param name="callerMemberName">The test method name. The caller of this method will implicitly be used, so don't set this parameter explicitly.</param>
        protected void Assert([CallerMemberName] string callerMemberName = "")
        {
            Assert(this, this.GetAssertTestMethod(callerMemberName));
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this method.
        /// </summary>
        /// <param name="callerMemberName">The test method name. The caller of this method will implicitly be used, so don't set this parameter explicitly.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected async Task AssertAsync([CallerMemberName] string callerMemberName = "")
        {
            await AssertAsync(this, this.GetAssertAsyncTestMethod(callerMemberName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the specified message, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <remarks>Override this method to change how the test output is written.</remarks>
        protected virtual void Log(string message) => Console.WriteLine(message);

        private static void ValidateMethod(MethodBase method)
        {
            var invalidTestFunctions = method.ReflectedType?
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(x =>
                    x.GetCustomAttributes<CompilerGeneratedAttribute>().Any() &&
                    x.ReturnType == typeof(void) &&
                    x.GetParameters().Length > 0 &&
                    x.Name.StartsWith(method.WrappedName(), StringComparison.Ordinal))
                .OrderBy(x => x.MetadataToken)
                .ToList();

            if (invalidTestFunctions?.Any() != true) return;

            var names = invalidTestFunctions.ConvertAll(x => x.GetFunctionName(method));

            ThrowInconclusive(method, names);
        }

        private static void ValidateAsyncMethod(MethodBase method)
        {
            var displayClasses = method.ReflectedType?
                .GetNestedTypes(BindingFlags.NonPublic)
                .Where(x =>
                    x.GetCustomAttributes<CompilerGeneratedAttribute>().Any() &&
                    !typeof(IAsyncStateMachine).IsAssignableFrom(x))
                .OrderBy(x => x.MetadataToken)
                .ToList();

            var invalidTestFunctions = displayClasses?
                .SelectMany(t => t
                    .GetNestedTypes(BindingFlags.NonPublic)
                    .Where(x =>
                        typeof(IAsyncStateMachine).IsAssignableFrom(x) &&
                        !x.GetCustomAttributes<CompilerGeneratedAttribute>().Any() &&
                        x.Name.StartsWith("<" + method.WrappedName() + "g__", StringComparison.Ordinal)))
                .OrderBy(x => x.MetadataToken)
                .ToList();

            if (invalidTestFunctions?.Any() != true) return;

            var names = invalidTestFunctions.ConvertAll(x => x.GetFunctionName(method));

            ThrowInconclusive(method, names);
        }

        private static void ThrowInconclusive(MethodBase method, IEnumerable<string> names)
        {
            throw new InconclusiveLoFuTestException($"Invocation of test method '{method.Name}' aborted. One or more test functions are inconclusive. Test functions must be parameterless, and cannot use variables declared at test method scope. Please review the following local functions: {string.Join(", ", names)}");
        }
    }
}
