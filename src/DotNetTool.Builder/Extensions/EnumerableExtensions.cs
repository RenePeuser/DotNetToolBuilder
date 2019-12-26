using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Argument.Check;


namespace DotNetTool.Builder.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> FilterNullObjects<T>(this IEnumerable<T> source)
            where T : class
        {
            Throw.IfNull(() => source);

            return source.Where(item => item != null);
        }

        public static TSource FirstOfType<TSource>(this IEnumerable source)
        {
            Throw.IfNull(() => source);

            var result = source.FirstOrDefaultOfType<TSource>();

            if (result == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Enumeration does not contains any item of the specific type: {0}",
                        typeof(TSource).Name));
            }

            return result;
        }

        public static bool AnyOfType<TSource>(this IEnumerable source)
            where TSource : class
        {
            Throw.IfNull(() => source);

            var result = source.FirstOrDefaultOfType<TSource>();

            return result.IsNotNull();
        }

        public static TSource FirstOrDefaultOfType<TSource>(this IEnumerable source)
        {
            Throw.IfNull(() => source);

            return source.OfType<TSource>().FirstOrDefault();
        }

        public static TSource FirstOfType<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => predicate);

            var result = source.FirstOrDefaultOfType(predicate);

            if (result == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Enumeration does not contains any item of the specific type: {0}",
                        typeof(TSource).Name));
            }

            return result;
        }

        public static TSource FirstOrDefaultOfType<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => predicate);

            return source.OfType<TSource>().FirstOrDefault(predicate);
        }

        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => action);

            var sourceList = source.ToList();
            sourceList.ForEach(action);

            return sourceList;
        }

        public static void ForEachIndex<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => action);

            var index = 0;
            foreach (var item in source)
            {
                action(item, index++);
            }
        }

        public static IEnumerable<TSource> ForEachOfType<TSource>(this IEnumerable source, Action<TSource> action)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => action);

            var sourceList = source.OfType<TSource>().ToList();
            sourceList.ForEach(action);

            return sourceList;
        }

        public static bool NotSequenceEqualsTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return !first.SequenceEqualsTo(second);
        }

        public static bool SequenceEqualsTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if (first == null || second == null)
            {
                return false;
            }

            return first.SequenceEqual(second);
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This is for comfort usage")]
        public static bool SequenceEqualsOfType<TSource>(this IEnumerable first, IEnumerable second)
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);

            return first.OfType<TSource>().SequenceEqual(second.OfType<TSource>());
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            Throw.IfNull(() => source);

            return source.ToList().AsReadOnly();
        }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "All as expected.")]
        public static List<TSource> ToListOfType<TSource>(this IEnumerable source)
        {
            Throw.IfNull(() => source);

            return source.OfType<TSource>().ToList();
        }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "All as expected.")]
        public static ImmutableList<TSource> ToImmutableOfType<TSource>(this IEnumerable source)
        {
            Throw.IfNull(() => source);

            return source.ToListOfType<TSource>().ToImmutableList();
        }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "All as expected.")]
        public static List<TSource> ToListOfTypeOrEmpty<TSource>(this IEnumerable source)
        {
            if (source == null)
            {
                return new List<TSource>();
            }

            return source.OfType<TSource>().ToList();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumeration)
        {
            Throw.IfNull(() => enumeration);

            return new ObservableCollection<T>(enumeration);
        }

        public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> enumeration)
        {
            Throw.IfNull(() => enumeration);

            return enumeration.ToObservableCollection().ToReadOnlyObservableCollection();
        }

        public static IEnumerable<TSource> WhereOfType<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => predicate);

            return source.OfType<TSource>().Where(predicate);
        }

        public static bool IsAnyItemNull<T>(this IEnumerable<T> source)
        {
            Throw.IfNull(() => source);

            return source.IsAnyItem(item => item == null);
        }

        public static bool IsAnyItemNullOrWhitespace(this IEnumerable<string> source)
        {
            Throw.IfNull(() => source);

            return source.IsAnyItem(item => item.IsNullOrWhiteSpace());
        }

        public static bool IsAnyItem<T>(this IEnumerable<T> source, Predicate<T> check)
        {
            Throw.IfNull(() => source);

            return source.Any(item => check(item));
        }

        public static bool IsAnyItemNull(params object[] source)
        {
            Throw.IfNull(() => source);

            return source.IsNotAnyItemNull();
        }

        public static bool IsNotAnyItemNull<T>(this IEnumerable<T> source)
        {
            Throw.IfNull(() => source);

            return !source.IsAnyItemNull();
        }

        public static IEnumerable<T> Dispose<T>(this IEnumerable<T> source)
            where T : IDisposable
        {
            Throw.IfNull(() => source);

            source.ToList().ForEach(item => item.Dispose());

            return source;
        }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "All as expected.")]
        public static List<T> Remove<T>(this IEnumerable<T> source, params T[] itemsToRemove)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => itemsToRemove);

            var items = source.ToList();
            items.RemoveRange(itemsToRemove);

            return items;
        }

        public static bool Remove<T>(this IList<T> source, T item, IEqualityComparer<T> equalityComparer)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => equalityComparer);

            if (source.Contains(item, equalityComparer))
            {
                return source.Remove(source.First(i => equalityComparer.Equals(item, i)));
            }

            return false;
        }

        public static bool ContainsAll<T>(this IEnumerable<T> source, params T[] expectedItems)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => expectedItems);

            return source.ContainsAll(expectedItems.ToList());
        }

        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> expectedItems)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => expectedItems);

            return expectedItems.All(source.Contains);
        }

        public static bool ContainsAll<T1, T2, TProperty>(
            this IEnumerable<T1> source,
            IEnumerable<T2> expectedItems,
            Func<T1, TProperty> funcSelector1,
            Func<T2, TProperty> funcSelector2)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => expectedItems);
            Throw.IfNull(() => funcSelector1);
            Throw.IfNull(() => funcSelector2);

            return source.ContainsAllInternal(expectedItems, funcSelector1, funcSelector2);
        }

        public static bool ContainsAllInternal<T1, T2, TProperty>(
            this IEnumerable<T1> source,
            IEnumerable<T2> expectedItems,
            Func<T1, TProperty> funcSelector1,
            Func<T2, TProperty> funcSelector2)
        {
            var list1 = source.ToList();
            var list2 = expectedItems.ToList();

            var itemsToCompare1 = list1.Select(funcSelector1);
            var itemsToCompare2 = list2.Select(funcSelector2);

            return itemsToCompare1.ContainsAll(itemsToCompare2);
        }

        public static bool ContainsNotAll<T>(this IEnumerable<T> source, IEnumerable<T> expectedItems)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => expectedItems);

            return !source.ContainsAll(expectedItems);
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, params T[] expectedItems)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => expectedItems);

            return expectedItems.Any(source.Contains);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            Throw.IfNull(() => source);

            return !source.Any();
        }

        public static IEnumerable<T> Except<T, TProperty>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TProperty> selector)
            where T : class
        {
            return first.Except(second, selector, selector);
        }

        public static IEnumerable<T> Intersect<T, TProperty>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TProperty> selector)
            where T : class
        {
            return first.Intersect(second, selector, selector);
        }

        public static IEnumerable<T1> Except<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);
            Throw.IfNull(() => selector1);
            Throw.IfNull(() => selector2);

            return first.InternalExcept(second, selector1, selector2);
        }

        public static IEnumerable<T1> Intersect<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);
            Throw.IfNull(() => selector1);
            Throw.IfNull(() => selector2);

            return first.InternalIntersect(second, selector1, selector2);
        }

        public static bool SequenceEqualsTo<T, TProperty>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TProperty> selector)
            where T : class
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);
            Throw.IfNull(() => selector);

            return first.SequenceEqualsTo(second, selector, selector);
        }

        public static bool SequenceEqualsTo<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
            where T1 : class where T2 : class
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);
            Throw.IfNull(() => selector1);
            Throw.IfNull(() => selector2);

            var valueOfFirst = first.Select(selector1).ToList();
            var valueOfSecond = second.Select(selector2).ToList();

            return valueOfFirst.SequenceEqual(valueOfSecond);
        }

        public static IEnumerable<T> MergeItemsWithoutRemove<T, TProperty>(
            this IEnumerable<T> first,
            IEnumerable<T> second,
            Func<T, TProperty> selector)
            where T : class
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);
            Throw.IfNull(() => selector);

            var targetItems = first.ToList();
            var sourceItems = second.ToList();
            var itemsToAdd = sourceItems.Except(targetItems, selector).ToList();

            foreach (var item in itemsToAdd)
            {
                var index = sourceItems.IndexOf(item);
                targetItems.Insert(index, item);
            }

            return targetItems;
        }

        public static string ToString<T>(this IEnumerable<T> source, string title, params Expression<Func<T, object>>[] infoSelector)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => title);
            Throw.IfNull(() => infoSelector);

            var compiledExpressions = infoSelector.ToCompiledExpressionWithInfo();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(title);
            source.ForEach(item => stringBuilder.AppendLine(item.ToString(compiledExpressions)));

            return stringBuilder.ToString();
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => predicate);

            return !source.Any(predicate);
        }

        public static bool HasAny<T>(this IEnumerable<T> source, params object[] expectedValues)
        {
            Throw.IfNull(() => source);

            return source.ToArray().HasAny(expectedValues);
        }

        public static IEnumerable<T> Distinct<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> selector)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => selector);

            var dictionary = new Dictionary<TProperty, T>();

            foreach (var item in source)
            {
                var value = selector(item);

                if (!dictionary.ContainsKey(value))
                {
                    dictionary.Add(value, item);
                }
            }

            return dictionary.Values;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T itemToConcat)
        {
            Throw.IfNull(() => source);

            foreach (var item in source)
            {
                yield return item;
            }

            yield return itemToConcat;
        }

        public static bool HasSameItems<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            Throw.IfNull(() => list1);
            Throw.IfNull(() => list2);

            var listCopy1 = list1.ToList();
            var listCopy2 = list2.ToList();

            var result = listCopy1.ContainsAll(listCopy2);
            var sameCount = listCopy1.Count == listCopy2.Count;

            return result && sameCount;
        }

        public static bool HasSameItems<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
        {
            Throw.IfNull(() => first);
            Throw.IfNull(() => second);
            Throw.IfNull(() => selector1);
            Throw.IfNull(() => selector2);

            return first.InternalHasSameItems(second, selector1, selector2);
        }

        public static bool InternalHasSameItems<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
        {
            var list1 = first.ToList();
            var list2 = second.ToList();

            var infoToCompareList1 = list1.Select(selector1);
            var infoToCompareList2 = list2.Select(selector2);

            return infoToCompareList1.HasSameItems(infoToCompareList2);
        }

        public static bool HasNotSameItems<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            Throw.IfNull(() => list1);
            Throw.IfNull(() => list2);

            return !list1.HasSameItems(list2);
        }

        public static string Flatten(this IEnumerable<string> strings)
        {
            Throw.IfNull(() => strings);

            return strings.Flatten(string.Empty);
        }

        public static string Flatten(this IEnumerable<string> strings, string separator)
        {
            Throw.IfNull(() => strings);
            Throw.IfNull(() => separator);

            return string.Join(separator, strings);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            Throw.IfNull(() => source);

            return source.SelectMany(s => s);
        }

        public static bool HasOneThatEquals<T>(this IEnumerable<T> master, IEnumerable<T> slave, params Func<T, object>[] selectors)
            where T : class
        {
            Throw.IfNull(() => master);
            Throw.IfNull(() => slave);
            Throw.IfNull(() => selectors);

            using (var masterEnumerator = master.GetEnumerator())
            using (var slaveEnumerator = slave.GetEnumerator())
            {
                while (masterEnumerator.MoveNext())
                {
                    var allValues = masterEnumerator.GetSelectorResults(selectors).ToList();

                    while (slaveEnumerator.MoveNext())
                    {
                        var slaveValues = slaveEnumerator.GetSelectorResults(selectors);

                        if (allValues.SequenceEqual(slaveValues))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source.IsNull())
            {
                return true;
            }

            return source.OfType<object>().IsEmpty();
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item)
        {
            Throw.IfNull(() => source);

            return source.ToList().IndexOf(item);
        }

        public static string ToString<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groups, Expression<Func<TValue, object>> groupKey, params Expression<Func<TValue, object>>[] infoSelector)
        {
            var stringBuilder = new StringBuilder();
            foreach (var group in groups)
            {
                stringBuilder.AppendLine($"{groupKey.NameOf()}: {group.Key}");
                foreach (var value in group)
                {
                    foreach (var expression in infoSelector)
                    {
                        stringBuilder.AppendLine($"-> {expression.NameOf()} : {expression.Compile().Invoke(value)}");
                    }
                }
            }

            return stringBuilder.ToString();
        }

        internal static StringCollection ToStringCollection<T>(this IEnumerable<T> source)
        {
            Throw.IfNull(() => source);

            var stringCollection = new StringCollection();
            source.ForEach(item => stringCollection.Add(item.ToString()));

            return stringCollection;
        }

        private static IEnumerable<T1> InternalExcept<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
        {
            var firstItems = first.ToList();
            var secondItems = second.ToList();

            foreach (var item in firstItems)
            {
                var result = secondItems.FirstOrDefault(i => selector2(i).EqualsTo(selector1(item)));

                if (result == null)
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<T1> InternalIntersect<T1, T2, TProperty>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, TProperty> selector1,
            Func<T2, TProperty> selector2)
        {
            var firstItems = first.ToList();
            var secondItems = second.ToList();

            foreach (var item in firstItems)
            {
                var result = secondItems.FirstOrDefault(i => selector2(i).EqualsTo(selector1(item)));

                if (result != null)
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<object> GetSelectorResults<T>(this IEnumerator<T> enumerator, params Func<T, object>[] selectors)
            where T : class
        {
            return selectors.Select(func => func(enumerator.Current));
        }
    }
}
