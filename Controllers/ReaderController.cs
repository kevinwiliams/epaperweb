using ePaperWeb.DBModel;
using ePaperWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

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
                MemberData mb = new MemberData();

                // Convert any HTML markup in the status text.
                reader.call = HttpUtility.HtmlEncode(reader.call);
                using (var context = new Entities())
                {
                    var subscriber = context.subscribers.SingleOrDefault(b => b.emailAddress == reader.username);
                    if (subscriber != null) {
                        mb.userID = subscriber.subscriberID;
                        mb.loginName = subscriber.emailAddress;
                        mb.firstname = subscriber.firstName;
                        mb.lastname = subscriber.lastName;

                    }
                }

                //var response = new HttpResponseMessage(HttpStatusCode.Created);
                var response =  Request.CreateResponse(HttpStatusCode.Created, mb);

                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            // return reader.call;
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
