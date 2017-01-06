using AdventureWorksAPI.Core.DataLayer;
using Microsoft.Extensions.Options;

namespace AdventureWorksAPI.Tests
{
    public static class RepositoryMocker
    {
        public static IAdventureWorksRepository AdventureWorksRepository
        {
            get
            {
                var appSettings = Options.Create(new AppSettings
                {
                    ConnectionString = "server=(local);database=AdventureWorks2012;integrated security=yes;"
                });

                var entityMapper = new AdventureWorksEntityMapper() as IEntityMapper;

                return new AdventureWorksRepository(new AdventureWorksDbContext(appSettings, entityMapper)) as IAdventureWorksRepository;
            }
        }
    }
}
