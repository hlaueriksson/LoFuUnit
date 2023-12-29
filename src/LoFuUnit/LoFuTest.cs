using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Validate(testMethod);

            var testFunctions = testMethod.ReflectedType?
                                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                    .Where(x => x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                                    .Where(x => x.ReturnType == typeof(void))
                                    .Where(x => x.GetParameters().Length == 0)
                                    .Where(x => x.Name.StartsWith(testMethod.WrappedName(), StringComparison.Ordinal))
                                    .OrderBy(x => x.MetadataToken)
                                    .ToList() ?? Enumerable.Empty<MethodInfo>().ToList();

            Log(testMethod.GetFormattedName());

            foreach (var testFunction in testFunctions)
            {
                Log("\t" + testFunction.GetFormattedFunctionName(testMethod));
                testFunction.Invoke(testFixture, Array.Empty<object>());
            }
        }

        internal async Task AssertAsync(object testFixture, MethodBase testMethod)
        {
            ValidateAsync(testMethod);
            Validate(testMethod);

            var testFunctions = testMethod.ReflectedType?
                                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                    .Where(x => x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                                    .Where(x => x.ReturnType == typeof(void) || x.ReturnType == typeof(Task))
                                    .Where(x => x.GetParameters().Length == 0)
                                    .Where(x => x.Name.StartsWith(testMethod.WrappedName(), StringComparison.Ordinal))
                                    .OrderBy(x => x.MetadataToken)
                                    .ToList() ?? Enumerable.Empty<MethodInfo>().ToList();

            Log(testMethod.GetFormattedName());

            foreach (var testFunction in testFunctions)
            {
                Log("\t" + testFunction.GetFormattedFunctionName(testMethod));

                if (IsAsync(testFunction))
                {
                    var task = testFunction.Invoke(testFixture, Array.Empty<object>()) as Task;

                    if (task == null) throw new InconclusiveLoFuTestException($"Invocation of test function '{testFunction.GetFunctionName(testMethod)}' failed. The asynchronous local function does not have a valid return type. Asynchronous test functions must return a Task, and cannot return void or Task<TResult>.");

                    await task.ConfigureAwait(false);
                }
                else
                {
                    testFunction.Invoke(testFixture, Array.Empty<object>());
                }
            }

            static bool IsAsync(MethodInfo testFunction)
            {
                return testFunction.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
            }
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this method.
        /// </summary>
        protected void Assert()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(Configuration.StackTraceFrameIndexForAssert()).GetMethod();

            Assert(this, method);
        }

        /// <summary>
        /// Runs the local functions in the containing test method that invoked this method.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected async Task AssertAsync()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(Configuration.StackTraceFrameIndexForAssertAsync()).GetMethod();

            await AssertAsync(this, method).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the specified message, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <remarks>Override this method to change how the test output is written.</remarks>
        protected virtual void Log(string message) => Console.WriteLine(message);

        private static void Validate(MethodBase method)
        {
            var invalidTestFunctions = method.ReflectedType?
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(x => x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                .Where(x => x.ReturnType == typeof(void))
                .Where(x => x.GetParameters().Length > 0)
                .Where(x => x.Name.StartsWith(method.WrappedName(), StringComparison.Ordinal))
                .OrderBy(x => x.MetadataToken)
                .ToList();

            if (invalidTestFunctions == null || !invalidTestFunctions.Any()) return;

            var names = invalidTestFunctions.Select(x => x.GetFunctionName(method)).ToList();

            ThrowInconclusive(method, names);
        }

#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods
        private static void ValidateAsync(MethodBase method)
#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
        {
            var displayClasses = method.ReflectedType?
                .GetNestedTypes(BindingFlags.NonPublic)
                .Where(x => x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                .Where(x => !typeof(IAsyncStateMachine).IsAssignableFrom(x))
                .OrderBy(x => x.MetadataToken)
                .ToList();

            var invalidTestFunctions = displayClasses?
                .SelectMany(t => t
                    .GetNestedTypes(BindingFlags.NonPublic)
                    .Where(x => typeof(IAsyncStateMachine).IsAssignableFrom(x))
                    .Where(x => !x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                    .Where(x => x.Name.StartsWith("<" + method.WrappedName() + "g__", StringComparison.Ordinal)))
                .OrderBy(x => x.MetadataToken)
                .ToList();

            if (invalidTestFunctions == null || !invalidTestFunctions.Any()) return;

            var names = invalidTestFunctions.Select(x => x.GetFunctionName(method)).ToList();

            ThrowInconclusive(method, names);
        }

        private static void ThrowInconclusive(MethodBase method, IEnumerable<string> names)
        {
            throw new InconclusiveLoFuTestException($"Invocation of test method '{method.Name}' aborted. One or more test functions are inconclusive. Test functions must be parameterless, and cannot use variables declared at test method scope. Please review the following local functions: {string.Join(", ", names)}");
        }
    }
}
