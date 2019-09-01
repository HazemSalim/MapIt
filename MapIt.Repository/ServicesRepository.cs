using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MapIt.Data;
using System.Data.Entity.Core.Objects;

namespace MapIt.Repository
{
    public class ServicesRepository : Repository<Service>
    {
        public GeneralSetting GSetting
        {
            get
            {
                var gSettingsRepository = new GeneralSettingsRepository();
                var gSettingObj = gSettingsRepository.Get();
                return gSettingObj;
            }
        }

        public IQueryable<Service> Search(long? user, int? country, int? city, int? category, int? company, int? active, int? avDuration, string keyword)
        {
            return base.Find(s =>
                    (user.HasValue ? s.UserId == user.Value : true) &&
                    (country.HasValue ? s.City.CountryId == country.Value : true) &&
                    (city.HasValue ? s.CityId == city.Value : true) &&
                    (category.HasValue ? s.CategoryId == category.Value : true) &&
                    (company.HasValue ? (s.IsCompany == (company.Value == 1 ? true : false)) : true) &&
                    (active.HasValue ? (s.IsActive == (active.Value == 1 ? true : false)) : true) &&
                    ((!string.IsNullOrEmpty(keyword) ? s.Title.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? s.Description.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)) &&
                    (avDuration.Value == 1 ? ((!s.IsSpecial && EntityFunctions.AddDays(s.AddedOn, GSetting.NormalAdDuration).Value >= DateTime.Now) ||
                    (s.IsSpecial && EntityFunctions.AddDays(s.AddedOn, GSetting.SpecAdDuration).Value >= DateTime.Now)) : true));
        }

        public IQueryable<Service> GetAvServices()
        {
            return Find(s => s.IsActive && s.User.IsActive && !s.User.IsCanceled &&
                (!s.IsSpecial && EntityFunctions.AddDays(s.AddedOn, GSetting.NormalAdDuration).Value >= DateTime.Now) ||
                (s.IsSpecial && EntityFunctions.AddDays(s.AddedOn, GSetting.SpecAdDuration).Value >= DateTime.Now));
        }

        public bool IncreaseViewersCount(long serviceId)
        {
            var serObj = Entities.Services.Where(o => o.Id == serviceId).Single();
            serObj.ViewersCount++;
            return Entities.SaveChanges() > 0;
        }

        public int SetViewed(long serviceId, long userId)
        {
            var srvViewList = Entities.ServiceViews.Where(uv => uv.ServiceId == serviceId && uv.UserId == userId);
            if (srvViewList != null && srvViewList.Count() > 0)
            {
                foreach (var item in srvViewList)
                {
                    item.ViewedOn = DateTime.Now;
                }

                Entities.SaveChanges();
                return 2;
            }
            else
            {
                Entities.ServiceViews.Add(new ServiceView { ServiceId = serviceId, UserId = userId, ViewedOn = DateTime.Now });
                Entities.SaveChanges();
                return 1;
            }
        }

        public bool IsViewed(long serviceId, long userId)
        {
            return Entities.ServiceViews.Any(p => p.ServiceId == serviceId && p.UserId == userId) ? true : false;
        }

        public int SetFavorite(long serviceId, long userId)
        {
            var serFavList = Entities.ServiceFavorites.Where(sf => sf.ServiceId == serviceId && sf.UserId == userId);
            if (serFavList != null && serFavList.Count() > 0)
            {
                foreach (var item in serFavList)
                {
                    Entities.ServiceFavorites.Remove(item);
                }

                Entities.SaveChanges();
                return 2;
            }
            else
            {
                Entities.ServiceFavorites.Add(new ServiceFavorite { ServiceId = serviceId, UserId = userId });
                Entities.SaveChanges();
                return 1;
            }
        }

        public decimal SetRate(long serviceId, long userId, int rate)
        {
            var serRateList = Entities.ServiceRates.Where(sr => sr.ServiceId == serviceId && sr.UserId == userId);
            if (serRateList != null && serRateList.Count() > 0)
            {
                foreach (var item in serRateList)
                {
                    item.Rate = rate;
                }

                Entities.SaveChanges();
                //return 1;
            }
            else
            {
                Entities.ServiceRates.Add(new ServiceRate { ServiceId = serviceId, UserId = userId, Rate = rate });
                Entities.SaveChanges();
                //return 1;
            }

            double? RatingAverage = Entities.ServiceRates.Where(x => x.ServiceId == serviceId).Average(x => x.Rate);
            decimal numb = RatingAverage == null ? 0 : decimal.Parse(RatingAverage.ToString());
            return Math.Ceiling(numb);
        }

        public bool IsFavourite(long serviceId, long userId)
        {
            return Entities.ServiceFavorites.Any(p => p.ServiceId == serviceId && p.UserId == userId) ? true : false;
        }

        public long SetReport(long serviceId, long userId, int reasonId)
        {
            var abusives = Entities.ServiceReports.Where(sr => sr.ServiceId == serviceId && sr.UserId == userId).ToList();
            if (abusives != null && abusives.Count > 0)
            {
                return -2;
            }
            else
            {
                var serviceReport = new ServiceReport();
                serviceReport.ServiceId = serviceId;
                serviceReport.UserId = userId;
                serviceReport.ReasonId = reasonId;
                serviceReport.CreatedOn = DateTime.Now;
                Entities.ServiceReports.Add(serviceReport);
                Entities.SaveChanges();
                return serviceReport.Id;
            }
        }

        public bool IsReport(long serviceId, long userId)
        {
            return Entities.ServiceReports.Any(s => s.ServiceId == serviceId && s.UserId == userId) ? true : false;
        }

        public bool DeleteServiceAreas(Service serviceEnt)
        {
            var serviceData = Entities.Services.Include("ServiceAreas").Where(p => p.Id == serviceEnt.Id).FirstOrDefault();
            var areaData = serviceData.ServiceAreas.ToList();
            foreach (var data in areaData)
            {
                Entities.ServiceAreas.Remove(data);
            }
            return Entities.SaveChanges() > 0;
        }

        public int DeleteAnyWay(Int64 serviceId)
        {
            var serviceObj = base.GetByKey(serviceId);
            if (serviceObj == null)
                return -3;

            Entities.ToObjectContext().Connection.Open();
            DbTransaction dbTrans = Entities.ToObjectContext().Connection.BeginTransaction();

            try
            {
                Entities.ServiceAreas.Where(sa => sa.ServiceId == serviceId).ToList().ForEach(sa => Entities.ServiceAreas.Remove(sa));
                Entities.ServiceComments.Where(sc => sc.ServiceId == serviceId).ToList().ForEach(sc => Entities.ServiceComments.Remove(sc));
                Entities.ServiceFavorites.Where(sf => sf.ServiceId == serviceId).ToList().ForEach(sf => Entities.ServiceFavorites.Remove(sf));
                Entities.ServicePhotos.Where(sp => sp.ServiceId == serviceId).ToList().ForEach(sp => Entities.ServicePhotos.Remove(sp));
                Entities.ServiceRates.Where(sr => sr.ServiceId == serviceId).ToList().ForEach(sr => Entities.ServiceRates.Remove(sr));
                Entities.ServiceReports.Where(sr => sr.ServiceId == serviceId).ToList().ForEach(sr => Entities.ServiceReports.Remove(sr));
                Entities.ServiceViews.Where(sv => sv.ServiceId == serviceId).ToList().ForEach(sv => Entities.ServiceViews.Remove(sv));
                Entities.Notifications.Where(n => n.ServiceId.HasValue && n.ServiceId == serviceId).ToList().ForEach(n => Entities.Notifications.Remove(n));
                Entities.Services.Where(s => s.Id == serviceId).ToList().ForEach(s => Entities.Services.Remove(s));

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
