using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.TypeTest
{

    [TestClass]
    public class ValidTypeTests
    {
        [TestMethod]
        public void All_Types_Must_Be_Supported_Primitive_Types()
        {
            var assemblies = TypeNames().Select(t => t.Assembly).ToList().Distinct();


            var allTypes = TypeNames().Select(t => $"System.{t}").ToList();
            var result = allTypes.Select(Type.GetType);

            Assert.AreEqual(allTypes.Count, result.Count());
        }

        private IEnumerable<Type> TypeNames()
        {
            yield return typeof(object);
            yield return typeof(string);
            yield return typeof(sbyte);
            yield return typeof(sbyte);
            yield return typeof(double);
            yield return typeof(decimal);
            yield return typeof(char);
            yield return typeof(bool);
            yield return typeof(int);
            yield return typeof(long);

            yield return typeof(DateTime);
            yield return typeof(TimeSpan);

            yield return typeof(Single);
            yield return typeof(Boolean);
                         
            yield return typeof(Int16);
            yield return typeof(UInt16);
            yield return typeof(Int32);
            yield return typeof(UInt32);
            yield return typeof(Int64);
            yield return typeof(UInt64);
                         
            yield return typeof(System.IO.FileInfo);
            yield return typeof(System.IO.DirectoryInfo);
            yield return typeof(System.IO.FileSystemInfo);
            yield return typeof(System.IO.FileSystemInfo);
            yield return typeof(Path);
        }

    }

    [TestClass]
    public class InValidTypeTests
    {
        [TestMethod]
        public void All_Types_Must_Be_Supported_Primitive_Types()
        {

        }
    }
}
