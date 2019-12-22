using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DotNetTool.Builder.ArgumentChecking;

namespace DotNetTool.Builder.Extensions
{
    public static class GenericTypeExtensions
    {
        private const double DEFAULT_DOUBLE_TOLERANCE = 0.000001;

        public static bool EqualsTo<T>(this T source, T target)
        {
            return EqualityComparer<T>.Default.Equals(source, target);
        }

        public static bool EqualsTo(this double source, double target)
        {
            return EqualsTo(source, target, DEFAULT_DOUBLE_TOLERANCE);
        }

        public static bool NotEqualsTo(this double source, double target)
        {
            return NotEqualsTo(source, target, DEFAULT_DOUBLE_TOLERANCE);
        }

        public static bool EqualsTo(this double source, double target, double tolerance)
        {
            return Math.Abs(source - target).IsLessThan(tolerance);
        }

        public static bool NotEqualsTo(this double source, double target, double tolerance)
        {
            return !source.EqualsTo(target, tolerance);
        }

        public static bool NotEqualsTo<T>(this T source, T target)
        {
            return !source.EqualsTo(target);
        }

        public static bool NotEqualsToUri(this Uri source, Uri target)
        {
            var compareResult = Uri.Compare(source, target, UriComponents.AbsoluteUri, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);
            var isNotEqual = compareResult != 0;

            return isNotEqual;
        }

        public static IList<T> ToIList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static ImmutableList<T> AsImmutableList<T>(this T item)
        {
            return ImmutableList.Create(item);
        }

        public static bool EqualsAny<T>(this T source, params object[] expectedValues)
            where T : class
        {
            if (source.EqualsTo<object>(expectedValues))
            {
                return true;
            }

            if (expectedValues == null)
            {
                return false;
            }

            var result = expectedValues.Any(item => item.EqualsTo(source));

            return result;
        }

        public static T IfNotNullThen<T>(this T source, Func<T, Action> action)
            where T : class
        {
            if (source == null)
            {
                return null;
            }

            Throw.IfNull(() => action);

            action(source)();

            return source;
        }

        public static T IfNotNullThen<T>(this T source, Action action)
            where T : class
        {
            if (source == null)
            {
                return null;
            }

            Throw.IfNull(() => action);

            action();

            return source;
        }

        public static T IfNotNullThen<T>(this T source, Action<T> action)
            where T : class
        {
            if (source == null)
            {
                return null;
            }

            Throw.IfNull(() => action);

            action(source);

            return source;
        }

        public static T IfNullThen<T>(this T source, Func<T, Action> action)
            where T : class
        {
            if (source != null)
            {
                return source;
            }

            Throw.IfNull(() => action);

            action(null)();

            return null;
        }

        public static T IfNullThen<T>(this T source, Action action)
            where T : class
        {
            if (source != null)
            {
                return source;
            }

            Throw.IfNull(() => action);

            action();

            return null;
        }

        public static string ToString<T>(this T source, string title, params Expression<Func<T, object>>[] infoSelector)
            where T : class
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => title);
            Throw.IfNull(() => infoSelector);

            var compiledExpressions = infoSelector.ToCompiledExpressionWithInfo();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(title);
            stringBuilder.AppendLine(source.ToString(compiledExpressions));

            return stringBuilder.ToString();
        }

        public static string ToString<T>(this T source, params Expression<Func<T, object>>[] infoSelector)
            where T : class
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => infoSelector);

            var compiledExpressions = infoSelector.ToCompiledExpressionWithInfo();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(source.ToString(compiledExpressions));

            return stringBuilder.ToString();
        }

        public static string ToString<T>(this T source, Dictionary<string, Func<T, object>> compiledExpressions)
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull(() => compiledExpressions);

            var stringBuilder = new StringBuilder();
            compiledExpressions.ForEach(item => stringBuilder.Append(item.ToString(source)));

            return stringBuilder.ToString();
        }

        public static IEnumerable<T> Repeat<T>(this T source, int count)
        {
            Throw.IfLessThan(() => count, 0);

            return Enumerable.Repeat(source, count);
        }

        public static int ToInt<T>(this T source)
            where T : IConvertible
        {
            Throw.IfNull<object>(() => source);

            return Convert.ToInt32(source, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<T> Concat<T>(this T source, IEnumerable<T> items)
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull(() => items);

            yield return source;

            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static bool NotHasValue<T>(this T? nullable)
            where T : struct
        {
            return !nullable.HasValue;
        }
    }
}
