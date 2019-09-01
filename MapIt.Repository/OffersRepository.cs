using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MapIt.Data;

namespace MapIt.Repository
{
    public class OffersRepository : Repository<Offer>
    {
        public IQueryable<Offer> Search(long? offerId, int? country, int? active, DateTime? cDateFrom, DateTime? cDateTo, string keyword)
        {
            return base.Find(u =>
                    (offerId.HasValue && offerId.Value > 0 ? u.Id == offerId.Value : true) &&
                    (country.HasValue && country.Value > 0 ? u.CountryId == country.Value : true) &&
                    (active.HasValue ? (u.IsActive == (active.Value == 1 ? true : false)) : true) &&
                    ((cDateFrom.HasValue && cDateTo.HasValue) ? (u.AddedOn >= cDateFrom.Value && u.AddedOn < cDateTo.Value) : true) &&
                    ((!string.IsNullOrEmpty(keyword) ? u.TitleEN.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.TitleAR.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.Phone.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)));
        }

        public bool IncreaseViewersCount(long offerId)
        {
            var offerObj = Entities.Offers.Where(o => o.Id == offerId).Single();
            offerObj.ViewersCount++;
            return Entities.SaveChanges() > 0;
        }

        public int DeleteAnyWay(Int32 offerId)
        {
            var offerObj = base.GetByKey(offerId);
            if (offerObj == null)
                return -3;

            Entities.ToObjectContext().Connection.Open();
            DbTransaction dbTrans = Entities.ToObjectContext().Connection.BeginTransaction();

            try
            {
                Entities.Notifications.Where(n => n.OfferId.HasValue && n.OfferId == offerId).ToList().ForEach(n => Entities.Notifications.Remove(n));
                Entities.Offers.Where(o => o.Id == offerId).ToList().ForEach(o => Entities.Offers.Remove(o));

                Entities.ToObjectContext().SaveChanges(false);
                Entities.ToObjectContext().AcceptAllChanges();
                dbTrans.Commit();

                return 1;
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbTrans.Dispose();
                dbTrans = null;
                Entities.ToObjectContext().Connection.Close();
            }
        }
    }
}
