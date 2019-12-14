namespace trumpf.hmi.dotnettool.builder
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