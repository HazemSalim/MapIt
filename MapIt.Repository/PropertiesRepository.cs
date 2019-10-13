using System;
using System.Linq;
using MapIt.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace MapIt.Repository
{
    public class PropertiesRepository : Repository<Property>
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

        List<int> tointarray(string value, char sep)
        {
            if (string.IsNullOrEmpty(value))
                return new List<int>();

            string[] sa = value.Split(sep);
            List<int> ia = new List<int>();
            for (int i = 0; i < sa.Count(); ++i)
            {
                int j;
                string s = sa[i];
                if (int.TryParse(s, out j))
                {
                    ia.Add(j);
                }
            }
            return ia;
        }

        public IQueryable<Property> Search(long? propertyId, long? userId, int? purposeId, int? typeId, int? countryId, int? cityId, int? areaId, int? blockId,
            string street, string portalAddress, string paci, double? areaFrom, double? areaTo, int? yearFrom, int? yearTo, double? mIncomeFrom, double? mIncomeTo, double? sPriceFrom,
            double? sPriceTo, double? rPriceFrom, double? rPriceTo, DateTime? addedFrom, DateTime? addedTo, int? special, int? available, int? active, int? canceled,
            int? admin, int? avDuration, string keyword = "", int userTypeID = 0, string propertytypesIDs = "", string usertypesIDs = "")
        {
            var tmpUserTpes = tointarray(usertypesIDs, ',');
            var tmpPropertyTypesIDs = tointarray(propertytypesIDs, ',');

            return Find(p =>
                                       (p.User != null && tmpUserTpes.Count > 0 ? tmpUserTpes.Contains(p.User.UserTypeID.Value) : true) &&
                                       (tmpPropertyTypesIDs.Count > 0 ? tmpPropertyTypesIDs.Contains(p.TypeId) : true) &&

                                       (userTypeID > 0 && p.User != null ? p.User.UserTypeID == userTypeID : true) &&
                                       (propertyId.HasValue && propertyId.Value > 0 ? p.Id == propertyId.Value : true) &&
                                       (userId.HasValue && userId.Value > 0 ? p.UserId == userId.Value : true) &&
                                       (purposeId.HasValue && purposeId.Value > 0 ? p.PurposeId == purposeId.Value : true) &&
                                       (typeId.HasValue && typeId.Value > 0 ? p.TypeId == typeId.Value : true) &&
                                       (countryId.HasValue && countryId.Value > 0 ? p.CountryId == countryId.Value : true) &&
                                       (cityId.HasValue && cityId.Value > 0 ? (p.BlockId.HasValue ? p.Block.Area.CityId == cityId.Value : true) : true) &&
                                       (areaId.HasValue && areaId.Value > 0 ? (p.BlockId.HasValue ? p.Block.AreaId == areaId.Value : true) : true) &&
                                       (blockId.HasValue && blockId.Value > 0 ? (p.BlockId.HasValue ? p.BlockId == blockId.Value : true) : true) &&
                                       (!string.IsNullOrEmpty(street) ? p.Street.Replace(" ", string.Empty).ToLower().Contains(street.Replace(" ", string.Empty).ToLower()) : true) &&
                                       ((!string.IsNullOrEmpty(portalAddress) ? p.PortalAddressEN.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.PortalAddressAR.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.Block.Area.City.TitleEN.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.Block.Area.City.TitleAR.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.Block.Area.TitleEN.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.Block.Area.TitleAR.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.Block.TitleEN.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                                       (!string.IsNullOrEmpty(portalAddress) ? p.Block.TitleAR.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true)) &&
                                       (!string.IsNullOrEmpty(paci) ? p.PACI.Replace(" ", string.Empty).ToLower().Contains(paci.Replace(" ", string.Empty).ToLower()) : true) &&
                                       ((areaFrom.HasValue && areaFrom.Value > 0 && areaTo.HasValue && areaTo.Value > 0) ?
                                       (p.Area.Value >= areaFrom.Value && p.Area.Value <= areaTo.Value) : true) &&
                                       ((yearFrom.HasValue && yearFrom.Value > 0 && yearTo.HasValue && yearTo.Value > 0) ?
                                       (p.BuildingAge.Value >= yearFrom.Value && p.BuildingAge.Value < yearTo.Value) : true) &&
                                       ((mIncomeFrom.HasValue && mIncomeFrom.Value > 0 && mIncomeTo.HasValue && mIncomeTo.Value > 0) ?
                                       (p.MonthlyIncome.Value >= mIncomeFrom.Value && p.MonthlyIncome.Value < mIncomeTo.Value) : true) &&
                                       ((sPriceFrom.HasValue && sPriceFrom.Value > 0 && sPriceTo.HasValue && sPriceTo.Value > 0) ?
                                       (p.SellingPrice.Value >= sPriceFrom.Value && p.SellingPrice.Value < sPriceTo.Value) : true) &&
                                       ((rPriceFrom.HasValue && rPriceFrom.Value > 0 && rPriceTo.HasValue && rPriceTo.Value > 0) ?
                                       (p.RentPrice.Value >= rPriceFrom.Value && p.RentPrice.Value < rPriceTo.Value) : true) &&
                                       ((addedFrom.HasValue && addedTo.HasValue) ? (p.AddedOn >= addedFrom.Value && p.AddedOn < addedTo.Value) : true) &&
                                       (special.HasValue ? (p.IsSpecial == (special.Value == 1 ? true : false)) : true) &&
                                       (available.HasValue ? (p.IsAvailable == (available.Value == 1 ? true : false)) : true) &&
                                       (active.HasValue ? (p.IsActive == (active.Value == 1 ? true : false)) : true) &&
                                       (admin.HasValue ? (p.AdminAdded == (admin.Value == 1 ? true : false)) : true) &&
                                       (canceled.HasValue ? (p.User.IsCanceled == (canceled.Value == 1 ? true : false)) : true)
                                       &&
                                       ((!string.IsNullOrEmpty(keyword) ? p.User.Phone.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                                        (!string.IsNullOrEmpty(keyword) ? p.Street.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                                        (!string.IsNullOrEmpty(keyword) ? p.PortalAddressEN.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                                        (!string.IsNullOrEmpty(keyword) ? p.PortalAddressAR.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                                        (!string.IsNullOrEmpty(keyword) ? p.PACI.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)) &&
                                        (avDuration.Value == 1 ? ((!p.IsSpecial && System.Data.Entity.DbFunctions.AddDays(p.AddedOn, GSetting.NormalAdDuration).Value >= DateTime.Now) ||
                                        (p.IsSpecial && System.Data.Entity.DbFunctions.AddDays(p.AddedOn, GSetting.SpecAdDuration).Value >= DateTime.Now)) : true));
        }

        public IQueryable<Property> GetAvProperties()
        {
            return Find(p => p.IsActive && p.IsAvailable && p.User.IsActive && !p.User.IsCanceled &&
                (!p.IsSpecial && System.Data.Entity.DbFunctions.AddDays(p.AddedOn, GSetting.NormalAdDuration).Value >= DateTime.Now) ||
                (p.IsSpecial && System.Data.Entity.DbFunctions.AddDays(p.AddedOn, GSetting.SpecAdDuration).Value >= DateTime.Now));
        }

        public bool IncreaseViewersCount(long propertyId)
        {
            var proObj = Entities.Properties.Where(o => o.Id == propertyId).Single();
            proObj.ViewersCount++;
            return Entities.SaveChanges() > 0;
        }

        public int SetViewed(long propertyId, long userId)
        {
            var proViewList = Entities.PropertyViews.Where(uv => uv.PropertyId == propertyId && uv.UserId == userId);
            if (proViewList != null && proViewList.Count() > 0)
            {
                foreach (var item in proViewList)
                {
                    item.ViewedOn = DateTime.Now;
                }

                Entities.SaveChanges();
                return 2;
            }
            else
            {
                Entities.PropertyViews.Add(new PropertyView { PropertyId = propertyId, UserId = userId, ViewedOn = DateTime.Now });
                Entities.SaveChanges();
                return 1;
            }
        }

        public bool IsViewed(long propertyId, long userId)
        {
            return Entities.PropertyViews.Any(p => p.PropertyId == propertyId && p.UserId == userId) ? true : false;
        }

        public int SetFavorite(long propertyId, long userId)
        {
            var proFavList = Entities.PropertyFavorites.Where(uf => uf.PropertyId == propertyId && uf.UserId == userId);
            if (proFavList != null && proFavList.Count() > 0)
            {
                foreach (var item in proFavList)
                {
                    Entities.PropertyFavorites.Remove(item);
                }

                Entities.SaveChanges();
                return 2;
            }
            else
            {
                Entities.PropertyFavorites.Add(new PropertyFavorite { PropertyId = propertyId, UserId = userId });
                Entities.SaveChanges();
                return 1;
            }
        }

        public bool IsFavourite(long propertyId, long userId)
        {
            return Entities.PropertyFavorites.Any(p => p.PropertyId == propertyId && p.UserId == userId) ? true : false;
        }

        public long SetReport(long propertyId, long userId, int reasonId, string notes)
        {
            var abusives = Entities.PropertyReports.Where(pr => pr.PropertyId == propertyId && pr.UserId == userId).ToList();
            if (abusives != null && abusives.Count > 0)
            {
                return -2;
            }
            else
            {
                var propertyReport = new PropertyReport();
                propertyReport.PropertyId = propertyId;
                propertyReport.UserId = userId;
                propertyReport.ReasonId = reasonId;
                propertyReport.Notes = notes;
                propertyReport.CreatedOn = DateTime.Now;
                Entities.PropertyReports.Add(propertyReport);
                Entities.SaveChanges();
                return propertyReport.Id;
            }
        }

        public bool IsReport(long propertyId, long userId)
        {
            return Entities.PropertyReports.Any(p => p.PropertyId == propertyId && p.UserId == userId) ? true : false;
        }

        public bool DeletePropertyComponents(Property PropertyEnt)
        {
            var propertyData = Entities.Properties.Include("PropertyComponents").Where(p => p.Id == PropertyEnt.Id).FirstOrDefault();
            var compData = propertyData.PropertyComponents.ToList();
            foreach (var data in compData)
            {
                Entities.PropertyComponents.Remove(data);
            }
            return Entities.SaveChanges() > 0;
        }

        public bool DeletePropertyFeatures(Property PropertyEnt)
        {
            var propertyData = Entities.Properties.Include("PropertyFeatures").Where(p => p.Id == PropertyEnt.Id).FirstOrDefault();
            var featData = propertyData.PropertyFeatures.ToList();
            foreach (var data in featData)
            {
                Entities.PropertyFeatures.Remove(data);
            }
            return Entities.SaveChanges() > 0;
        }

        public int DeleteAnyWay(Int64 propertyId)
        {
            var propertyObj = base.GetByKey(propertyId);
            if (propertyObj == null)
                return -3;

            Entities.ToObjectContext().Connection.Open();
            DbTransaction dbTrans = Entities.ToObjectContext().Connection.BeginTransaction();

            try
            {
                Entities.PropertyComments.Where(pc => pc.PropertyId == propertyId).ToList().ForEach(pc => Entities.PropertyComments.Remove(pc));
                Entities.PropertyComponents.Where(pc => pc.PropertyId == propertyId).ToList().ForEach(pc => Entities.PropertyComponents.Remove(pc));
                Entities.PropertyFavorites.Where(pf => pf.PropertyId == propertyId).ToList().ForEach(pf => Entities.PropertyFavorites.Remove(pf));
                Entities.PropertyFeatures.Where(pf => pf.PropertyId == propertyId).ToList().ForEach(pf => Entities.PropertyFeatures.Remove(pf));
                Entities.PropertyPhotos.Where(pp => pp.PropertyId == propertyId).ToList().ForEach(pp => Entities.PropertyPhotos.Remove(pp));
                Entities.PropertyReports.Where(pr => pr.PropertyId == propertyId).ToList().ForEach(pr => Entities.PropertyReports.Remove(pr));
                Entities.PropertyVideos.Where(pv => pv.PropertyId == propertyId).ToList().ForEach(pv => Entities.PropertyVideos.Remove(pv));
                Entities.PropertyViews.Where(pv => pv.PropertyId == propertyId).ToList().ForEach(pv => Entities.PropertyViews.Remove(pv));
                Entities.Notifications.Where(n => n.PropertyId.HasValue && n.PropertyId == propertyId).ToList().ForEach(n => Entities.Notifications.Remove(n));
                Entities.Properties.Where(p => p.Id == propertyId).ToList().ForEach(pp => Entities.Properties.Remove(pp));

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
