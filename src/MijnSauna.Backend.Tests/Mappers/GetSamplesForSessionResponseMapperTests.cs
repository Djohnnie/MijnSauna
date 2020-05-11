using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Backend.Model;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class GetSamplesForSessionResponseMapperTests
    {
        [Fact]
        public void Mapping_From_A_List_Of_Sample_To_GetSamplesForSessionResponse_Should_Work()
        {
            // Arrange
            var mapper = new GetSamplesForSessionResponseMapper();
            var session = new Session { Id = Guid.NewGuid() };
            var samples = new Fixture().Build<Sample>()
                .With(x => x.Session, session)
                .CreateMany(3).ToList();

            // Act
            var result = mapper.Map(samples);

            // Assert
            result.SessionId.Should().Be(Guid.Empty);
            result.Samples.Should().HaveCount(3);
            result.Samples.Should().BeEquivalentTo(samples, c => c.Excluding(x => x.SysId).Excluding(x => x.Session));
        }
    }
}