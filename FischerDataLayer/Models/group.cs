//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FischerDataLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class group
    {
        public group()
        {
            this.activities = new HashSet<activity>();
        }
    
        public decimal GroupRecordId { get; set; }
        public decimal CompanyRecordId { get; set; }
        public string GroupId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Substitutable { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
    
        public virtual ICollection<activity> activities { get; set; }
        public virtual company company { get; set; }
    }
}
