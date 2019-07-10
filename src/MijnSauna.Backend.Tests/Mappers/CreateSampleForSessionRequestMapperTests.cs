using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Common.DataTransferObjects.Samples;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class CreateSampleForSessionRequestMapperTests
    {
        [Fact]
        public void Mapping_From_CreateSampleForSessionRequest_To_Sample_Should_Work()
        {
            // Arrange
            var mapper = new CreateSampleForSessionRequestMapper();
            var createSessionRequest = new Fixture().Create<CreateSampleForSessionRequest>();

            // Act
            var result = mapper.Map(createSessionRequest);

            // Assert
            result.Should().BeEquivalentTo(createSessionRequest);
        }
    }
}