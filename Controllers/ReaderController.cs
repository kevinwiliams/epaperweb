using ePaperWeb.DBModel;
using ePaperWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml;
using System.Xml.Serialization;
using static ePaperWeb.Util;

namespace ePaperWeb.Controllers
{
    public class ReaderController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(Reader))]
        public HttpResponseMessage Post([FromBody] Reader reader)
        {
            if (ModelState.IsValid && reader != null)
            {
                member mb = new member();
                error error = new error();
                var xml = "";
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
                // Convert any HTML markup in the status text.
                reader.call = HttpUtility.HtmlEncode(reader.call);
                using (var context = new Entities())
                {

                    var result = (from s in context.subscribers
                                     join a in context.subscriber_address on s.addressID equals a.addressID
                                     join e in context.subscriber_epaper on s.subscriberID equals e.subscriberID
                                  // where s.emailAddress == reader.username
                                  select new { 
                                      subscriber = s, 
                                      address = a,
                                      subscription = e
                                      }).SingleOrDefault(b => b.subscriber.emailAddress == reader.username);


                    //var subscriber = context.subscribers.SingleOrDefault(b => b.emailAddress == reader.username);
                    if (result != null) {
                        mb.userID = result.subscriber.subscriberID;
                        mb.email = result.subscriber.emailAddress;
                        mb.loginName = result.subscriber.emailAddress;
                        mb.firstname = result.subscriber.firstName;
                        mb.lastname = result.subscriber.lastName;
                        mb.homeareacode = result.subscriber.phoneNumber;
                        mb.homephone = result.subscriber.phoneNumber;
                        mb.workareacode = result.subscriber.phoneNumber;
                        mb.workphone = result.subscriber.phoneNumber;
                        mb.address = result.address.addressLine1;
                        mb.apartment = result.address.addressLine2;
                        mb.city = result.address.cityTown;
                        mb.province = result.address.stateParish;
                        mb.postalcode = result.address.zipCode;
                        mb.country = result.address.country;
                        mb.gender = String.Empty;
                        mb.nickname = String.Empty;
                        mb.subscription = "digital";
                        mb.expiration = result.subscription.endDate.ToString();


                        //serialize class to xml string
                        xml = ObjectSerializer<member>.Serialize(mb);

                    }
                    else
                    {
                        //error message
                        error.code = "04";
                        error.message = "User not found";
                        xml = ObjectSerializer<error>.Serialize(error);
                    }

                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(xml, Encoding.UTF8, "application/xml"),
                    };

                }
               
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        [NonAction]
        public string memberXML(Reader reader)
        {
            return "";
        }


        public string Get()
        {
            return "Welcome To Web API";
        }

        [HttpPost]
        public List<string> Get(int Id)
        {
            return new List<string> {
                "Data1",
                "Data2"
            };
        }
    }
}
