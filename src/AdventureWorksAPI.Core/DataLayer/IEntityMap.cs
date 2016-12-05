using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Core.DataLayer
{
    public interface IEntityMap
    {
        void Map(ModelBuilder modelBuilder);
    }
}
