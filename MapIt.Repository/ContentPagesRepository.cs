using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ContentPagesRepository : Repository<ContentPage>
    {
        public void DeletePageShows(ContentPage pageEnt)
        {
            var pageData = Entities.ContentPages.Include("PageShows").Where(p => p.Id == pageEnt.Id).FirstOrDefault();
            var showData = pageData.PageShows.ToList();
            foreach (var data in showData)
            {
                Entities.PageShows.Remove(data);
            }
            Entities.SaveChanges();
        }
    }
}
