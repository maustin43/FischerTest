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
    
    public partial class jobcostlevel
    {
        public jobcostlevel()
        {
            this.potypes = new HashSet<potype>();
        }
    
        public decimal JobCostLevelRecordId { get; set; }
        public decimal CompanyRecordId { get; set; }
        public string JobCostLevelId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastModified { get; set; }
    
        public virtual company company { get; set; }
        public virtual ICollection<potype> potypes { get; set; }
    }
}
