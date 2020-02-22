using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using DDWebservice.BLL;
using DDWebservice.Models;


namespace DDWebservice.Controllers
{
   
    public class AccountController : ApiController
    {
        /// <summary>
        /// Method: Post
        /// Creates a New User
        /// </summary>
        /// <returns> JSON</returns>   
        [HttpPost]
        public IHttpActionResult Register([FromBody]RegistrationUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AccountBll.CreateAccount(user);
                var status = new MessageStatus()
                {
                    Code = "200",
                    Message = "User was Created Succesfully"
                };
                return Content(HttpStatusCode.OK,status, new JsonMediaTypeFormatter());
            }
            catch (Exception e)
            {
                var status = new MessageStatus
                {
                    Code="500",
                    Message = e.Message
                };
                return Content(HttpStatusCode.BadRequest, status, new JsonMediaTypeFormatter());
            }

        }
        /// <summary>
        /// Validate the user password against the stored password
        /// Generate token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token in Json</returns>
        [HttpPost]
         public IHttpActionResult Login([FromBody]UserLoginModel user)
        {       // If model is Invalid return failed login. 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AccountBll.Login(user))
            {
                var jwtservice = new JWTService();
                var jwtmodel = JWTModel.GetJWTContainerModel(user.Email);
                var token = jwtservice.GenerateToken(jwtmodel);
                var dict = new Dictionary<string,string>();
                dict.Add("token", token);
                return Content(HttpStatusCode.OK,dict, new JsonMediaTypeFormatter());
            }
            return Content(HttpStatusCode.BadRequest, new MessageStatus() { Code = "500", Message = "Login Failed" },
                new JsonMediaTypeFormatter());

        }

    }
}
