using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Models
{
    public interface IEntityMap
    {
        void Map(ModelBuilder modelBuilder);
    }
}
