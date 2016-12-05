using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Core.DataLayer
{
    public class EntityMapper : IEntityMapper
    {
        public IEnumerable<IEntityMap> Mappings { get; protected set; }

        public void MapEntities(ModelBuilder modelBuilder)
        {
            foreach (var item in Mappings)
            {
                item.Map(modelBuilder);
            }
        }
    }
}
