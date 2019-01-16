using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LoFuUnit
{
    public abstract class LoFuTest
    {
        protected void Assert()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            Assert(this, method);
        }

        protected async Task AssertAsync()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(5).GetMethod();

            await AssertAsync(this, method).ConfigureAwait(false);
        }

        internal void Assert(object fixture, MethodBase method)
        {
            var type = method.ReflectedType;

            var methodName = $"<{method.Name}>";

            var localFunctions = type?
                                     .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                     .Where(x => x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                                     .Where(x => x.ReturnType == typeof(void))
                                     .Where(x => x.GetParameters().Length == 0)
                                     .Where(x => x.Name.StartsWith(methodName))
                                     .ToList() ?? Enumerable.Empty<MethodInfo>().ToList();

            Log(method.Name.ToFormat());

            foreach (var localFunction in localFunctions)
            {
                Log("\t" + localFunction.Name.ToFormat(methodName));
                localFunction.Invoke(fixture, new object[0]);
            }
        }

        internal async Task AssertAsync(object fixture, MethodBase method)
        {
            var type = method.ReflectedType;

            var methodName = $"<{method.Name}>";

            var localFunctions = type?
                                     .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                     .Where(x => x.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                                     .Where(x => x.ReturnType == typeof(void) || x.ReturnType == typeof(Task))
                                     .Where(x => x.GetParameters().Length == 0)
                                     .Where(x => x.Name.StartsWith(methodName))
                                     .ToList() ?? Enumerable.Empty<MethodInfo>().ToList();

            Log(method.Name.ToFormat());

            foreach (var localFunction in localFunctions)
            {
                Log("\t" + localFunction.Name.ToFormat(methodName));

                if (IsAsync(localFunction))
                {
                    var task = localFunction.Invoke(fixture, new object[0]) as Task;

                    if (task == null) throw new InvalidOperationException("Invocation of async local function failed.");

                    await task.ConfigureAwait(false);
                }
                else
                {
                    localFunction.Invoke(fixture, new object[0]);
                }
            }

            bool IsAsync(MethodInfo localFunction)
            {
                return localFunction.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
            }
        }

        protected virtual void Log(string message) => Console.WriteLine(message);
    }
}