using System;
using Argument.Check;

namespace DotNetTool.Builder.Extensions
{
    public static class BoolExtensions
    {
        public static bool ToBool(this bool? value)
        {
            return value.HasValue && value.Value;
        }

        public static int ToInt(this bool? value)
        {
            return value.ToBool().ToInt();
        }

        public static bool IfTrueThen(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            if (value)
            {
                action();
            }

            return value;
        }

        public static bool IfFalseThen(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            if (!value)
            {
                action();
            }

            return value;
        }

        public static bool IsFalse(this bool source)
        {
            return !source;
        }
    }
}
