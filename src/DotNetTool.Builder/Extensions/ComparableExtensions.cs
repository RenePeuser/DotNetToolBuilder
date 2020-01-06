using System;
using Argument.Check;

namespace DotNetTool.Builder.Extensions
{
    internal static class ComparableExtensions
    {
        internal static bool IsEqualTo<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) == 0;
        }

        internal static bool IsLessThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) < 0;
        }
    }
}
