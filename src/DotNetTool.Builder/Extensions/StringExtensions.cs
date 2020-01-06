using System;
using System.Linq;

namespace DotNetTool.Builder.Extensions
{
    internal static class StringExtensions
    {
        internal static bool ContainsNotAnyOf(this string source, params string[] notContainStrings)
        {
            return !notContainStrings.Any(source.Contains);
        }

        internal static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
        }

        internal static string FirstCharToLower(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToLower() + input.Substring(1)
            };
        }

        internal static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        internal static bool IsNotNullOrEmpty(this string source)
        {
            return !source.IsNullOrEmpty();
        }

        internal static bool IsEmpty(this string source)
        {
            return source == string.Empty;
        }

        internal static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        internal static bool IsNotNullOrWhiteSpace(this string source)
        {
            return source.IsNullOrWhiteSpace().IsFalse();
        }
    }
}
