namespace DotNetTool.Builder.Services.Optimizer
{
    internal sealed class FileInfoOptimizer : ITypeNameOptimizer
    {
        private static readonly string FileInfoFullQualifiedName = "FileInfo";

        public string Optimize(string typeName)
        {
            var lowerTypeName = typeName.ToLower();
            switch (lowerTypeName)
            {
                case "file":
                case "fileinfo":
                case "system.io.file":
                case "system.io.fileinfo":
                    return FileInfoFullQualifiedName;
            }

            return typeName;
        }
    }
}
