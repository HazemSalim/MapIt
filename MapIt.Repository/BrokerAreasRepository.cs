using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class BrokerAreasRepository : Repository<BrokerArea>
    {
        public IQueryable<BrokerArea> GetByBrokerId(int brokerId)
        {
            return base.Find(ba => ba.BrokerId == brokerId);
        }
    }
}
