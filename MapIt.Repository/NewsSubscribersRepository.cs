using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class NewsSubscribersRepository : Repository<NewsSubscriber>
    {
        public NewsSubscriber GetByEmail(string email, long userId = 0)
        {
            if (string.IsNullOrEmpty(email) && email.Trim() != string.Empty)
                return null;

            return base.First(u => u.Email == email && u.Id != userId);
        }
    }
}
