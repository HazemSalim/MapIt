using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class TechMessagesRepository : Repository<TechMessage>
    {
        public IQueryable<TechMessage> Search(long? userId, DateTime? dateFrom, DateTime? dateTo)
        {
            return base.Find(tm =>
                (userId.HasValue ? tm.UserId == userId.Value : true) &&
               ((dateFrom.HasValue && dateTo.HasValue) ? (tm.AddedOn >= dateFrom.Value && tm.AddedOn < dateTo.Value) : true));
        }

        public void SetRead(Int64 userId)
        {
            var messagesList = Entities.TechMessages.Where(m => m.UserId == userId);
            if (messagesList != null && messagesList.Count() > 0)
            {
                foreach (var msg in messagesList)
                {
                    msg.IsRead = true;
                }

                Entities.SaveChanges();
            }
        }
    }
}
