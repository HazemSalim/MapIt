using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ComponentsRepository : Repository<Component>
    {
        public List<GetComponents_Result> GetBasic(int? propertyTypeId = null, string lang = "")
        {
            var result = Entities.GetComponents(propertyTypeId.HasValue ? propertyTypeId.Value : 0).ToList();

            if (!String.IsNullOrEmpty(lang) && lang == "en")
            {
                result = result.OrderBy(r => r.Ordering).ToList();
            }
            else if (!String.IsNullOrEmpty(lang) && lang == "ar")
            {
                result = result.OrderBy(r => r.Ordering).ToList();
            }
            else
            {
                result = result.OrderBy(r => r.Id).ToList();
            }

            return result;
        }
    }
}
