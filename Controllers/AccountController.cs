using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ePaperWeb.Models;
using ePaperWeb.DBModel;
using System.Dynamic;

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
                    obj.ipAddress = Request.UserHostAddress;
                    return View("AddressDetails");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddressDetails(AddressDetails data, string prevBtn, string nextBtn)
        {
            
            if (prevBtn != null)
            {
                subscriber obj = GetSubscriber();
                LoginDetails ld = new LoginDetails 
                { 
                    FirstName = obj.firstName,
                    LastName = obj.lastName,
                    EmailAddress = obj.emailAddress
                };

                return View("LoginDetails", ld);
            }

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
                    objAdd.addressLine2 = data.addressLine2;
                    objAdd.cityTown = data.cityTown;
                    objAdd.stateParish = data.stateParish;
                    objAdd.zipCode = data.zipCode;
                    objAdd.country = data.country;
                    objAdd.createdAt = DateTime.Now;

                    return View("SubscriptionInfo");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult SubscriptionInfo(SubscriptionDetails data, string prevBtn, string nextBtn)
        {
            //dynamic printSubRateModel = new ExpandoObject();
            Entities db = new Entities();

            PrintSubRates psr = new PrintSubRates
            {
                RatesList = db.printandsubrates.ToList(),
                rateID = 0
            };

            //printSubRateModel.PrintSubRates = psr;
            //printSubRateModel.data = data;


            if (prevBtn != null)
            {
                subscriber_address objAdd = GetSubscriberAddress();
                AddressDetails ad = new AddressDetails
                {
                    addressLine1 = objAdd.addressLine1,
                    addressLine2 = objAdd.addressLine2,
                    cityTown = objAdd.cityTown,
                    stateParish = objAdd.stateParish,
                    zipCode = objAdd.zipCode,
                    country = objAdd.country
                };

                return View("AddressDetails", ad);
            }

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    subscriber objSub = GetSubscriber();
                    subscriber_epaper objEp = GetEpaperDetails();
                    subscriber_print objPr = GetPrintDetails();

                    objSub.newsletter = data.newsletterSignUp;

                    if (data.subType == "Print")
                    {
                        objPr.startDate = data.startDate;
                        objPr.endDate = data.endDate;
                        objPr.rateID = data.rateID;
                        objPr.emailAddress = objSub.emailAddress;
                        objPr.deliveryInstructions = data.deliveryInstructions;
                        objPr.createdAt = DateTime.Now;

                    }
                    else
                    {
                        objEp.startDate = data.startDate;
                        objEp.endDate = data.endDate;
                        objEp.rateID = data.rateID;
                        objEp.subType = data.subType;
                        objEp.isActive = 1;
                        objEp.emailAddress = objSub.emailAddress;
                        objEp.notificationEmail = data.notificationEmail;
                        objEp.createdAt = DateTime.Now;
                    }

                    return View("PaymentDetails");
                }
            }
            
            return View(psr);
        }


        [HttpPost]
        public ActionResult PaymentDetails(PaymentDetails data, string prevBtn, string nextBtn)
        {
            subscriber_epaper objEp = GetEpaperDetails();
            subscriber_print objPr = GetPrintDetails();
            if (prevBtn != null)
            {
                SubscriptionDetails sd = new SubscriptionDetails();
                sd.rateID = objPr.rateID;
                sd.rateID = objEp.rateID;
                return View("SubscriptionInfo", sd);
            }

            

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    //get all session variables
                    subscriber objSub = GetSubscriber();
                    subscriber_address objAdd = GetSubscriberAddress();
                    subscriber_epaper objE = GetEpaperDetails();
                    subscriber_print objP = GetPrintDetails();
                    subscriber_tranx objTran = GetTransaction();

                    int subscriberID = 0;
                    int addressID = 0;
                    //TODO: better implementation
                    int rateID = (objE.rateID > 0) ? objE.rateID : objP.rateID;

                    //save to DB
                    using (var context = new Entities())
                    {
                        //save subscribers
                        objSub.isActive = 1;
                        context.subscribers.Add(objSub);
                        context.SaveChanges();

                        //get Subscriber ID
                        subscriberID = objSub.subscriberID;

                        //save address
                        objAdd.subscriberID = subscriberID;
                        context.subscriber_address.Add(objAdd);
                        context.SaveChanges();

                        //get Address ID
                        addressID = objAdd.addressID;

                        //update subscribers table w/ address ID
                        var result = context.subscribers.SingleOrDefault(b => b.subscriberID == subscriberID);
                        if (result != null)
                        {
                            result.addressID = addressID;
                            context.SaveChanges();
                        }
                        //save based on subscription
                        var subType = context.printandsubrates.SingleOrDefault(b => b.rateid == rateID);
                        if (subType != null)
                        {

                            switch (subType.Type)
                            {
                                case "Print":
                                    //save print subscription
                                    objP.subscriberID = subscriberID;
                                    context.subscriber_print.Add(objP);
                                    context.SaveChanges();
                                    break;

                                case "Epaper":
                                    //save epaper subscription
                                    objE.subscriberID = subscriberID;
                                    context.subscriber_epaper.Add(objE);
                                    context.SaveChanges();
                                    break;

                                case "Bundle":
                                    break;
                                default:
                                    break;
                            }

                            //save transaction
                            objTran.emailAddress = objSub.emailAddress;
                            objTran.cardType = data.cardType;
                            objTran.cardOwner = data.cardOwner;
                            objTran.tranxAmount = subType.Rate;
                            objTran.tranxDate = DateTime.Now;
                            objTran.subscriberID = subscriberID;
                            objTran.ipAddress = Request.UserHostAddress;
                            context.subscriber_tranx.Add(objTran);
                            context.SaveChanges();
                        }
                    }

                    RemoveSubscriber();

                    



                    return View("PaymentSuccess");
                }
            }


            return View();
        }

        [HttpPost]
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
            return (subscriber)Session["subscriber"];
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