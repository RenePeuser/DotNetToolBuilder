using System;
using Argument.Check;

namespace DotNetTool.Builder.Extensions
{
    public static class BoolExtensions
    {
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

        public static bool IfTrueThen(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            if (value)
            {
                action();
            }

            return value;
        }

        public static bool If(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            return IfTrueThen(value, action);
        }

        public static bool Else(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            return IfFalseThen(value, action);
        }
    }
}
