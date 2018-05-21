using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MailLogging.Models;

namespace MailLogging.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView lvm, string returnUrl)
        {
            var email = "ochiakao@gmail.com";
            var password = "1234567890";
            if (!this.ModelState.IsValid)
                this.ModelState.AddModelError("", "Invalid Username or Password");
            if (lvm.EmailAddress == email && lvm.Password == password)
            {
                ClaimsIdentity claims = new ClaimsIdentity("ApplicationCookie");
                claims.AddClaim(new Claim(ClaimTypes.Email, lvm.EmailAddress));
                var ctxt = this.Request.GetOwinContext();
                ctxt.Authentication.SignIn(claims);
                return Redirect(GetRedirectUrl(returnUrl));
            }

            return View();

        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }
    }
}