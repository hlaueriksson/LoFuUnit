namespace LoFuUnit
{
    internal static class Configuration
    {
        public static int StackTraceFrameIndexForAssert()
        {
            return 1;
        }

#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods
        public static int StackTraceFrameIndexForAssertAsync()
#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
        {
#if NET461
            return 5;
#else
            return 7;
#endif
        }
    }
}
