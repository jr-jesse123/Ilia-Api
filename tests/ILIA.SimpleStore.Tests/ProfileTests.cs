using AutoMapper;
using ILIA.SimpleStore.API.Mappings;
using Xunit;

namespace ILIA.SimpleStore.Tests
{
    public class ProfileTests
    {
        [Fact]
        public void Test1()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToModelMappingProfile>());
            config.AssertConfigurationIsValid();

        }
    }
}