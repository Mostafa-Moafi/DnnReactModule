using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("Module.UnitTests.Startup", "Module.UnitTests")]

namespace Module.UnitTests
{
    public class Startup : XunitTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink)
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }
    }
}
