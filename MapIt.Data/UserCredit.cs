//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapIt.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserCredit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserCredit()
        {
            this.PaymentTransactions = new HashSet<PaymentTransaction>();
        }
    
        public long Id { get; set; }
        public string TransNo { get; set; }
        public long UserId { get; set; }
        public Nullable<int> PackageId { get; set; }
        public int PaymentMethodId { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public int CurrencyId { get; set; }
        public double ExchangeRate { get; set; }
        public double Amount { get; set; }
        public int PaymentStatus { get; set; }
        public System.DateTime TransOn { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Package Package { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; }
        public virtual User User { get; set; }
    }
}
