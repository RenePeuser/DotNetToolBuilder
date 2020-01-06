using System.Collections.Generic;

namespace DotNetTool.Builder.Extensions
{
    internal static class GenericTypeExtensions
    {
        internal static bool EqualsTo<T>(this T source, T target)
        {
            return EqualityComparer<T>.Default.Equals(source, target);
        }

        internal static bool NotEqualsTo<T>(this T source, T target)
        {
            return !source.EqualsTo(target);
        }

        internal static IList<T> ToIList<T>(this T item)
        {
            return new List<T> { item };
        }
    }
}
