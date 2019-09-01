using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;
using System.Data.Common;

namespace MapIt.Repository
{
    public class AdminUsersRepository : Repository<AdminUser>
    {
        public AdminUser GetByUserName(string username, long userId = 0)
        {
            if (string.IsNullOrEmpty(username) && username.Trim() != string.Empty)
                return null;

            return base.First(au => au.UserName == username && au.Id != userId);
        }

        public AdminUser Login(string userName, string password)
        {
            var userObj = base.First(au => au.UserName.Trim().ToLower() == userName.Trim().ToLower());
            if (userObj != null && userObj.Password == password)
            {
                return userObj;
            }

            return null;
        }

        public bool DeleteAdminUserPermissions(AdminUser adminUserEnt)
        {
            var adminUserData = Entities.AdminUsers.Include("AdminPermissions").Where(p => p.Id == adminUserEnt.Id).FirstOrDefault();
            var permData = adminUserData.AdminPermissions.ToList();
            foreach (var data in permData)
            {
                Entities.AdminPermissions.Remove(data);
            }
            return Entities.SaveChanges() > 0;
        }
    }
}
