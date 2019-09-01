using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertyVideosRepository : Repository<PropertyVideo>
    {
        public IQueryable<PropertyVideo> GetByPropertyId(Int64 propertyId)
        {
            return base.Find(pv => pv.PropertyId == propertyId);
        }
    }
}
