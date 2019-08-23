namespace MockGrpcPayDisplay
{
    public static partial class Translate
    {
        public static bool From(bool src) => src;
        public static double From(double src) => src;
        public static int From(int src) => src;
        public static long From(long src) => src;
        public static string From(string src) => src;

        public static bool From(bool? src) => src.GetValueOrDefault();
        public static int From(int? src) => src.GetValueOrDefault();
        public static long From(long? src) => src.GetValueOrDefault();
    }
}
