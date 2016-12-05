using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AdventureWorksAPI.Core.DataLayer
{
    public class AdventureWorksDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AdventureWorksDbContext(IOptions<AppSettings> appSettings, IEntityMapper entityMapper)
        {
            ConnectionString = appSettings.Value.ConnectionString;

            EntityMapper = entityMapper;
        }

        public String ConnectionString { get; }

        public IEntityMapper EntityMapper { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityMapper.MapEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
