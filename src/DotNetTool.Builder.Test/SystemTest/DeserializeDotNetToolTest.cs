using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.SystemTest
{
    [TestClass]
    public class DeserializeDotNetToolTest
    {
        private DirectoryInfo _currentDirectory;
        private DirectoryInfo _generatedTools;
        private DirectoryInfo _toolSerializeResult;

        [TestInitialize]
        public void Init()
        {
            _currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            _generatedTools = new DirectoryInfo(Path.Combine(_currentDirectory.FullName, "GeneratedTools"));
            _toolSerializeResult = new DirectoryInfo(Path.Combine(_currentDirectory.FullName, "saved-tools"));
        }

        [TestMethod]
        public async Task Should_Create_Successfully_Solutions_From_Serialized_Test_Tools()
        {
            var allTools = _generatedTools.EnumerateFiles("*.json").ToList();
            var result = await CollectNotCreatableTools(allTools).ToListAsync();
            var fileNames = result.Select(f => f.FullName);

            Assert.IsFalse(result.Any(), AssertHelper.AssertHelper.ToErrorMessage(fileNames, "Following dotnet tools could not successfully created:"));
        }

        private async IAsyncEnumerable<FileInfo> CollectNotCreatableTools(IEnumerable<FileInfo> toolsToDeserialize)
        {
            foreach (var tool in toolsToDeserialize)
            {
                var result = await Program.Main(new[] { "--from-file", tool.FullName, "--save-to", _toolSerializeResult.FullName });
                if (result.NotEqualsTo(0))
                {
                    yield return tool;
                }

                var savedTool = _toolSerializeResult.EnumerateFiles().First(f => f.Name.ToLower().EqualsTo(tool.Name));
                if (savedTool.IsNull())
                {
                    yield return savedTool;
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_toolSerializeResult.Exists)
            {
                _toolSerializeResult.Delete(true);
            }

            var allTools = _generatedTools.EnumerateFiles("*.json");
            foreach (var tool in allTools)
            {
                var toolFolder = Path.Combine(_currentDirectory.FullName, tool.Name.Replace(".json", string.Empty));
                var createdTool = new DirectoryInfo(toolFolder);
                if (createdTool.Exists)
                {
                    createdTool.Delete(true);
                }
            }
        }
    }
}
