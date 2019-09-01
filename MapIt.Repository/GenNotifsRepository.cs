using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MapIt.Data;

namespace MapIt.Repository
{
    public class GenNotifsRepository : Repository<GenNotif>
    {
        public int DeleteAnyWay(Int32 notifId)
        {
            var notifObj = base.GetByKey(notifId);
            if (notifObj == null)
                return -3;

            Entities.ToObjectContext().Connection.Open();
            DbTransaction dbTrans = Entities.ToObjectContext().Connection.BeginTransaction();

            try
            {
                Entities.Notifications.Where(n => n.GenNotifId.HasValue && n.GenNotifId == notifId).ToList().ForEach(n => Entities.Notifications.Remove(n));
                Entities.GenNotifs.Where(n => n.Id == notifId).ToList().ForEach(n => Entities.GenNotifs.Remove(n));

                Entities.ToObjectContext().SaveChanges(false);
                Entities.ToObjectContext().AcceptAllChanges();
                dbTrans.Commit();

                return 1;
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbTrans.Dispose();
                dbTrans = null;
                Entities.ToObjectContext().Connection.Close();
            }
        }
    }
}
