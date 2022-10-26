using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ePaperWeb.Models
{
    public class PaymentDetails
    {
        [Display(Name = "Name on Card")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter cardholder name")]
        public string cardOwner { get; set; }

        [Display(Name = "Type of Card")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a card type")]
        public string cardType { get; set; }

        [Display(Name = "Card Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter cardholder name")]
        public int cardNumber { get; set; }

        [Display(Name = "CVV")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter card CVV number")]
        public string cardCVV { get; set; }

        [Display(Name = "Expiration Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter card expiration date")]
        public string cardExp { get; set; }

        public int rateID;

      

    }
}