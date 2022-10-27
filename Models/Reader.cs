using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePaperWeb.Models
{
    public class Reader
    {
        public string call { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int userid { get; set; }
        public string token { get; set; }
    }

    public class MemberData
    {
        public int userID { get; set; }
        public string loginName { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}