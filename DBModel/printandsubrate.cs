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
    
    public partial class printandsubrate
    {
        public int rateid { get; set; }
        public string Market { get; set; }
        public string Type { get; set; }
        public string RateDescr { get; set; }
        public string PrintDayPattern { get; set; }
        public Nullable<int> PrintTerm { get; set; }
        public string PrintTermUnit { get; set; }
        public string EDayPattern { get; set; }
        public Nullable<int> ETerm { get; set; }
        public string ETermUnit { get; set; }
        public string Curr { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> Active { get; set; }
    }
}
