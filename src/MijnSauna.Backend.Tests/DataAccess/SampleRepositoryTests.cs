using System.Threading.Tasks;
using FluentAssertions;
using MijnSauna.Backend.Common;
using MijnSauna.Backend.DataAccess;
using MijnSauna.Backend.DataAccess.Repositories;
using MijnSauna.Backend.Tests.Extensions.AutoFixture;
using Xunit;

namespace MijnSauna.Backend.Tests.DataAccess
{
    public class SampleRepositoryTests
    {
        [Fact]
        public async Task Repository_GetAll_Should_Return_All_Records()
        {
            // Arrange
            var configurationHelper = new ConfigurationHelper();
            var dbContext = new DatabaseContext(configurationHelper);
            var session = SessionFixture.Create();
            var sample1 = SampleFixture.Create();
            sample1.Session = session;
            var sample2 = SampleFixture.Create();
            sample2.Session = session;
            await dbContext.Samples.AddRangeAsync(sample1, sample2);
            await dbContext.SaveChangesAsync();
            var playerRepository = new SampleRepository(new DatabaseContext(configurationHelper));

            // Act
            var result = await playerRepository.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Session.Should().BeNull();
            result[1].Session.Should().BeNull();
            result.Should().ContainEquivalentOf(sample1, c => c.Excluding(x => x.Session));
            result.Should().ContainEquivalentOf(sample2, c => c.Excluding(x => x.Session));
        }

        [Fact]
        public async Task Repository_Find_Should_Return_Queried_Records()
        {
            // Arrange
            var configurationHelper = new ConfigurationHelper();
            var dbContext = new DatabaseContext(configurationHelper);
            var session = SessionFixture.Create();
            var sample1 = SampleFixture.Create();
            sample1.Session = session;
            var sample2 = SampleFixture.Create();
            sample2.Session = session;
            var sample3 = SampleFixture.Create();
            sample3.Session = session;
            await dbContext.Samples.AddRangeAsync(sample1, sample2, sample3);
            await dbContext.SaveChangesAsync();
            var playerRepository = new SampleRepository(new DatabaseContext(configurationHelper));

            // Act
            var result = await playerRepository.Find(x => x.Id == sample2.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result[0].Session.Should().BeNull();
            result.Should().ContainEquivalentOf(sample2, c => c.Excluding(x => x.Session));
        }

        [Fact]
        public async Task Repository_Find_With_Include_Should_Return_Queried_Records()
        {
            // Arrange
            var configurationHelper = new ConfigurationHelper();
            var dbContext = new DatabaseContext(configurationHelper);
            var session = SessionFixture.Create();
            var sample1 = SampleFixture.Create();
            sample1.Session = session;
            var sample2 = SampleFixture.Create();
            sample2.Session = session;
            var sample3 = SampleFixture.Create();
            sample3.Session = session;
            await dbContext.Samples.AddRangeAsync(sample1, sample2, sample3);
            await dbContext.SaveChangesAsync();
            var playerRepository = new SampleRepository(new DatabaseContext(configurationHelper));

            // Act
            var result = await playerRepository.Find(x => x.Id == sample2.Id, inc => inc.Session);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result[0].Session.Should().NotBeNull();
            result.Should().ContainEquivalentOf(sample2, c => c.Excluding(x => x.Session));
        }
    }
}