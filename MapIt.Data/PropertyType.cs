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
    
    public partial class PropertyType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropertyType()
        {
            this.Properties = new HashSet<Property>();
            this.WatchLists = new HashSet<WatchList>();
        }
    
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string Color { get; set; }
        public int Ordering { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WatchList> WatchLists { get; set; }
    }
}
