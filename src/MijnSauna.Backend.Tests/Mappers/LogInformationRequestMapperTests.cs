using System;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.Mappers;
using MijnSauna.Common.DataTransferObjects.Logs;
using Xunit;

namespace MijnSauna.Backend.Tests.Mappers
{
    public class LogInformationRequestMapperTests
    {
        [Fact]
        public void Mapping_From_LogInformationRequestMapper_To_Log_Should_Work()
        {
            // Arrange
            var mapper = new LogInformationRequestMapper();
            var logInformationRequest =
                new Fixture().Build<LogInformationRequest>().Create();

            // Act
            var result = mapper.Map(logInformationRequest);

            // Assert
            result.Should().BeEquivalentTo(logInformationRequest);
            result.IsError.Should().BeFalse();
            result.TimeStamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}