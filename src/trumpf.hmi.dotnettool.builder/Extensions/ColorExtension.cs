using System.Drawing;
using Pastel;

namespace trumpf.hmi.dotnettool.builder.Extensions
{
    public static class ColorExtension
    {
        public static string AsInput(this string source)
        {
            return source.Pastel(Color.DarkCyan);
        }

        public static string AsSample(this string source)
        {
            return source.Pastel(Color.Gray);
        }

        public static string AsError(this string source)
        {
            return source.Pastel(Color.Red);
        }

        public static string AsSuccessfull(this string source)
        {
            return source.Pastel(Color.LawnGreen);
        }
    }
}