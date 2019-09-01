using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class ServiceCommentsRepository : Repository<ServiceComment>
    {
        public IQueryable<ServiceComment> GetByServiceId(Int64 serviceId)
        {
            return base.Find(c => c.ServiceId == serviceId);
        }

        public IQueryable<ServiceComment> GetByReceiverId(Int64 serviceId, Int64 receiverId)
        {
            var messagesList = Entities.ServiceComments.Where(m => m.ServiceId == serviceId && (m.ReceiverId == receiverId || m.SenderId == receiverId))
                .OrderByDescending(x => x.AddedOn).AsQueryable().GroupBy(m => m.SenderId | m.ReceiverId).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.AddedOn).AsQueryable();

            return messagesList;
        }

        public IQueryable<ServiceComment> GetCommentsBetween(Int64 serviceId, Int64 senderId, Int64 receiverId)
        {
            var commentsList = Entities.ServiceComments.Where(c => c.ServiceId == serviceId && c.ReceiverId == senderId && c.SenderId == receiverId);
            if (commentsList != null && commentsList.Count() > 0)
            {
                foreach (var item in commentsList)
                {
                    item.IsRead = true;
                }

                Entities.SaveChanges();
            }

            return Find(c => c.ServiceId == serviceId &&
                ((c.SenderId == senderId && c.ReceiverId == receiverId) || (c.SenderId == receiverId && c.ReceiverId == senderId)));
        }
    }
}
