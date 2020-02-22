using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDWebservice.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using DDWebservice.Models;
using DDWebserviceTests;

namespace DDWebservice.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void Test_User_Is_Registered()
        {
            AccountController controller = new AccountController();
            string randomchar = Util.GenerateRandomChar();
            RegistrationUserModel model = new RegistrationUserModel();
            model.Email = randomchar + "@gmail.com";
            model.FamilyName = randomchar;
            model.FirstName = randomchar;
            model.Password = randomchar;
            IHttpActionResult result = controller.Register(model);
            var contentresult = result as FormattedContentResult<MessageStatus>;
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentresult.Content.Code);
            Assert.IsNotNull(contentresult.Content.Message);
            Assert.AreEqual("200",contentresult.Content.Code);
        }

        [TestMethod]
        public void Test_user_Login()
        {
            AccountController controller = new AccountController();
            string randomchar = Util.GenerateRandomChar();
            RegistrationUserModel model = new RegistrationUserModel();
            model.Email = randomchar + "@gmail.com";
            model.FamilyName = randomchar;
            model.FirstName = randomchar;
            model.Password = randomchar;
            controller.Register(model);
            var controller2 = CreatePost("account");
            UserLoginModel loginModel = new UserLoginModel {Email = model.Email, Password = model.Password};
            var result2 = controller2.Login(loginModel);
            var contentresult = result2 as FormattedContentResult<Dictionary<string,string>>;
            Assert.IsNotNull(result2);
            Assert.IsNotNull(contentresult.Content);
            //token not null
            Assert.IsNotNull(contentresult.Content["token"]);

        }

        [TestMethod]
        public void Test_wrong_credentials()
        {
            var controller = CreatePost("login");
            UserLoginModel loginModel = new UserLoginModel { Email = "me@gmail.com", Password =Util.GenerateRandomChar() };
            var result2 = controller.Login(loginModel);
            var contentresult = result2 as FormattedContentResult<MessageStatus>;
            Assert.AreEqual("500",contentresult.Content.Code);
            
        }

        public AccountController CreatePost(string path)
        {
            AccountController controller2 = new AccountController();
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/account/"+path);
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "account" } });

            controller2.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller2.Request = request;
            controller2.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            return controller2;
        }
    }
}