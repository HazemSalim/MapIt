using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MapIt.Data;

namespace MapIt.Repository
{
    public class MerchantsRepository : Repository<Merchant>
    {
        public IQueryable<Merchant> Search(long? merchantId, int? active, DateTime? cDateFrom, DateTime? cDateTo, string keyword)
        {
            return base.Find(m =>
                    (merchantId.HasValue && merchantId.Value > 0 ? m.Id == merchantId.Value : true) &&
                    (active.HasValue ? (m.IsActive == (active.Value == 1 ? true : false)) : true) &&
                    ((cDateFrom.HasValue && cDateTo.HasValue) ? (m.AddedOn >= cDateFrom.Value && m.AddedOn < cDateTo.Value) : true) &&
                    ((!string.IsNullOrEmpty(keyword) ? m.FullName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? m.Country.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? m.City.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? m.Phone.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? m.Email.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? m.CompanyName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? m.Details.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)));
        }

        public Merchant GetByPhone(string phone, long merchantId = 0)
        {
            if (string.IsNullOrEmpty(phone) && phone.Trim() != string.Empty)
                return null;

            return base.First(m => m.Phone == phone && m.Id != merchantId);
        }

        public Merchant GetByEmail(string email, long merchantId = 0)
        {
            if (string.IsNullOrEmpty(email) && email.Trim() != string.Empty)
                return null;

            return base.First(m => m.Email == email && m.Id != merchantId);
        }

        public Merchant GetByCompanyName(string companyName, long merchantId = 0)
        {
            if (string.IsNullOrEmpty(companyName) && companyName.Trim() != string.Empty)
                return null;

            return base.First(m => m.CompanyName == companyName && m.Id != merchantId);
        }
    }
}
