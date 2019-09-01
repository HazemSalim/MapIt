using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class GeneralSettingsRepository : Repository<GeneralSetting>
    {
        public GeneralSetting Get()
        {
            if (Count() > 0)
                return base.GetAll().OrderByDescending(l => l.Id).FirstOrDefault();
            return null;
        }
    }
}
