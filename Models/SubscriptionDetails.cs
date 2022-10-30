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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a plan to proceed")]
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
        public bool newsletterSignUp { get; set; }

        
        [Display(Name = "Terms & Conditions")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Please select terms and conditions")]
        [MustBeTrue(ErrorMessage = "Please select terms and conditions!")]
        //[Required(ErrorMessage = "Please select terms and conditions")]
        public bool termsAndCon { get; set; }

        public IEnumerable<printandsubrate> RatesList { get; set; }

    }

    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}