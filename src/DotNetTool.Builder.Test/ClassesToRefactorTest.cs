using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DotNetTool.Builder.Test.AssertHelper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test
{
    [TestClass]
    public class ClassesToRefactorTest
    {
        private static IEnumerable<CSharpFileInfo> _csharpFileInfos;
        private static readonly string[] CodeFilesOnWhiteList = new[] { "Startup.cs", "ArgumentValidator.cs" };

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            var argumentCheckDirectory = FindFolderWithSources(currentDirectory, "DotNetTool.Builder");
            var allCSharpFiles = argumentCheckDirectory.EnumerateFiles("*.cs", SearchOption.AllDirectories);
            _csharpFileInfos = allCSharpFiles.Select(csharpFile =>
                {
                    var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(csharpFile.FullName));
                    return new CSharpFileInfo(csharpFile, syntaxTree);
                })
                .ToList();
        }

        [TestMethod]
        public void All_Class_Should_Have_Maximum_120_Lines_Of_Code()
        {
            var errors = _csharpFileInfos.Where(csharp => csharp.SyntaxTree.GetText().Lines.Count > 120)
                .Where(csharp => CodeFilesOnWhiteList.All(toIgnore => csharp.FileInfo.Name != toIgnore))
                .Select(csharp => $"{csharp.FileInfo.FullName} - Line of codes: {csharp.SyntaxTree.GetText().Lines.Count}")
                .ToList();

            Assert.IsFalse(errors.Any(), errors.ToErrorMessage("Following C# files should be refactored:"));
        }

        private static DirectoryInfo FindFolderWithSources(DirectoryInfo startDirectoryInfo, string name)
        {
            if (startDirectoryInfo == null)
            {
                return null;
            }

            if (startDirectoryInfo.Name == name)
            {
                return startDirectoryInfo;
            }

            var directory = startDirectoryInfo.EnumerateDirectories(name).FirstOrDefault();
            if (directory != null)
            {
                return directory;
            }

            return FindFolderWithSources(startDirectoryInfo.Parent, name);
        }

        [DebuggerDisplay("{FileInfo.Name}")]
        private class CSharpFileInfo
        {
            public CSharpFileInfo(FileInfo fileInfo, SyntaxTree syntaxTree)
            {
                FileInfo = fileInfo;
                SyntaxTree = syntaxTree;
            }

            public FileInfo FileInfo { get; }

            public SyntaxTree SyntaxTree { get; }
        }
    }
}
