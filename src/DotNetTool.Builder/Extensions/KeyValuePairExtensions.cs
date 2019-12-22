using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using DotNetTool.Builder.ArgumentChecking;

namespace DotNetTool.Builder.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Throw.IfNull(() => keyValuePairs);

            var dictionary = keyValuePairs.ToDictionary(item => item.Key, item => item.Value);

            return dictionary;
        }

        public static string ToString<T>(this KeyValuePair<string, Func<T, object>> compiledExpression, T argument)
        {
            Throw.IfNull<object>(() => argument);

            var result = string.Format(CultureInfo.InvariantCulture, "{0}[{1}] ", compiledExpression.Key, compiledExpression.Value.Invoke(argument));

            return result;
        }

        internal static OrderedDictionary ToOrderedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Throw.IfNull(() => keyValuePairs);

            var result = Create<OrderedDictionary, TKey, TValue>(keyValuePairs);

            return result;
        }

        internal static ListDictionary ToListDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Throw.IfNull(() => keyValuePairs);

            var result = Create<ListDictionary, TKey, TValue>(keyValuePairs);

            return result;
        }

        internal static HybridDictionary ToHybridDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Throw.IfNull(() => keyValuePairs);

            var result = Create<HybridDictionary, TKey, TValue>(keyValuePairs);

            return result;
        }

        internal static StringDictionary ToStringDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Throw.IfNull(() => keyValuePairs);

            var stringDictionary = new StringDictionary();
            keyValuePairs.ForEach(item => stringDictionary.Add(item.Key.ToString(), item.Value.ToString()));

            return stringDictionary;
        }

        internal static NameValueCollection ToNameValueCollection<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Throw.IfNull(() => keyValuePairs);

            var nameValueCollection = new NameValueCollection();
            keyValuePairs.ForEach(item => nameValueCollection.Add(item.Key.ToString(), item.Value.ToString()));

            return nameValueCollection;
        }

        private static TDictionary Create<TDictionary, TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
            where TDictionary : IDictionary, new()
        {
            var expectedDicitonary = new TDictionary();
            keyValuePairs.ForEach(item => expectedDicitonary.Add(item.Key.ToString(), item.Value.ToString()));

            return expectedDicitonary;
        }
    }
}