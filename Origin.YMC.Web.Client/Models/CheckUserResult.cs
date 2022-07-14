using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Origin.YMC.Web.Client.Models
{
    public class CheckUserResult
    {
        public bool IsEmailExist { get; set; }
        public bool IsPhoneExists { get; set; }
        public bool IsUserNameExists { get; set; }
        public bool IsPasswordGood { get; set; }
    }
}