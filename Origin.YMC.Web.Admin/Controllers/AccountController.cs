using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Origin.YMC.Web.Admin.Models;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities;
using Origin.YMC.Business.Components.Interfaces;

namespace Origin.YMC.Web.Admin.Controllers
{
    public class AccountController : Controller
    {
        protected ApplicationUserManager _userManager;
       

        public AccountController()
        {
           
        }

       

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        #region Login
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);

                    switch (user.Roles.FirstOrDefault().Role.Name)
                    {
                        case AppRoles.Admin:
                            return RedirectToAction("Index", "Home");
                            
                        

                    }  

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Add Default Admin
        public async Task<ActionResult> AddDefaultAdmin()
        {
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                UserName = "YMC_admin@admin.com",
                Email = "YMC_admin@admin.com",
                CreationDate = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                BirthDate = DateTime.Now,
                FirstName = "YMC admin",
                LastName = "YMC admin",
                PhoneNumber = "0",
                Grade = 1,
                CityId = Guid.Parse("A8859A35-0898-4DDB-9E32-FAE5867C064A"),
                GenderEnum = GenderEnum.Male
            };

            var result = await UserManager.CreateAsync(user, "P@ssw0rd");
            user = await UserManager.FindByNameAsync("YMC_admin@admin.com");
            result = await UserManager.AddToRoleAsync(user.Id, AppRoles.Admin);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion

        #region Register
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.UserName,
                    CreationDate = DateTime.Now,
                    LastUpdatedAt = DateTime.Now,
                    BirthDate = DateTime.Now
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                user = await UserManager.FindByNameAsync(model.UserName);
                result = await UserManager.AddToRoleAsync(user.Id, AppRoles.Admin);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Logoff
        //
        // POST: /Account/LogOff
        [HttpGet]

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Teacher");
            }
        }

        #endregion

    }

}