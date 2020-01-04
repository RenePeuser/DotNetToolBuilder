using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.SystemTest
{
    [Ignore]
    [TestClass]
    public class DeserializeDotNetToolTest
    {
        private FileInfo _serializedDotNetTool;
        private DirectoryInfo _createdDirectory;

        [TestInitialize]
        public void Init()
        {
            _serializedDotNetTool = new FileInfo(Path.Combine(Environment.CurrentDirectory, "test.json"));
            _createdDirectory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "my.test"));

            if (_createdDirectory.Exists)
            {
                _createdDirectory.Delete(true);
            }
        }

        [TestMethod]
        public async Task Creating_Dot_Net_Tool_From_Serialized_JSon()
        {
            var result = await Program.Main(new[] { _serializedDotNetTool.FullName, "--no-visualstudio" });

            Assert.AreEqual(0, result);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_createdDirectory.Exists)
            {
                _createdDirectory.Delete(true);
            }
        }
    }
}
