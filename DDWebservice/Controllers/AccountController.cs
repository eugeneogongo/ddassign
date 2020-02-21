using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace DDWebservice.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public string Register()
        {
            return "Hello world";
        }
    }
}
