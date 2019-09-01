using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertyPhotosRepository : Repository<PropertyPhoto>
    {
        public IQueryable<PropertyPhoto> GetByPropertyId(Int64 propertyId)
        {
            return base.Find(pp => pp.PropertyId == propertyId);
        }
    }
}
