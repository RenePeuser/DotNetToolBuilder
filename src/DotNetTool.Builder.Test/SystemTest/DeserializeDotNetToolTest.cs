using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetTool.Builder.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.SystemTest
{
    [TestClass]
    public class DeserializeDotNetToolTest
    {
        private DirectoryInfo _createdDirectory;
        private FileInfo _serializedDotNetTool;
        private DirectoryInfo _toolSerializeResult;
        private DirectoryInfo _generatedTools;
        private DirectoryInfo _currentDirectory;

        [TestInitialize]
        public void Init()
        {
            _currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            _generatedTools = new DirectoryInfo(Path.Combine(_currentDirectory.FullName, "GeneratedTools"));
            _serializedDotNetTool = new FileInfo(Path.Combine(_generatedTools.FullName, "my.test.json"));
            _createdDirectory = new DirectoryInfo(Path.Combine(_currentDirectory.FullName, "my.test"));
            _toolSerializeResult = new DirectoryInfo(Path.Combine(_currentDirectory.FullName, "saved-tools"));
        }

        [TestMethod]
        public async Task Should_Create_A_DotNetTool_When_Used_Save_Tool_Option()
        {
            await Program.Main(new[] { "--from-file", _serializedDotNetTool.FullName, "--save-to", _toolSerializeResult.FullName }).ConfigureAwait(false);
            var savedDotNetTool = new FileInfo(Path.Combine(_toolSerializeResult.FullName, "my.test.json"));

            Assert.IsTrue(savedDotNetTool.Exists, $"Expected saved tool: '{savedDotNetTool.FullName}' was not created");
        }

        [TestMethod]
        public async Task Should_Create_All_Possible_Test_Commands()
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
                var result = await Program.Main(new[] { "--from-file", tool.FullName });

                if (result.NotEqualsTo(0))
                {
                     yield return tool;
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_createdDirectory.Exists)
            {
                _createdDirectory.Delete(true);
            }

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
