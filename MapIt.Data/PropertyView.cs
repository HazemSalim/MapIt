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
    
    public partial class PropertyView
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PropertyId { get; set; }
        public System.DateTime ViewedOn { get; set; }
    
        public virtual User User { get; set; }
        public virtual Property Property { get; set; }
    }
}
