using System;
using Argument.Check;


namespace DotNetTool.Builder.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object source)
        {
            var result = default(T);

            if (source is T)
            {
                result = (T) source;
            }

            return result;
        }

        public static T Cast<T>(this object source)
        {
            Throw.IfNull(() => source);

            return (T) source;
        }

        public static bool Is<T>(this object source)
        {
            return source is T;
        }

        public static bool IsNotNull(this object source)
        {
            return !source.EqualsTo(null);
        }

        public static bool IsNull(this object source)
        {
            return source.EqualsTo(null);
        }

        public static void IfType<TType>(this object source, Action<TType> action)
            where TType : class
        {
            Throw.IfNull(() => action);

            var expectedType = source.As<TType>();

            expectedType.IfNotNullThen(() => action(expectedType));
        }
    }
}
