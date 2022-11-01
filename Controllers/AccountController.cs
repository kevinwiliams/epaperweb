using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePaperWeb.Models;
using ePaperWeb.DBModel;
using static ePaperWeb.Util;
using System.Globalization;

namespace ePaperWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("LoginDetails");
        }

        [AllowAnonymous]
        public ActionResult Subscribe()
        {
            //Test Data
            LoginDetails ld = new LoginDetails
            {
                FirstName = "Dwayne",
                LastName = "Mendez",
                EmailAddress = "dwayne.mendez@live.net",
            };
            return View("LoginDetails", ld);
        }

        [HttpPost]
        public ActionResult LoginDetails(LoginDetails data, string prevBtn, string nextBtn)
        {
           if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {

                    var isExist = IsEmailExist(data.EmailAddress);
                    if (isExist)
                    {
                        ModelState.AddModelError("EmailExist", "Email address is already assigned. Please use forget password option to log in");
                        return View(data);
                    }

                    subscriber obj = GetSubscriber();
                    obj.firstName = data.FirstName;
                    obj.lastName = data.LastName;
                    obj.emailAddress = data.EmailAddress;
                    obj.passwordHash = PasswordHash(data.Password);
                    obj.createdAt = DateTime.Now;
                    obj.ipAddress = Request.UserHostAddress;

                    //Test Data
                    AddressDetails ad = new AddressDetails
                    {
                        addressLine1 = "Lot 876 Scheme Steet",
                        cityTown = "Bay Town",
                        stateParish = "Portland",
                        country = "Jamaica",
                        zipCode = "JAMWI",
                        phone = "876-875-8651"
                    };

                    return View("AddressDetails", ad);
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
                    UserLocation objLoc = GetSubscriberLocation();

                    var market = (objLoc.Country_Code == "JM") ? "Local" : "International";
                    
                    objSub.phoneNumber = data.phone;
                    //add email from subscriber
                    objAdd.emailAddress = objSub.emailAddress;
                    //address type M - Mailing --- B - Billing
                    objAdd.addressType = "M";
                    objAdd.addressLine1 = data.addressLine1;
                    objAdd.addressLine2 = data.addressLine2;
                    objAdd.cityTown = data.cityTown;
                    objAdd.stateParish = data.stateParish;
                    objAdd.zipCode = data.zipCode;
                    objAdd.country = data.country;
                    objAdd.createdAt = DateTime.Now;

                    //load rates on the next (subscription) page
                    Entities db = new Entities();
                    SubscriptionDetails subscriptionDetails = new SubscriptionDetails 
                    {
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddDays(30),
                        RatesList = db.printandsubrates
                            .Where(x => x.Market == market)
                            .Where(x => x.Active == 1).ToList(),
                    };
                    

                    return View("SubscriptionInfo", subscriptionDetails);
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult SubscriptionInfo(SubscriptionDetails data, string prevBtn, string nextBtn)
        {
            Entities db = new Entities();

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
                    subscriber_tranx objTran = GetTransaction();

                    objSub.newsletter = data.newsletterSignUp;
                    objTran.rateID = data.rateID;

                    var selectedPlan = db.printandsubrates.FirstOrDefault(x => x.rateid == data.rateID);

                    if (selectedPlan.Type == "Print")
                    {
                        objPr.startDate = data.startDate;
                        objPr.endDate = data.startDate.AddDays(30);
                        objPr.rateID = data.rateID;
                        objPr.isActive = true;
                        objPr.emailAddress = objSub.emailAddress;
                        objPr.deliveryInstructions = data.deliveryInstructions;
                        objPr.createdAt = DateTime.Now;

                    }
                    if (selectedPlan.Type == "Epaper")
                    {
                        objEp.startDate = data.startDate;
                        objEp.endDate = data.startDate.AddDays((double)selectedPlan.ETerm);
                        objEp.rateID = data.rateID;
                        objEp.subType = data.subType;
                        objEp.isActive = true;
                        objEp.emailAddress = objSub.emailAddress;
                        objEp.notificationEmail = data.notificationEmail;
                        objEp.createdAt = DateTime.Now;
                    }
                    if (selectedPlan.Type == "Bundle")
                    {
                        //print subscription
                        objPr.startDate = data.startDate;
                        objPr.endDate = data.startDate.AddDays(30);
                        objPr.rateID = data.rateID;
                        objPr.isActive = true;
                        objPr.emailAddress = objSub.emailAddress;
                        objPr.deliveryInstructions = data.deliveryInstructions;
                        objPr.createdAt = DateTime.Now;

                        //Epaper subscription
                        objEp.startDate = data.startDate;
                        objEp.endDate = data.startDate.AddDays((double)selectedPlan.ETerm);
                        objEp.rateID = data.rateID;
                        objEp.subType = data.subType;
                        objEp.isActive = true;
                        objEp.emailAddress = objSub.emailAddress;
                        objEp.notificationEmail = data.notificationEmail;
                        objEp.createdAt = DateTime.Now;
                    }

                    //Test Data
                    PaymentDetails pd = new PaymentDetails
                    {
                        cardOwner = "Dwayne Mendez",
                    };

                    return View("PaymentDetails", pd);
                }
            }

            UserLocation objLoc = GetSubscriberLocation();
            var market = (objLoc.Country_Code == "JM") ? "Local" : "International";
            
            SubscriptionDetails subscriptionDetails = new SubscriptionDetails 
            {
                startDate = DateTime.Now,
                RatesList = db.printandsubrates
                                .Where(x => x.Market == market)
                                .Where(x => x.Active == 1).ToList(),

            };
            

            return View(subscriptionDetails);
        }


        [HttpPost]
        public ActionResult PaymentDetails(PaymentDetails data, string prevBtn, string nextBtn)
        {

            var cardType = data.cardType;

            if (prevBtn != null)
            {
                Entities db = new Entities();
                subscriber objSub = GetSubscriber();
                subscriber_epaper objEp = GetEpaperDetails();
                subscriber_print objPr = GetPrintDetails();
                UserLocation objLoc = GetSubscriberLocation();
                var market = (objLoc.Country_Code == "JM") ? "Local" : "International";


                SubscriptionDetails sd = new SubscriptionDetails
                {
                    startDate = objEp.startDate,
                    rateID = objEp.rateID,
                    deliveryInstructions = objPr.deliveryInstructions,
                    newsletterSignUp = objSub.newsletter ?? false,
                    notificationEmail = objEp.notificationEmail,
                    subType = objEp.subType,
                    RatesList = db.printandsubrates.Where(x => x.Market == market).Where(x => x.Active == 1).ToList()
            };

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
                    var rateID = objTran.rateID;

                    //save to DB
                    using (var context = new Entities())
                    {
                        //save subscribers
                        objSub.isActive = true;
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
                        var selectedPlan = context.printandsubrates.SingleOrDefault(b => b.rateid == rateID);
                        if (selectedPlan != null)
                        {
                            switch (selectedPlan.Type)
                            {
                                case "Print":
                                    //save print subscription
                                    objP.addressID = addressID;
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
                                    //save print subscription
                                    objP.addressID = addressID;
                                    objP.subscriberID = subscriberID;
                                    context.subscriber_print.Add(objP);

                                    //save epaper subscription
                                    objE.subscriberID = subscriberID;
                                    context.subscriber_epaper.Add(objE);

                                    context.SaveChanges();
                                    break;

                                default:
                                    break;
                            }

                            //save transaction
                            objTran.emailAddress = objSub.emailAddress;
                            objTran.cardType = data.cardType;
                            objTran.cardOwner = data.cardOwner;
                            objTran.tranxAmount = selectedPlan.Rate;
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


        [NonAction]
        public bool IsEmailExist(string emailAddress)
        {
            using (Entities dc = new Entities())
            {
                var v = dc.subscribers.Where(a => a.emailAddress == emailAddress).FirstOrDefault();
                return v != null;
            }
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

        private UserLocation GetSubscriberLocation()
        {
            if (Session["subscriber_location"] == null)
            {
                Session["subscriber_location"] = new UserLocation();
            }
            return (UserLocation)Session["subscriber_location"];
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