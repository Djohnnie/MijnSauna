using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess.Repositories
{
    public class ConfigurationValueRepository : Repository<ConfigurationValue>
    {
        public ConfigurationValueRepository(DatabaseContext dbContext) : base(dbContext, dbContext.ConfigurationValues) { }
    }
}