using Microsoft.Extensions.Options;
using NorthwindApi.Dapper.Models;

namespace NorthwindApi.Dapper.Tests
{
    public static class RepositoryMocker
    {
        public static INorthwindRepository GetNorthwindRepository()
            => new NorthwindRepository(Options.Create(new AppSettings
            {
                ConnectionString = "server=(local);database=Northwind;integrated security=yes;"
            }));
    }
}
