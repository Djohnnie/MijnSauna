using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Backend.Model;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class GetActiveSessionResponseMapperTests
    {
        [Fact]
        public void Mapping_From_Session_To_GetActiveSessionResponse_Should_Work()
        {
            // Arrange
            var mapper = new GetActiveSessionResponseMapper();
            var session = new Fixture().Build<Session>()
                .With(x => x.Samples, new List<Sample>())
                .Create();

            // Act
            var result = mapper.Map(session);

            // Assert
            result.SessionId.Should().Be(session.Id);
            result.Start.Should().Be(session.Start);
            result.End.Should().Be(session.End);
            result.IsSauna.Should().Be(session.IsSauna);
            result.IsInfrared.Should().Be(session.IsInfrared);
            result.TemperatureGoal.Should().Be(session.TemperatureGoal);
        }
    }
}