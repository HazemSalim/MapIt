using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PhotographersRepository : Repository<Photographer>
    {
        public IQueryable<Photographer> Search(long? pId, int? active, DateTime? cDateFrom, DateTime? cDateTo, string keyword)
        {
            return base.Find(p =>
                    (pId.HasValue && pId.Value > 0 ? p.Id == pId.Value : true) &&
                    (active.HasValue ? (p.IsActive == (active.Value == 1 ? true : false)) : true) &&
                    ((cDateFrom.HasValue && cDateTo.HasValue) ? (p.AddedOn >= cDateFrom.Value && p.AddedOn < cDateTo.Value) : true) &&
                    ((!string.IsNullOrEmpty(keyword) ? p.FullName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? p.Country.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? p.City.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? p.Phone.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? p.Email.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? p.Details.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)));
        }

        public Photographer GetByPhone(string phone, long pId = 0)
        {
            if (string.IsNullOrEmpty(phone) && phone.Trim() != string.Empty)
                return null;

            return base.First(p => p.Phone == phone && p.Id != pId);
        }

        public Photographer GetByEmail(string email, long pId = 0)
        {
            if (string.IsNullOrEmpty(email) && email.Trim() != string.Empty)
                return null;

            return base.First(p => p.Email == email && p.Id != pId);
        }
    }
}
