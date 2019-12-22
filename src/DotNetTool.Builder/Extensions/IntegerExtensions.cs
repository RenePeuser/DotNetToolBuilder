using System;
using System.Collections.Generic;
using DotNetTool.Builder.ArgumentChecking;

namespace DotNetTool.Builder.Extensions
{
    public static class IntegerExtensions
    {
        public static int ToInt(this int? value)
        {
            return value ?? default(int);
        }

        public static bool ToBool(this int value)
        {
            return value.IsGreaterThan(0);
        }

        public static double DivideBy(this int value, double divisor)
        {
            if (divisor.IsZero())
            {
                throw new ArgumentException("Division by 0 is not allowed");
            }

            if (divisor.IsNan())
            {
                throw new ArgumentException("Divisor is not a number");
            }

            return value / divisor;
        }

        public static int DivideBy(this int value, int divisor)
        {
            if (divisor.EqualsTo(0))
            {
                throw new ArgumentException("Division by 0 is not allowed");
            }

            return value / divisor;
        }

        public static int MultiplyBy(this int value, int multiplier)
        {
            return value * multiplier;
        }

        public static int Minus(this int value, int subtrahend)
        {
            return value - subtrahend;
        }

        public static int Plus(this int value, int addend)
        {
            return value + addend;
        }

        public static double ToDouble(this int value)
        {
            return value;
        }

        public static void Times(this int source, Action action)
        {
            Throw.IfNull(() => action);
            Throw.IfLessThan(() => source, 0);

            source.Times(index => action());
        }

        public static void Times(this int source, Action<int> action)
        {
            Throw.IfNull(() => action);
            Throw.IfLessThan(() => source, 0);

            source.Times(action, 0);
        }

        public static void Times(this int source, Action<int> action, int startIndex)
        {
            Throw.IfNull(() => action);

            for (var i = startIndex; i.IsLessThan(source.Plus(startIndex)); i++)
            {
                action(i);
            }
        }

        public static IEnumerable<T> Times<T>(this int source, Func<T> func)
        {
            Throw.IfNull(() => func);
            Throw.IfLessThan(() => source, 0);

            return source.Times(index => func());
        }

        public static IEnumerable<T> Times<T>(this int source, Func<int, T> func)
        {
            Throw.IfNull(() => func);
            Throw.IfLessThan(() => source, 0);

            return source.Times(func, 0);
        }

        public static IEnumerable<T> Times<T>(this int source, Func<int, T> func, int startIndex)
        {
            Throw.IfNull(() => func);

            for (var i = startIndex; i.IsLessThan(source.Plus(startIndex)); i++)
            {
                yield return func(i);
            }
        }
    }
}