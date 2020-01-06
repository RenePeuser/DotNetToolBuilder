using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;

namespace DotNetTool.Builder.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<string> FilterNullOrWhitespace(this IEnumerable<string> source)
        {
            Throw.IfNull(() => source);

            return source.Where(s => s.IsNotNullOrWhiteSpace());
        }

        internal static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => action);

            var sourceList = source.ToList();
            sourceList.ForEach(action);
        }

        internal static bool ContainsAny<T>(this IEnumerable<T> source, params T[] expectedItems)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => expectedItems);

            return expectedItems.Any(source.Contains);
        }

        internal static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            Throw.IfNull(() => source);

            return !source.Any();
        }


        internal static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T itemToConcat)
        {
            Throw.IfNull(() => source);

            foreach (var item in source)
            {
                yield return item;
            }

            yield return itemToConcat;
        }

        internal static string Flatten(this IEnumerable<string> strings)
        {
            Throw.IfNull(() => strings);

            return strings.Flatten(string.Empty);
        }

        internal static string Flatten(this IEnumerable<string> strings, string separator)
        {
            Throw.IfNull(() => strings);
            Throw.IfNull(() => separator);

            return string.Join(separator, strings);
        }

        internal static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source.IsNull())
            {
                return true;
            }

            return source.OfType<object>().IsEmpty();
        }
    }
}
