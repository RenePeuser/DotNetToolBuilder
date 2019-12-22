using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.ArgumentChecking;

namespace DotNetTool.Builder.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            var result = dictionary.GetValueOrDefaults(key, default);

            if (result.Exists.IsFalse())
            {
                throw new ArgumentException("Value for expected key does not exists.");
            }

            return result.Value;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => key);

            return dictionary.GetValueOrDefaults(key, default).Value;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => key);

            return dictionary.GetValueOrDefaults(key, defaultValue).Value;
        }

        public static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => value);

            var result = dictionary.GetKeyOrDefaultInternal(value, default);

            if (result.Exists.IsFalse())
            {
                throw new ArgumentException("Values of dictionary does not contains expected value.");
            }

            return result.Value;
        }

        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => key);

            var result = dictionary.GetValueOrDefaults(key, defaultValue);

            if (result.Exists.IsFalse())
            {
                throw new ArgumentException("Values of dictionary does not contains expected value.");
            }

            return result.Value;
        }

        public static TKey GetKeyOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value, TKey defaultValue)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => value);

            return dictionary.GetKeyOrDefaultInternal(value, defaultValue).Value;
        }

        public static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => value);

            return dictionary.Values.Contains(value);
        }

        private static DictionaryResult<TKey> GetKeyOrDefaultInternal<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TValue value,
            TKey defaultValue)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => value);

            var contains = dictionary.ContainsValue(value);
            var result = contains ? dictionary.First(item => item.Value.EqualsTo(value)).Key : defaultValue;

            return new DictionaryResult<TKey>(result, contains);
        }

        private static DictionaryResult<TValue> GetValueOrDefaults<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            Throw.IfNull(() => dictionary);
            Throw.IfNull<object>(() => key);

            TValue result;
            var exists = dictionary.TryGetValue(key, out result);

            if (!exists)
            {
                result = defaultValue;
            }

            return new DictionaryResult<TValue>(result, exists);
        }

        public class DictionaryResult<T>
        {
            internal DictionaryResult(T value, bool exists)
            {
                Value = value;
                Exists = exists;
            }

            public T Value { get; }

            public bool Exists { get; }
        }
    }
}
