using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class UserCreditsRepository : Repository<UserCredit>
    {
        public IQueryable<UserCredit> Search(long? userId, int? paymentMethodId, DateTime? dateFrom, DateTime? dateTo, int? paymentStatus)
        {
            return base.Find(cre =>
                (userId.HasValue ? cre.UserId == userId.Value : true) &&
                (paymentMethodId.HasValue ? cre.PaymentMethodId == paymentMethodId.Value : true) &&
               ((dateFrom.HasValue && dateTo.HasValue) ? (cre.TransOn >= dateFrom.Value && cre.TransOn < dateTo.Value) : true) &&
               (paymentStatus.HasValue ? cre.PaymentStatus == paymentStatus.Value : true));
        }

        public void SetPaymentStatus(long id, int paymentStatus)
        {
            var creObj = base.GetByKey(id);
            creObj.PaymentStatus = paymentStatus;
            base.Update(creObj);
        }
    }
}
