using System.Collections.Generic;

namespace AdventureWorksAPI.Core.DataLayer
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
