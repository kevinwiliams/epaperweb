using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ePaperWeb.DBModel;
using System.Linq;
using System.Web;

namespace ePaperWeb.Models
{
    public class SubscriptionDetails
    {
        public int rateID { get; set; }
        public string subType { get; set; }

        [Display(Name = "Subscription Start Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a start date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        [Display(Name = "Promotions and newsletters")]
        public string notificationEmail { get; set; }

        [Display(Name = "Special delivery instructions")]
        public string deliveryInstructions { get; set; }

        [Display(Name = "Newsletter")]
        public int newsletterSignUp { get; set; }

        [Display(Name = "Terms & Conditions")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select terms and conditions")]
        public int termsAndCon { get; set; }

        public List<printandsubrate> RatesList { get; set; }

    }
}