using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertyFeaturesRepository : Repository<PropertyFeature>
    {
        public IQueryable<PropertyFeature> GetByPropertyId(Int64 propertyId)
        {
            return base.Find(pf => pf.PropertyId == propertyId);
        }
    }
}
