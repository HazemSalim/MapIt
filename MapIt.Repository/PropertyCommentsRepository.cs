using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertyCommentsRepository : Repository<PropertyComment>
    {
        public IQueryable<PropertyComment> GetByPropertyId(Int64 propertyId)
        {
            return base.Find(c => c.PropertyId == propertyId);
        }

        public IQueryable<PropertyComment> GetByReceiverId(Int64 propertyId, Int64 receiverId)
        {
            var messagesList = Entities.PropertyComments.Where(m => m.PropertyId == propertyId && (m.ReceiverId == receiverId || m.SenderId == receiverId))
                .OrderByDescending(x => x.AddedOn).AsQueryable().GroupBy(m => m.SenderId | m.ReceiverId).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.AddedOn).AsQueryable();

            return messagesList;
        }

        public IQueryable<PropertyComment> GetCommentsBetween(Int64 propertyId, Int64 senderId, Int64 receiverId)
        {
            var commentsList = Entities.PropertyComments.Where(c => c.PropertyId == propertyId && c.ReceiverId == senderId && c.SenderId == receiverId);
            if (commentsList != null && commentsList.Count() > 0)
            {
                foreach (var item in commentsList)
                {
                    item.IsRead = true;
                }

                Entities.SaveChanges();
            }

            return Find(c => c.PropertyId == propertyId &&
                ((c.SenderId == senderId && c.ReceiverId == receiverId) || (c.SenderId == receiverId && c.ReceiverId == senderId)));
        }
    }
}
