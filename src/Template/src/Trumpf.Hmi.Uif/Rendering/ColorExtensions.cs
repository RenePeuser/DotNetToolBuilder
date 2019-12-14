namespace Trumpf.Hmi.Uif.Rendering
{
    using Pastel;

    public static class ColorExtensions
    {
        public static string AsDescription(this string str) => str.Pastel(Colors.DescriptionColor);

        public static string AsError(this string str) => str.Pastel(Colors.ErrorColor);

        public static string AsSuccess(this string str) => str.Pastel(Colors.SuccessColor);
        
        public static string AsHighlight(this string str) => str.Pastel(Colors.HighlightColor);
    }
}