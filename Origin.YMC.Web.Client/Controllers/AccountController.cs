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
using Origin.YMC.Web.Client.Models;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities;
using Origin.YMC.Business.Components.Interfaces;
using System.Net;
using Origin.YMC.Business.Entities.Domain.Interpreter.ViewModels;
using Origin.YMC.Web.Client.Helper;

namespace Origin.YMC.Web.Client.Controllers
{
    public class AccountController : Controller
    {
        protected ApplicationUserManager _userManager;
        private readonly IUserComponent _userComponent;
        private readonly IInterpreterComponent _interpreterComponent;

        public AccountController(IUserComponent userComponent, IInterpreterComponent interpreterComponent)
        {
            _userComponent = userComponent;
            _interpreterComponent = interpreterComponent;
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
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, bool SignInDoctorModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByEmail(model.Email);

                if (user != null)
                {
                    var res = UserManager.CheckPassword(user, model.Password);
                    if (res)
                    {
                      
                        var userIsDoctor = UserManager.IsInRole(user.Id, "Doctor") || UserManager.IsInRole(user.Id, "Interpreter");
                        if (SignInDoctorModel && userIsDoctor)
                            await SignInAsync(user, model.RememberMe);
                        else if (SignInDoctorModel == false && userIsDoctor == false)
                            await SignInAsync(user, model.RememberMe);
                        else
                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "you are sign in with different place , so please make sure if you are doctor select doctor tab or  select patient tab if you patient. ");
                    }
                    else
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Please check your password !");

                    return new HttpStatusCodeResult(HttpStatusCode.OK, string.Join(",", UserManager.GetRoles(user.Id).ToList()));
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Invalid username or password.");
                }
                
            }

            // If we got this far, something failed, redisplay form
          
            return RedirectToAction("index", "Home");
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
            //if (ModelState.IsValid)
            //{

            if (model.SpecialtieId == null || model.SpecialtieId == Guid.Empty)
            {
                if (!string.IsNullOrWhiteSpace(model.UserName) && !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Password))
                {
                    var user = new ApplicationUser()
                    {
                        Id = Guid.NewGuid(),
                        UserName = model.UserName,
                        Email = model.Email,
                        CreationDate = DateTime.Now,
                        LastUpdatedAt = DateTime.Now,

                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    user = await UserManager.FindByNameAsync(model.UserName);
                    result = await UserManager.AddToRoleAsync(user.Id,  AppRoles.Patient);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("PatientProfileSteps", "Home");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {

                if (!string.IsNullOrWhiteSpace(model.Name) && !string.IsNullOrWhiteSpace(model.Email)  && !string.IsNullOrWhiteSpace(model.Password))
                {
                    var user = new ApplicationUser()
                    {
                        Id = Guid.NewGuid(),
                        UserName = model.Name.Trim().Replace(" ", ""),
                        Email = model.Email,
                        CreationDate = DateTime.Now,
                        LastUpdatedAt = DateTime.Now,
                        Mobile = model.MobileNumber,
                        FirstName = model.SpecialtieId.ToString()
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    user = await UserManager.FindByNameAsync(user.UserName);
                    result = await UserManager.AddToRoleAsync(user.Id, model.IsInterpreter ? AppRoles.Interpreter : AppRoles.Doctor);
                    if (result.Succeeded)
                    {
                        if (model.IsInterpreter)
                        {
                            _interpreterComponent.Add(new InterpreterViewModels()
                            {
                                ApplicationUserId = user.Id,
                                //SpecialtyId= model.SpecialtieId,
                            });
                            
                        }
                        await SignInAsync(user, isPersistent: false);
                        if (model.IsInterpreter)
                        {
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            return RedirectToAction("DoctorProfileSteps", "Home");

                        }
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            // }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterInterpreter(RegisterViewModel model)
        {
            
           

                if (!string.IsNullOrWhiteSpace(model.Name) && !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.MobileNumber) && !string.IsNullOrWhiteSpace(model.Password))
                {
                    var user = new ApplicationUser()
                    {
                        Id = Guid.NewGuid(),
                        UserName = model.Name.Trim().Replace(" ", ""),
                        Email = model.Email,
                        CreationDate = DateTime.Now,
                        LastUpdatedAt = DateTime.Now,
                        Mobile = model.MobileNumber,
                        FirstName = model.SpecialtieId.ToString()
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    user = await UserManager.FindByNameAsync(user.UserName);
                    result = await UserManager.AddToRoleAsync(user.Id,AppRoles.Interpreter);
                    if (result.Succeeded)
                    {
                        
                            _interpreterComponent.Add(new InterpreterViewModels()
                            {
                                ApplicationUserId = user.Id,
                                //SpecialtyId = model.SpecialtieId,
                            });

                        
                        await SignInAsync(user, isPersistent: false);
                        
                            return RedirectToAction("Index", "Home");

                        
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            
            return View(model);
        }

        [AllowAnonymous]
        public JsonResult CheckUserExistance(string Email, string Phone, string UserName, string Password)
        {
            CheckUserResult result = new CheckUserResult();
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                UserName = UserName.Replace(" ", "");
                result.IsUserNameExists = (UserManager.FindByName(UserName)) != null;
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                result.IsEmailExist = (UserManager.FindByEmail(Email)) != null;
            }
            if (!string.IsNullOrWhiteSpace(Phone))
            {
                result.IsPhoneExists = UserManager.Users.Any(x => x.PhoneNumber == Phone);
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                Regex r = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$");
                result.IsPasswordGood = r.Match(Password).Success;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Logoff
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
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


        #region Remote Validation Method
        [HttpGet]
        public ActionResult IsNameExist(string userName)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    bool isExist = _userComponent.CheckNameExist(userName) != null;
                    return Json(!isExist, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(true);
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult IsEmailExist(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    bool isExist = _userComponent.EmailExist(email) != null;
                    return Json(!isExist, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(true);
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult IsMobileExist(string mobileNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    bool isExist = _userComponent.MobileExist(mobileNumber) != null;
                    return Json(!isExist, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(true);
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }
        }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    var myError = new
                    {
                        Message = "This Email not Exist",
                        Code = 500,
                    };
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "This Email not Exist");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", $"Please reset your password by using this {code}");
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Done");
            }

            // If we got this far, something failed, redisplay form
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "This Email not Exist");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var myError = new
                {
                    Message = "This Email not Exist",
                    Code = 500,
                };
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "This Email not Exist");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Done");
            else
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Join(" ", result.Errors));

        }
    }

}