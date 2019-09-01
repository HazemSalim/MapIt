using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ServicePhotosRepository : Repository<ServicePhoto>
    {
        public IQueryable<ServicePhoto> GetByServiceId(Int64 serviceId)
        {
            return base.Find(sp => sp.ServiceId == serviceId);
        }
    }
}
