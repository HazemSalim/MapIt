using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class CommercialAdsRepository : Repository<CommercialAd>
    {
        public IQueryable<CommercialAd> Search(int? placeId, int? countryId, DateTime? dateFrom, DateTime? dateTo, string keyword)
        {
            return base.Find(ad =>
                (placeId.HasValue ? ad.CommAdPlaceId == placeId.Value : true) &&
                (countryId.HasValue ? ad.CountryId == countryId.Value : true) &&
                ((dateFrom.HasValue && dateTo.HasValue) ? (ad.AddedOn >= dateFrom.Value && ad.AddedOn < dateTo.Value) : true) &&
                ((!string.IsNullOrEmpty(keyword) ? ad.Title.Replace(" ", string.Empty).ToLower().IndexOf(keyword.Replace(" ", string.Empty).ToLower()) > -1 : true) ||
                (!string.IsNullOrEmpty(keyword) ? ad.CommAdPlace.Title.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                (!string.IsNullOrEmpty(keyword) ? ad.Country.TitleEN.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                (!string.IsNullOrEmpty(keyword) ? ad.Country.TitleAR.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)));
        }
    }
}
