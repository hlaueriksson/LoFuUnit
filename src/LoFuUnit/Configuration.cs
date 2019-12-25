namespace LoFuUnit
{
    internal static class Configuration
    {
        public static int StackTraceFrameIndexForAssert()
        {
            return 1;
        }

        public static int StackTraceFrameIndexForAssertAsync()
        {
#if NET461
            return 5;
#else
            return 7;
#endif
        }
    }
}