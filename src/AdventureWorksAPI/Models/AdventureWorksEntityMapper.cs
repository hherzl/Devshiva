using System.Collections.Generic;

namespace AdventureWorksAPI.Models
{
    public class AdventureWorksEntityMapper : EntityMapper
    {
        public AdventureWorksEntityMapper()
        {
            Mappings = new List<IEntityMap>()
            {
                new ProductMap() as IEntityMap
            };
        }
    }
}
