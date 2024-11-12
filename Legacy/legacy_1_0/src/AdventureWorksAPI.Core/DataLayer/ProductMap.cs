using AdventureWorksAPI.Core.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Core.DataLayer
{
    public class ProductMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Product>();

            entity.ToTable("Product", "Production");

            entity.HasKey(p => new { p.ProductID });

            entity.Property(p => p.ProductID).UseSqlServerIdentityColumn();
        }
    }
}
