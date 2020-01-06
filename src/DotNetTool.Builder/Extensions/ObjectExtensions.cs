namespace DotNetTool.Builder.Extensions
{
    internal static class ObjectExtensions
    {
        internal static T As<T>(this object source)
        {
            var result = default(T);
            return source is T castedObject ? castedObject : result;
        }

        internal static T Cast<T>(this object source) where T : class
        {
            return (T)source;
        }

        internal static bool Is<T>(this object source)
        {
            return source is T;
        }

        internal static bool IsNot<T>(this object source)
        {
            return Is<T>(source).IsFalse();
        }

        internal static bool IsNotNull(this object source)
        {
            return !source.EqualsTo(null);
        }

        internal static bool IsNull(this object source)
        {
            return source.EqualsTo(null);
        }
    }
}
