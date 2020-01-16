using System.Linq;
using MapIt.Data;

namespace MapIt.Repository
{
    public class PaymentTransactionsRepository : Repository<PaymentTransaction>
    {
        public IQueryable<PaymentTransaction> GetByCreditId(int creId)
        {
            return Find(opt => opt.CreditId == creId);
        }

        public PaymentTransaction GetByPaymentId(string payId)
        {
            return First(opt => opt.PaymentId == payId);
        }

        public PaymentTransaction GetByInvoiceId(string invoiceId)
        {
            return First(opt => opt.RefId == invoiceId);
        }

        public PaymentTransaction GetByTranId(string tranId)
        {
            return First(opt => opt.TranId == tranId);
        }
    }
}
