using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MapIt.Data;

namespace MapIt.Repository
{
    public class BrokersRepository : Repository<Broker>
    {
        public IQueryable<Broker> Search(long? brokerId, int? country, int? city, int? active, DateTime? cDateFrom, DateTime? cDateTo, string keyword)
        {
            return base.Find(u =>
                    (brokerId.HasValue && brokerId.Value > 0 ? u.Id == brokerId.Value : true) &&
                    (country.HasValue && country.Value > 0 ? u.City.CountryId == country.Value : true) &&
                    (country.HasValue && country.Value > 0 ? u.CityId == country.Value : true) &&
                    (active.HasValue ? (u.IsActive == (active.Value == 1 ? true : false)) : true) &&
                    ((cDateFrom.HasValue && cDateTo.HasValue) ? (u.AddedOn >= cDateFrom.Value && u.AddedOn < cDateTo.Value) : true) &&
                    ((!string.IsNullOrEmpty(keyword) ? u.FullName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.Phone.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.Email.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.DetailsEN.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.DetailsAR.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)));
        }

        public bool DeleteBrokerAreas(Broker brokerEnt)
        {
            var brokerData = Entities.Brokers.Include("BrokerAreas").Where(p => p.Id == brokerEnt.Id).FirstOrDefault();
            var areaData = brokerData.BrokerAreas.ToList();
            foreach (var data in areaData)
            {
                Entities.BrokerAreas.Remove(data);
            }
            return Entities.SaveChanges() > 0;
        }

        public int DeleteAnyWay(Int32 brokerId)
        {
            var brokerObj = base.GetByKey(brokerId);
            if (brokerObj == null)
                return -3;

            Entities.ToObjectContext().Connection.Open();
            DbTransaction dbTrans = Entities.ToObjectContext().Connection.BeginTransaction();

            try
            {
                Entities.BrokerAreas.Where(ba => ba.BrokerId == brokerId).ToList().ForEach(ba => Entities.BrokerAreas.Remove(ba));
                Entities.Brokers.Where(b => b.Id == brokerId).ToList().ForEach(b => Entities.Brokers.Remove(b));

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
