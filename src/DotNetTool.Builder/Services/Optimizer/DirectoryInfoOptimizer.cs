namespace DotNetTool.Builder.Services.Optimizer
{
    internal class DirectoryInfoOptimizer : ITypeNameOptimizer
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

        public bool OptimizerFor(string typeName)
        {
            return typeName.Contains("directory");
        }
    }
}
