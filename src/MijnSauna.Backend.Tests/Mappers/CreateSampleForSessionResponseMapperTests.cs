using System;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Backend.Model;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class CreateSampleForSessionResponseMapperTests
    {
        [Fact]
        public void Mapping_From_Sample_To_CreateSampleForSessionResponse_Should_Work()
        {
            // Arrange
            var mapper = new CreateSampleForSessionResponseMapper();
            var sample = new Fixture().Build<Sample>()
                .With(x => x.Session, new Session { Id = Guid.NewGuid() })
                .Create();

            // Act
            var result = mapper.Map(sample);

            // Assert
            result.SampleId.Should().Be(sample.Id);
            result.SessionId.Should().Be(sample.Session.Id);
        }
    }
}