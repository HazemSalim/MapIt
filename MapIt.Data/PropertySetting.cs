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
    
    public partial class PropertySetting
    {
        public long Id { get; set; }
        public int TypeId { get; set; }
        public string Category { get; set; }
        public string PropertyName { get; set; }
        public Nullable<int> PropertyId { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsMondatory { get; set; }
    }
}
