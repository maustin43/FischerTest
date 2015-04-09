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
    
    public partial class activity
    {
        public decimal ActivityRecordId { get; set; }
        public decimal CompanyRecordId { get; set; }
        public string ActivityId { get; set; }
        public Nullable<long> AlertType { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public Nullable<long> Duration { get; set; }
        public string eMailDescription { get; set; }
        public Nullable<bool> GLOnly { get; set; }
        public decimal GroupRecordId { get; set; }
        public Nullable<long> LeadTime { get; set; }
        public decimal POTypeRecordId { get; set; }
        public Nullable<decimal> ProductionStatusRecordId { get; set; }
        public Nullable<bool> PunchOut { get; set; }
        public Nullable<decimal> RegionRecordId { get; set; }
        public Nullable<bool> ReserveUseOnly { get; set; }
        public Nullable<bool> SchedulingOnly { get; set; }
        public Nullable<bool> ShowOnCorpReports { get; set; }
        public Nullable<bool> SpecialTax { get; set; }
        public decimal StageRecordId { get; set; }
        public Nullable<bool> SubmitNotValidated { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
    
        public virtual company company { get; set; }
        public virtual group group { get; set; }
        public virtual potype potype { get; set; }
        public virtual stage stage { get; set; }
    }
}
