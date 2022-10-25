using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ePaperWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Subscribe()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult LoginDetails(LoginDetailsModel data, string prevBtn, string nextBtn)
        //{
        //    if (nextBtn != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            UserModel obj = GetCustomer();
        //            obj.LoginDetails.email = data.email.ToString();
        //            obj.LoginDetails.confirm_email = data.confirm_email;
        //            obj.LoginDetails.password = data.password;
        //            obj.LoginDetails.confirm_password = data.confirm_password;
        //            return View("PersonalDetails");
        //        }
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult PersonalDetails(PersonalDetailsModel data, string prevBtn, string nextBtn)
        //{
        //    if (nextBtn != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            UserModel obj = GetCustomer();
        //            obj.PersonalDetails.fname = data.fname;
        //            obj.PersonalDetails.lname = data.lname;
        //            obj.PersonalDetails.phone = data.phone;
        //            obj.PersonalDetails.address = data.address;
        //            obj.PersonalDetails.city = data.city;
        //            obj.PersonalDetails.state = data.state;
        //            obj.PersonalDetails.zip = data.zip;

        //            return View("Su");
        //        }
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult SecurityQuestions(SecurityQuestionsModel data, string prevBtn, string nextBtn)
        //{
        //    if (nextBtn != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            UserModel obj = GetCustomer();
        //            obj.SecurityQuestions.secretQuestion = data.secretQuestion;
        //            obj.SecurityQuestions.secretAnswer = data.secretAnswer;

        //            return View("SecurityQuestions");
        //        }
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult SubscriptionInfo(SubscriptionInfoModel data, string prevBtn, string nextBtn)
        //{
        //    if (nextBtn != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            UserModel obj = GetCustomer();
        //            obj.SubscriptionInfo.subscriptionStart = data.subscriptionStart;
        //            obj.SubscriptionInfo.subscriptionEnd = data.subscriptionEnd;
        //            obj.SubscriptionInfo.newsletter = data.newsletter;
        //            obj.SubscriptionInfo.termsAndConditions = data.termsAndConditions;
        //            return View("PaymentDetails");
        //        }
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult PaymentDetails(PaymentDetailsModel data, string prevBtn, string nextBtn)
        //{
        //    if (nextBtn != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            UserModel obj = GetCustomer();
        //            obj.PaymentDetails.cardType = data.cardType;
        //            obj.PaymentDetails.cardOwnerName = data.cardOwnerName;
        //            obj.PaymentDetails.cardNumber = data.cardNumber;
        //            obj.PaymentDetails.cardCVV = data.cardCVV;
        //            obj.PaymentDetails.cardExpiration = data.cardExpiration;
        //            obj.PaymentDetails.cardAmount = data.cardAmount;
        //            return View("PaymentDetails");
        //        }
        //    }


        //    return View();
        //}


        //private UserModel GetCustomer()
        //{
        //    if (Session["customer"] == null)
        //    {
        //        Session["customer"] = new UserModel();
        //    }
        //    return (UserModel)Session["customer"];
        //}

        //private void RemoveCustomer()
        //{
        //    Session.Remove("customer");
        //}
    }
}