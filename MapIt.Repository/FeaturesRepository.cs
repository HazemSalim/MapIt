using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class FeaturesRepository : Repository<Feature>
    {
        public List<GetFeatures_Result> GetBasic(int? propertyTypeId = null, string lang = "")
        {
            var result = Entities.GetFeatures(propertyTypeId.HasValue ? propertyTypeId.Value : 0).ToList();

            if (!String.IsNullOrEmpty(lang) && lang == "en")
            {
                result = result.OrderBy(r => r.TitleEN).ToList();
            }
            else if (!String.IsNullOrEmpty(lang) && lang == "ar")
            {
                result = result.OrderBy(r => r.TitleAR).ToList();
            }
            else
            {
                result = result.OrderBy(r => r.Id).ToList();
            }

            return result;
        }
    }
}
