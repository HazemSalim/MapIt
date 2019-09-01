using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class WatchListsRepository : Repository<WatchList>
    {
        public IQueryable<User> GetMatchUsers(int typeId, int? countryId, int? cityId, int? areaId, int? purposeId)
        {
            return base.Find(w =>
                    (w.TypeId == typeId) &&
                    (countryId.HasValue ? !w.CountryId.HasValue || (w.CountryId.HasValue && w.CountryId == countryId.Value) : true) &&
                    (cityId.HasValue ? !w.CityId.HasValue || (w.CityId.HasValue && w.CityId == cityId.Value) : true) &&
                    (areaId.HasValue ? !w.AreaId.HasValue || (w.AreaId.HasValue && w.AreaId == areaId.Value) : true) &&
                    (purposeId.HasValue ? !w.PurposeId.HasValue || (w.PurposeId.HasValue && w.PurposeId == purposeId.Value) : true)).Select(u => u.User).Distinct();
        }
    }
}
