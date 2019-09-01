using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class AdminPermissionsRepository : Repository<AdminPermission>
    {
        public bool GetByPageId(Int32 adminUserId, Int32 pageId)
        {
            if (adminUserId > 1)
            {
                var permObj = base.First(p => p.AdminUserId == adminUserId && p.AdminPageId == pageId);
                if (permObj != null)
                {
                    return permObj.IsAccessible;
                }
                return false;
            }
            return true;
        }
    }
}
