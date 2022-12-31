using System;
using System.Threading.Tasks;

namespace LoFuUnit.Tests.Integration
{
    public class FailSubject
    {
        public void Fail()
        {
            throw new Exception("Fail");
        }

        public async Task FailAsync()
        {
            await Task.Delay(1);

            throw new Exception("Fail");
        }
    }
}
