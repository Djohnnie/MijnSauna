using System;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Common.DataTransferObjects.Logs;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class LogErrorRequestMapperTests
    {
        [Fact]
        public void Mapping_From_LogErrorRequestMapper_To_Log_Should_Work()
        {
            // Arrange
            var mapper = new LogErrorRequestMapper();
            var logErrorRequest =
                new Fixture().Build<LogErrorRequest>().Create();

            // Act
            var result = mapper.Map(logErrorRequest);

            // Assert
            result.Should().BeEquivalentTo(logErrorRequest);
            result.IsError.Should().BeTrue();
            result.TimeStamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}