using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Common.DataTransferObjects.Sessions;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class CreateSessionRequestMapperTests
    {
        [Fact]
        public void Mapping_From_CreateSessionRequest_To_Session_Should_Work()
        {
            // Arrange
            var mapper = new CreateSessionRequestMapper();
            var createSessionRequest = new Fixture().Create<CreateSessionRequest>();

            // Act
            var result = mapper.Map(createSessionRequest);

            // Assert
            result.Should().BeEquivalentTo(createSessionRequest);
            result.ActualEnd.Should().Be(createSessionRequest.End);
        }
    }
}