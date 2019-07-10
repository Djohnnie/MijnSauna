using System.Collections.Generic;
using AutoFixture;
using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.Tests.Extensions.AutoFixture
{
    public static class SessionFixture
    {
        public static Session Create()
        {
            return new Fixture()
                .Build<Session>()
                .With(x => x.Samples, (List<Sample>)null)
                .Create();
        }
    }
}