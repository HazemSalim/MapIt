using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PaymentTransactionsRepository : Repository<PaymentTransaction>
    {
        public IQueryable<PaymentTransaction> GetByCreditId(Int64 creId)
        {
            return base.Find(opt => opt.CreditId == creId);
        }

        public PaymentTransaction GetByPaymentId(String payId)
        {
            return base.First(opt => opt.PaymentId == payId);
        }

        public PaymentTransaction GetByTranId(String tranId)
        {
            return base.First(opt => opt.TranId == tranId);
        }
    }
}
