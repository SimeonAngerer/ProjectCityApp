//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Follower
    {
        public System.Guid PK_FollowerID { get; set; }
        public System.Guid FK_CompanyID { get; set; }
        public System.Guid FK_CustomerID { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
