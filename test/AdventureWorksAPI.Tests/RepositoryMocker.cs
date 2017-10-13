using AdventureWorksAPI.Core.DataLayer;
using Microsoft.Extensions.Options;

namespace AdventureWorksAPI.Tests
{
    public static class RepositoryMocker
    {
        public static IAdventureWorksRepository GetAdventureWorksRepository()
        {
            var appSettings = Options.Create(new AppSettings
            {
                ConnectionString = "server=(local);database=AdventureWorks2012;integrated security=yes;"
            });

            return new AdventureWorksRepository(new AdventureWorksDbContext(appSettings, new AdventureWorksEntityMapper()));
        }
    }
}
