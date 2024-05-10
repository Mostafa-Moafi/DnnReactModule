using DnnReactModule.Utilities;
using Xunit;
using Assert = Xunit.Assert;

namespace Module.UnitTests.Utilities
{
    public class GlobalParameterTests
    {
        [Fact]
        public void ModuleNameIsValid()
        {
            var moduleName = GlobalParameter.ModuleName;
            Assert.Equal(expected: "DnnReactModule", moduleName);
        }
    }
}
