using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BruceFoxAuthenticationServer.Models;
using BruceFoxAuthenticationServer.Controllers;
using System.Web.Http.Results;
using System.Net;
using System.Threading.Tasks;
using BruceFoxAuthenticationServer;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Moq;
using Microsoft.AspNet.Identity;

namespace BF.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Register()
        {
            RegisterBindingModel model = new RegisterBindingModel();
            model.Email = "bob@gmail.com";
            model.Password = "Az@123456";
            model.ConfirmPassword = "Az@123456";

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var passwordManager = userStore.As<IUserPasswordStore<ApplicationUser>>();
            var userManager = new ApplicationUserManager(userStore.Object);
            var accessTokenFormat = new Mock<ISecureDataFormat<AuthenticationTicket>>();
            var controller = new AccountController(userManager, accessTokenFormat.Object);
            
            var result = controller.Register(model).Result as OkNegotiatedContentResult<RegisterBindingModel>;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.Email, model.Email);
        }
    }
}
