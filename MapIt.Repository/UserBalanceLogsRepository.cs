using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class UserBalanceLogsRepository : Repository<UserBalanceLog>
    {
        public IQueryable<UserBalanceLog> Search(long? userId, DateTime? dateFrom, DateTime? dateTo)
        {
            return base.Find(cre =>
                (userId.HasValue ? cre.UserId == userId.Value : true) &&
               ((dateFrom.HasValue && dateTo.HasValue) ? (cre.TransOn >= dateFrom.Value && cre.TransOn < dateTo.Value) : true));
        }
    }
}
