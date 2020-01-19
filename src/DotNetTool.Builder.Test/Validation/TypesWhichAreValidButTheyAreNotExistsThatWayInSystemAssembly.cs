using System.Collections.Generic;
using System.Linq;

using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Validation;
using Extensions.Pack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Validation
{
    [TestClass]
    public class TypesWhichAreValidButTheyAreNotExistsThatWayInSystemAssembly
    {
        private ArgumentTypeOptimizer _argumentTypeOptimizer;
        private PrimitiveTypeNameValidator _primitiveTypeConverter;

        [TestInitialize]
        public void OnInit()
        {
            _argumentTypeOptimizer = new ArgumentTypeOptimizer(GetTypeOptimizers().ToList());
            _primitiveTypeConverter = new PrimitiveTypeNameValidator(new BuiltInTypeTableService());
        }

        [TestMethod]
        public void Should_All_Fail_Because_They_Are_Not_Exist_That_Way()
        {
            var optimizedTypeNames = ValidTypeNames().Select(type => _argumentTypeOptimizer.OptimizeType(type)).ToList();
            var result = optimizedTypeNames.Select(t => _primitiveTypeConverter.IsTypeName(t));
            var invalidTypes = result.Where(r => r.IsValid.IsFalse()).ToList();

            Assert.IsTrue(invalidTypes.IsEmpty(), AssertHelper.AssertHelper.ToErrorMessage(invalidTypes.Select(r => r.Errors), $"Following '{invalidTypes.Count}' types should not be valid:"));
        }

        private IEnumerable<ITypeNameOptimizer> GetTypeOptimizers()
        {
            var builtInTypeTableService = new BuiltInTypeTableService();

            yield return new DirectoryInfoOptimizer();
            yield return new FileInfoOptimizer();
            yield return new FileSystemInfoOptimizer();
            yield return new SystemTypeNameOptimizer(new PrimitiveTypeNameValidator(builtInTypeTableService), builtInTypeTableService);
        }

        private IEnumerable<string> ValidTypeNames()
        {
            yield return "object";
            yield return "string";
            yield return "byte";
            yield return "sbyte";
            yield return "double";
            yield return "decimal";
            yield return "char";
            yield return "bool";
            yield return "int";
            yield return "long";
            yield return "DateTime";
            yield return "Single";
            yield return "Boolean";
            yield return "Int16";
            yield return "UInt16";
            yield return "Int32";
            yield return "UInt32";
            yield return "Int64";
            yield return "UInt64";
        }
    }
}
