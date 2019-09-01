using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PropertySettingsRepository : Repository<PropertySetting>
    {
        public bool Save(int propertyTypeId, string settings)
        {
            var result = Entities.SavePropertyTypeSettings(propertyTypeId, settings);
            if (result > 0)
                return true;

            return false;
        }

        public List<GetSettings_Result> GetSettings(int propertyTypeId = 0)
        {
            return Entities.GetSettings(propertyTypeId).ToList();
        }

        public List<PropertySetting> GetSettingsList(int typeId)
        {
            var settingsList = base.Find(s => s.TypeId == typeId && s.IsAvailable).ToList();

            if (settingsList == null)
                settingsList = new List<PropertySetting>();

            return settingsList;
        }
    }
}
