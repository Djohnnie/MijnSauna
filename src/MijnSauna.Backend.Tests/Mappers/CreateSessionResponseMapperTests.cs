using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Backend.Model;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class CreateSessionResponseMapperTests
    {
        [Fact]
        public void Mapping_From_Session_To_CreateSessionResponse_Should_Work()
        {
            // Arrange
            var mapper = new CreateSessionResponseMapper();
            var session = new Fixture().Build<Session>()
                .With(x => x.Samples, new List<Sample>())
                .Create();

            // Act
            var result = mapper.Map(session);

            // Assert
            result.SessionId.Should().Be(session.Id);
        }
    }
}