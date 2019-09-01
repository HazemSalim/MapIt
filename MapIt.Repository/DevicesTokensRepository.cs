using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class DevicesTokensRepository : Repository<DevicesToken>
    {
        public bool IncreasePushCounter(string dTDvTkn)
        {
            var dtObj = base.Single(c => c.DeviceToken == dTDvTkn);
            dtObj.PushCounter++;
            return Entities.SaveChanges() > 0;
        }
        public bool ResetPushCounter(string dTDvTkn)
        {
            var dtObj = base.Single(c => c.DeviceToken == dTDvTkn);
            dtObj.PushCounter = 0;
            return Entities.SaveChanges() > 0;
        }

        public Boolean ExistsToken(String dTDvTkn)
        {
            return base.Any(dT => dT.DeviceToken.Equals(dTDvTkn));
        }

        public Boolean ExistsDvId(String dTDvId)
        {
            return base.Any(dT => dT.DeviceId.Equals(dTDvId));
        }
    }
}
