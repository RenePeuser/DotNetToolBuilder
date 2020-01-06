using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.SystemTest
{
    [TestClass]
    public class DeserializeDotNetToolTest
    {
        private DirectoryInfo _createdDirectory;
        private FileInfo _serializedDotNetTool;
        private DirectoryInfo _toolSerializeResult;

        [TestInitialize]
        public void Init()
        {

            _serializedDotNetTool = new FileInfo(Path.Combine(Environment.CurrentDirectory, "test.json"));
            _createdDirectory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "my.test"));
            _toolSerializeResult = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "saved-tools"));

            if (_createdDirectory.Exists)
            {
                _createdDirectory.Delete(true);
            }
        }

        [TestMethod]
        public async Task Creating_Dot_Net_Tool_From_Serialized_JSon()
        {
            var result = await Program.Main(new[] { "--from-file", _serializedDotNetTool.FullName }).ConfigureAwait(false);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public async Task Should_Create__A_DotNetTool_When_Used_Save_Tool_Option()
        {
            await Program.Main(new[] { "--from-file", _serializedDotNetTool.FullName, "--save-to", _toolSerializeResult.FullName }).ConfigureAwait(false);
            var savedDotNetTool = new FileInfo(Path.Combine(_toolSerializeResult.FullName, "my.test.json"));

            Assert.IsTrue(savedDotNetTool.Exists, $"Expected saved tool: '{savedDotNetTool.FullName}' was not created");
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
        }
    }
}
