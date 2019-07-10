using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess.Repositories
{
    public class SampleRepository : Repository<Sample>
    {
        public SampleRepository(DatabaseContext dbContext) : base(dbContext, dbContext.Samples) { }
    }
}