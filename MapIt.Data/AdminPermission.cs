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
    
    public partial class AdminPermission
    {
        public int Id { get; set; }
        public int AdminUserId { get; set; }
        public int AdminPageId { get; set; }
        public bool IsAccessible { get; set; }
    
        public virtual AdminPage AdminPage { get; set; }
        public virtual AdminUser AdminUser { get; set; }
    }
}
