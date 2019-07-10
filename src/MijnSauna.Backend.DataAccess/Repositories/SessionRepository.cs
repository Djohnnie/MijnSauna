using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess.Repositories
{
    public class SessionRepository : Repository<Session>
    {
        public SessionRepository(DatabaseContext dbContext) : base(dbContext, dbContext.Sessions) { }
    }
}