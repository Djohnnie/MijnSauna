using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MijnSauna.Backend.DataAccess.Repositories.Interfaces;
using MijnSauna.Backend.Logic;
using MijnSauna.Backend.Mappers;
using MijnSauna.Backend.Model;
using MijnSauna.Backend.Tests.Extensions.Moq;
using Moq;
using Xunit;

namespace MijnSauna.Backend.Tests.Logic
{
    public class SampleLogicTests
    {
        [Fact]
        public async Task SampleLogic_GetSamplesForSession_Should_Only_Get_Samples_For_Given_Session()
        {
            // Arrange
            var sessionRepository = new Mock<IRepository<Session>>();
            var sampleRepository = new Mock<IRepository<Sample>>();
            var session1 = new Session { Id = Guid.NewGuid() };
            var session2 = new Session { Id = Guid.NewGuid() };
            var samples = new[] { session1, session2, session1 }.Select(session =>
                  new Fixture().Build<Sample>().With(x => x.Session, session).Create()).ToList();
            sampleRepository.Setup(r => r.Find(Any.Predicate<Sample>())).ExecutesAsyncPredicateOn(samples);
            var getSamplesForSessionResponseMapper = new GetSamplesForSessionResponseMapper();
            var logic = new SampleLogic(sessionRepository.Object, sampleRepository.Object, getSamplesForSessionResponseMapper, null, null);

            // Act
            var result = await logic.GetSamplesForSession(session2.Id);

            // Assert
            result.SessionId.Should().Be(session2.Id);
            result.Samples.Should().HaveCount(1);
        }

        [Fact]
        public async Task SampleLogic_CreateSampleForSession_Should_Return_Null_For_Unknown_SessionId()
        {
            // Arrange
            var sessionRepository = new Mock<IRepository<Session>>();
            var sessions = new Fixture().Build<Session>().With(x => x.Samples, new List<Sample>()).CreateMany(3).ToList();
            sessionRepository.Setup(x => x.Single(Any.Predicate<Session>())).ExecutesAsyncPredicateOn(sessions);
            var sampleRepository = new Mock<IRepository<Sample>>();
            var logic = new SampleLogic(sessionRepository.Object, sampleRepository.Object, null, null, null);

            // Act
            var result = await logic.CreateSampleForSession(Guid.NewGuid(), null);

            // Assert
            result.Should().BeNull();
        }
    }
}