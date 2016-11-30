using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Models
{
    public interface IEntityMapper
    {
        IEnumerable<IEntityMap> Mappings { get; }

        void MapEntities(ModelBuilder modelBuilder);
    }
}
