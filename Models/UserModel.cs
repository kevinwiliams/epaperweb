using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ePaperWeb.Models
{
    public class UserModel
    {
        public int recid { get; set; }
        
        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "Email address is required")]
        public string email { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        public string fname { get; set; }
        
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string lname { get; set; }
        

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        public string confirm_password { get; set; }

        //public string token { get; set; }

        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        public string phone { get; set; }

        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        public string address { get; set; }

        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        public string city { get; set; }

        [Display(Name = "State")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        public string state { get; set; }

        [Display(Name = "Province")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        public string province { get; set; }
        public int? zip { get; set; }

        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        public string country { get; set; }
        public System.DateTime subscriptionStart { get; set; }
        public System.DateTime subscriptionEnd { get; set; }
        //public bool active { get; set; }
        public bool newsletter { get; set; }
        public decimal cardAmount { get; set; }
        public string cardType { get; set; }
        public string cardOwnerName { get; set; }
        public string orderId { get; set; }
        public string secretQuestion { get; set; }
        public string secretAnswer { get; set; }
        public DateTime? transactionDate { get; set; }
        public string ip { get; set; }
    }
}