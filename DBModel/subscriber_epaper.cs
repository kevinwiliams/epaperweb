//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ePaperWeb.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class subscriber_epaper
    {
        public int epaperID { get; set; }
        public Nullable<int> subscriberID { get; set; }
        public string emailAddress { get; set; }
        public string token { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<int> isActive { get; set; }
        public string subType { get; set; }
        public Nullable<System.DateTime> createdAt { get; set; }
        public string notificationEmail { get; set; }
    }
}
