using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ServicesCategoriesRepository : Repository<ServicesCategory>
    {
        public List<ServicesCategory> ServicesCategories
        {
            get
            {
                return base.Find(c => c.IsActive).ToList();
            }
        }

        public List<ServicesCategory> MainServicesCategories
        {
            get
            {
                return base.Find(c => !c.ParentId.HasValue && c.IsActive).ToList();
            }
        }
    }
}
