namespace DotNetTool.Builder.Services.Optimizer
{
    internal class FileSystemInfoOptimizer : ITypeNameOptimizer
    {
        private static readonly string FileInfoFullQualifiedName = "System.IO.FileSystemInfo";

        public string Optimize(string typeName)
        {
            var lowerTypeName = typeName.ToLower();
            switch (lowerTypeName)
            {
                case "filesystem":
                case "filesysteminfo":
                case "system.io.filesysteminfo":
                case "io.filesysteminfo":
                    return FileInfoFullQualifiedName;
            }

            return typeName;
        }

        public bool OptimizerFor(string typeName)
        {
            return typeName.Contains("filesys");
        }
    }
}
