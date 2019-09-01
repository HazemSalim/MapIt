using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ServiceAreasRepository : Repository<ServiceArea>
    {
        public IQueryable<ServiceArea> GetByServiceId(Int64 serviceId)
        {
            return base.Find(sa => sa.ServiceId == serviceId);
        }
    }
}
