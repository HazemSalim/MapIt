using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertyReportsRepository : Repository<PropertyReport>
    {
        public IQueryable<PropertyReport> GetByPropertyId(Int64 propertyId)
        {
            return base.Find(pr => pr.PropertyId == propertyId);
        }
    }
}
