using ePaperWeb.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePaperWeb.Models
{
    public class PrintSubRates
    {
        public List<printandsubrate> RatesList { get; set; }
        public int rateID { get; set; }
    }
}