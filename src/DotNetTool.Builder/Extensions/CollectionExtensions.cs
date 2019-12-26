using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Argument.Check;


namespace DotNetTool.Builder.Extensions
{
    public static class CollectionExtensions
    {
        public static void ClearAndAddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => items);

            collection.Clear();
            collection.AddRange(items);
        }

        public static void ClearAndAddRange<T>(this ICollection<T> collection, params T[] items)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => items);

            collection.Clear();
            collection.AddRange(items);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => items);

            items.ToList().ForEach(collection.Add);
        }

        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => items);

            items.ForEach(collection.Add);
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => items);

            items.ToList().ForEach(item => collection.Remove(item));
        }

        public static void RemoveRange<T>(this ICollection<T> collection, Func<T, bool> selector)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => selector);

            var itemsToRemove = collection.Where(selector).ToList();
            collection.RemoveRange(itemsToRemove);
        }

        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => predicate);

            var itemsToRemove = collection.Where(predicate).ToList();
            collection.RemoveRange(itemsToRemove);
        }

        public static void AddOnce<T>(this ICollection<T> collection, T item)
            where T : class
        {
            Throw.IfNull(() => collection);
            Throw.IfNull(() => item);

            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
        }

        public static void ReplaceAt<T>(this Collection<T> collection, int index, T newItem)
        {
            Throw.IfNull(() => collection);

            collection[index] = newItem;
        }

        public static void Replace<T>(this Collection<T> sourceCollection, T oldItem, T newItem)
        {
            Throw.IfNull(() => sourceCollection);

            var index = sourceCollection.IndexOf(oldItem);
            sourceCollection.ReplaceAt(index, newItem);
        }

        public static void UnionByReplacing<T>(this ICollection<T> target, ICollection<T> source)
        {
            Throw.IfNull(() => target);
            Throw.IfNull(() => source);

            foreach (var item in source)
            {
                if (target.Contains(item))
                {
                    target.Remove(item);
                }

                target.Add(item);
            }
        }

        public static void SyncCollectionFrom<T>(this ICollection<T> target, IEnumerable<T> source)
            where T : class
        {
            Throw.IfNull(() => target);
            Throw.IfNull(() => source);

            var sourceItems = source.ToList();

            InvokeActionForExceptItems(target, sourceItems, target.Add);
            InvokeActionForExceptItems(sourceItems, target, item => target.Remove(item));
        }

        public static void SyncCollectionFrom<T, TProperty>(this ICollection<T> target, IEnumerable<T> source, Func<T, TProperty> selector)
            where T : class
        {
            Throw.IfNull(() => target);
            Throw.IfNull(() => source);
            Throw.IfNull(() => selector);

            var sourceItems = source.ToList();

            InvokeActionForExceptItems(target, sourceItems, selector, target.Add);
            InvokeActionForExceptItems(sourceItems, target, selector, item => target.Remove(item));
        }

        public static void SyncCollectionWithoutDeleteFrom<T, TProperty>(
            this ICollection<T> target,
            IEnumerable<T> source,
            Func<T, TProperty> selector)
            where T : class
        {
            Throw.IfNull(() => target);
            Throw.IfNull(() => source);
            Throw.IfNull(() => selector);

            var sourceItems = source.ToList();
            InvokeActionForExceptItems(target, sourceItems, selector, target.Add);
        }

        public static void SyncCollectionWithoutDeleteFrom<T>(this ICollection<T> target, IEnumerable<T> source)
            where T : class
        {
            Throw.IfNull(() => target);
            Throw.IfNull(() => source);

            var sourceItems = source.ToList();
            InvokeActionForExceptItems(target, sourceItems, target.Add);
        }

        private static void InvokeActionForExceptItems<T>(ICollection<T> first, IEnumerable<T> second, Action<T> action)
            where T : class
        {
            var exceptItems = second.Except(first).ToList();
            exceptItems.ForEach(action);
        }

        private static void InvokeActionForExceptItems<T, TProperty>(
            ICollection<T> first,
            IEnumerable<T> second,
            Func<T, TProperty> selector,
            Action<T> action)
            where T : class
        {
            var exceptItems = second.Except(first, selector).ToList();
            exceptItems.ForEach(action);
        }
    }
}
