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
    
    public partial class ServiceArea
    {
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public int AreaId { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Service Service { get; set; }
    }
}
