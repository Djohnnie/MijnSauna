using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess.Repositories
{
    public class LogRepository : Repository<Log>
    {
        public LogRepository(DatabaseContext dbContext) : base(dbContext, dbContext.Logs) { }
    }
}