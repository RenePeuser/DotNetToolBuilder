using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DotNetTool.Builder.ArgumentChecking
{
    internal static class FuncFieldInfoExtractor
    {
        [DebuggerHidden]
        internal static FieldInfo GetFieldInfo<T>(this Func<T> func, Func<object, bool> predicate)
        {
            Throw.IfNull(() => func);
            Throw.IfNull(() => predicate);

            var fields = func.Target.GetType().GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var fieldInfo = fields.FirstOrDefault(f => predicate(f.GetValue(func.Target)));
            if (fieldInfo == null)
            {
                throw new ArgumentException();
            }

            return fieldInfo;
        }


        [DebuggerHidden]
        internal static string GetParameterName<T>(this Func<T> func, Func<object, bool> predicate)
        {
            Throw.IfNull(() => func);
            Throw.IfNull(() => predicate);

            var result = func.GetFieldInfo(predicate);
            return result.Name;
        }
    }
}
