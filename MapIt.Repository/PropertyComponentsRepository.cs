using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertyComponentsRepository : Repository<PropertyComponent>
    {
        public IQueryable<PropertyComponent> GetByPropertyId(Int64 propertyId)
        {
            return base.Find(pc => pc.PropertyId == propertyId);
        }
    }
}
