using ePaperWeb.DBModel;
using ePaperWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using static ePaperWeb.Util;
using System.Globalization;

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
                var errCode = "";
                var errMsg = "";
                subscriber result = new subscriber();

                // Convert any HTML markup in the status text.
                reader.call = HttpUtility.HtmlEncode(reader.call);
                using (var context = new Entities())
                {

                    /* var result = (from s in context.subscribers
                                   join a in context.subscriber_address on s.addressID equals a.addressID
                                   join e in context.subscriber_epaper on s.subscriberID equals e.subscriberID
                                   select new { subscriber = s, address = a, subscription = e})
                                   .SingleOrDefault(b => b.subscriber.emailAddress == reader.username);*/
                    //load data
                    var tableData = context.subscribers
                        .Include(x => x.subscriber_address)
                        .Include(x => x.subscriber_epaper);

                    if(tableData != null)
                    {
                        switch (reader.call)
                        {
                            case "authenticate":
                                result = tableData.SingleOrDefault(b => b.emailAddress == reader.username && b.passwordHash == reader.password);
                                errCode = "03";
                                errMsg = "Invalid credentials";
                                break;
                            case "get_user_by_userid":
                                result = tableData.SingleOrDefault(b => b.subscriberID == reader.userid);
                                errCode = "04";
                                errMsg = "User not found";
                                break;
                            case "get_user_by_token":
                                result = tableData.SingleOrDefault(b => b.token == reader.token);
                                errCode = "05";
                                errMsg = "Invalid token";
                                break;
                            default:
                                break;
                        }
                    }
                    

                    //load object with database results
                    if (result != null) {
                        xml = MemberXML(mb, result);
                    }
                    else
                    {
                        xml = ErrorXML(errCode, errMsg);
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
        public string MemberXML(member mb, subscriber result)
        {
            var xml = "";
           

            try
            {
                //load object with database results
                if (result != null)
                {
                    mb.userID = result.subscriberID;
                    mb.email = result.emailAddress;
                    mb.loginName = result.emailAddress;
                    mb.firstname = result.firstName;
                    mb.lastname = result.lastName;
                    mb.homeareacode = result.phoneNumber;
                    mb.homephone = result.phoneNumber;
                    mb.workareacode = String.Empty;
                    mb.workphone = String.Empty;
                    mb.address = result.subscriber_address.addressLine1;
                    mb.apartment = result.subscriber_address.addressLine2;
                    mb.city = result.subscriber_address.cityTown;
                    mb.province = result.subscriber_address.stateParish;
                    mb.postalcode = result.subscriber_address.zipCode;
                    mb.country = result.subscriber_address.country;
                    mb.gender = String.Empty;
                    mb.nickname = String.Empty;
                    mb.subscription = "digital";

                    var dateTime = result.subscriber_epaper.FirstOrDefault(x => x.isActive == 1).endDate.ToString();
                    DateTime dt = DateTime.ParseExact(dateTime, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    mb.expiration = dt.ToString("yyyy-MM-dd");


                    //serialize class to xml string
                    xml = ObjectSerializer<member>.Serialize(mb);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return xml;
        }

        public string ErrorXML(string code, string msg) 
        {
            error error = new error
            {
                code = code,
                message = msg
            };
            var xml = ObjectSerializer<error>.Serialize(error);

            return xml;
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
