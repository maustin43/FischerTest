//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FischerLib.Logging
{
    using System;
    using System.Collections.Generic;
    
    public partial class log
    {
        public decimal LogRecordId { get; set; }
        public string CompanyId { get; set; }
        public string ApplicationName { get; set; }
        public string CallerFile { get; set; }
        public Nullable<int> CallerMethodLine { get; set; }
        public string CallerMethodName { get; set; }
        public string Exception { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string LogCallerFile { get; set; }
        public Nullable<int> LogCallerLine { get; set; }
        public string LogCallerMethodName { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public string LogServer { get; set; }
        public string LogUser { get; set; }
        public string Message { get; set; }
        public Nullable<int> MessageType { get; set; }
        public string ObjectType { get; set; }
        public string ObjectData { get; set; }
    }
}
