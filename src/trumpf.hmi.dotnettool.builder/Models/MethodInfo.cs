namespace trumpf.hmi.dotnettool.builder.Models
{
    public class MethodInfo
    {
        public MethodInfo(string methodName, string methodSyntax)
        {
            MethodName = methodName;
            MethodSyntax = methodSyntax;
        }

        public string MethodName { get; }

        public string MethodSyntax { get; }
    }
}