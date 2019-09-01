using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ServiceReportsRepository : Repository<ServiceReport>
    {
        public IQueryable<ServiceReport> GetByPropertyId(Int64 serviceId)
        {
            return base.Find(sr => sr.ServiceId == serviceId);
        }
    }
}
