using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ePaperWeb.Models;
using ePaperWeb.DBModel;

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
            //return View();
            return View("LoginDetails");
        }

        [HttpPost]
        public ActionResult LoginDetails(LoginDetails data, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    subscriber obj = GetSubscriber();
                    obj.firstName = data.FirstName;
                    obj.lastName = data.LastName;
                    obj.emailAddress = data.EmailAddress;
                    obj.passwordHash = data.Password;
                    obj.createdAt = DateTime.Now;
                    return View("PersonalDetails");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddressDetails(AddressDetails data, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    subscriber objSub = GetSubscriber();
                    subscriber_address objAdd = GetSubscriberAddress();

                    objSub.phoneNumber = data.phone;
                    //add email from subscriber
                    objAdd.emailAddress = objSub.emailAddress;

                    objAdd.addressLine1 = data.addressLine1;
                    objAdd.addressLine2 = data.addressLine1;
                    objAdd.cityTown = data.addressLine1;
                    objAdd.stateParish = data.addressLine1;
                    objAdd.zipCode = data.addressLine1;
                    objAdd.country = data.addressLine1;
                    objAdd.createdAt = DateTime.Now;

                    return View("SubscriptionInfo");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult SubscriptionInfo(SubscriptionDetails data, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    subscriber objSub = GetSubscriber();
                    subscriber_epaper objEp = GetEpaperDetails();
                    subscriber_print objPr = GetPrintDetails();

                    objSub.newsletter = data.newsletterSignUp;



                    if(data.subType == "Print")
                    {
                        objPr.startDate = data.startDate;
                        objPr.endDate = data.endDate;
                        objPr.rateID = data.rateID;
                        objPr.emailAddress = objSub.emailAddress;
                    }
                    else
                    {
                        objEp.startDate = data.startDate;
                        objEp.endDate = data.endDate;
                        objPr.rateID = data.rateID;
                        objPr.emailAddress = objSub.emailAddress;
                    }

                    return View("PaymentDetails");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult PaymentDetails(PaymentDetails data, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    subscriber objSub = GetSubscriber();
                    subscriber_tranx objTran = GetTransaction();

                    objTran.emailAddress = objSub.emailAddress;

                    objTran.rateID = data.rateID;

                    objTran.cardType = data.cardType;
                    objTran.cardOwner = data.cardOwner;
                    objTran.cardOwner = data.cardOwner;
                    objTran.tranxDate = DateTime.Now;
                    return View("PaymentSuccess");
                }
            }


            return View();
        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }


        private subscriber GetSubscriber()
        {
            if (Session["subscriber"] == null)
            {
                Session["subscriber"] = new subscriber();
            }
            return (subscriber)Session["customer"];
        }

        private subscriber_address GetSubscriberAddress()
        {
            if (Session["subscriber_address"] == null)
            {
                Session["subscriber_address"] = new subscriber_address();
            }
            return (subscriber_address)Session["subscriber_address"];
        }

        private subscriber_epaper GetEpaperDetails()
        {
            if (Session["subscriber_epaper"] == null)
            {
                Session["subscriber_epaper"] = new subscriber_epaper();
            }
            return (subscriber_epaper)Session["subscriber_epaper"];
        }

        private subscriber_print GetPrintDetails()
        {
            if (Session["subscriber_print"] == null)
            {
                Session["subscriber_print"] = new subscriber_print();
            }
            return (subscriber_print)Session["subscriber_print"];
        }

        private subscriber_tranx GetTransaction()
        {
            if (Session["subscriber_tranx"] == null)
            {
                Session["subscriber_tranx"] = new subscriber_tranx();
            }
            return (subscriber_tranx)Session["subscriber_tranx"];
        }

        private void RemoveSubscriber()
        {
            Session.Remove("subscriber");
            Session.Remove("subscriber_address");
            Session.Remove("subscriber_epaper");
            Session.Remove("subscriber_print");
            Session.Remove("subscriber_tranx");
        }
    }
}