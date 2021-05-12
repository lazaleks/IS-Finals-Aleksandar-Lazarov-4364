using midTerm.Models.Profiles;
using System;
using Xunit;

namespace midTerm.AutoMapper.Tests
{
    public class AutoMapperConfigurationIsValid
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = AutoMapperModule.CreateMapperConfiguration<BaseProfile>();

            // Act/Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
