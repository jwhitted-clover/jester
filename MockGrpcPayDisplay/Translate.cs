using System;
using System.Drawing;
using System.IO;
using Google.Protobuf;

namespace MockGrpcPayDisplay
{
    public static partial class Translate
    {
        public static bool From(bool src) => src;
        public static double From(double src) => src;
        public static int From(int src) => src;
        public static long From(long src) => src;
        public static string From(string src) => src ?? "";

        public static bool From(bool? src) => src.GetValueOrDefault();
        public static int From(int? src) => src.GetValueOrDefault();
        public static long From(long? src) => src.GetValueOrDefault();

        public static int? From<TResult>(int src)
        {
            if (typeof(TResult) == typeof(int?)) return src == 0 ? (int?)null : src;
            else throw new NotSupportedException();
        }

        public static Bitmap From(ByteString byteString)
        {
            var bytes = byteString.ToByteArray();
            var stream = new MemoryStream(bytes);
            return new Bitmap(stream);
        }
    }
}
