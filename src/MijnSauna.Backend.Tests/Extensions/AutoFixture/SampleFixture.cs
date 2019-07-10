using AutoFixture;
using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.Tests.Extensions.AutoFixture
{
    public static class SampleFixture
    {
        public static Sample Create()
        {
            return new Fixture()
                .Build<Sample>()
                .With(x => x.Session, (Session)null)
                .Create();
        }
    }
}