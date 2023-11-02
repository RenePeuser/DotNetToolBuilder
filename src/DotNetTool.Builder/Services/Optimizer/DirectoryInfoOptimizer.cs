namespace DotNetTool.Builder.Services.Optimizer
{
    internal sealed class DirectoryInfoOptimizer : ITypeNameOptimizer
    {
        private static readonly string FileInfoFullQualifiedName = "System.IO.DirectoryInfo";

        public string Optimize(string typeName)
        {
            var lowerTypeName = typeName.ToLower();
            switch (lowerTypeName)
            {
                case "directory":
                case "directoryinfo":
                case "system.io.directory":
                case "system.io.directoryinfo":
                    return FileInfoFullQualifiedName;
            }

            return typeName;
        }
    }
}
