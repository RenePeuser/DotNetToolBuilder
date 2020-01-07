using System.IO;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal class FileInfoOptimizer : ITypeNameOptimizer
    {
        private static readonly string FileInfoFullQualifiedName = "System.IO.FileInfo";

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

        public bool OptimizerFor(string typeName)
        {
            return typeName.Contains("file");
        }
    }
}