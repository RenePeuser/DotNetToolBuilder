using System;

namespace DotNetTool.Builder.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsNan(this double value)
        {
            return double.IsNaN(value);
        }

        public static bool IsNotNan(this double value)
        {
            return !value.IsNan();
        }

        public static double ToDouble(this double? value)
        {
            return value ?? default(double);
        }

        public static double ToValueOrDefault(this double value)
        {
            return value.IsNan() ? default(double) : value;
        }

        public static bool IsZero(this double source)
        {
            var result = source.EqualsTo(default(double));

            return result;
        }

        public static decimal ToDecimal(this double source)
        {
            return new decimal(source);
        }

        public static int Ceiling(this double value)
        {
            return (int)Math.Ceiling(value);
        }

        public static bool DoubleNotEqualsToExcludingNan(this double source, double target)
        {
            bool result;

            if (source.IsNotNan()
                && target.IsNotNan())
            {
                result = source.NotEqualsTo(target);
            }
            else
            {
                result = !source.Equals(target);
            }

            return result;
        }
    }
}