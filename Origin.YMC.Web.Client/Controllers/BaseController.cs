using Microsoft.AspNet.Identity.Owin;
using Origin.YMC.Business.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Origin.YMC.Business.Entities.Domain.Patients.ViewModel;
using Origin.YMC.Business.Entities.Domain.Payments.ViewModel;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;
using System.IO;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Web.Client._helper;
using System.Net;
using Origin.YMC.Web.Client.Filter;

namespace Origin.YMC.Web.Client.Controllers
{
    /// <summary>
    ///  This is base controll as parent controller
    /// </summary>
    public class BaseController : Controller
    {
        protected ApplicationUserManager _userManager;

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
        private readonly IAttachmentComponent _attachmentComponent;
        public BaseController( IAttachmentComponent attachmentComponent)
        {
            _attachmentComponent = attachmentComponent;
        }

        public void UploadFile(HttpPostedFileBase fileBase, Guid ownerId, int attachmentTypeId)
        {

            var _fileName = ownerId + "_" + Path.GetFileName(fileBase.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/User_Files"), _fileName);
            var extension = Path.GetExtension(fileBase.FileName);
            fileBase.SaveAs(path);
            string fl = path.Substring(path.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string newpath = split[1];
            string imagepath = "/Content/User_Files/" + newpath;

            _attachmentComponent.Add(new Attachment
            {
                GeneratedFileName = imagepath,
                FileExtenstion = extension,
                FileSize = fileBase.ContentLength,
                AttachmentTypeId = attachmentTypeId,
                OriginalFileName = fileBase.FileName,
                OwnerId = ownerId,

            });

        }
        public string UploadFile(HttpPostedFileBase fileBase,   int attachmentTypeId)
        {

            var _fileName = Guid.NewGuid() + "_" + Path.GetFileName(fileBase.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/User_Files"), _fileName);
            var extension = Path.GetExtension(fileBase.FileName);
            fileBase.SaveAs(path);
            string fl = path.Substring(path.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string newpath = split[1];
            string imagepath = "/Content/User_Files/" + newpath;
            return imagepath;
           

        }
        public void AddAttachment(string imagepath,Guid ownerId,   int attachmentTypeId)
        {
            FileInfo file = new FileInfo( Server.MapPath(imagepath));
            _attachmentComponent.Add(new Attachment
            {
                GeneratedFileName = imagepath,
                FileExtenstion = file.Extension,
                FileSize = file.Length,
                AttachmentTypeId = attachmentTypeId,
                OriginalFileName = file.Name,
                OwnerId = ownerId,

            });



        }


        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            try
            {
                string lang = null;
                HttpCookie langCookie = Request.Cookies["culture"];
                if (langCookie != null)
                {
                    lang = langCookie.Value;
                }
                else
                {
                    var userLanguage = Request.UserLanguages;
                    var userLang = userLanguage != null ? userLanguage[0] : "";
                    if (userLang != "")
                    {
                        lang = userLang;
                    }
                    else
                    {
                        lang = LanguageManage.GetDefaultLanguage();
                    }
                }
                new LanguageManage().SetLanguage(lang);
                return base.BeginExecuteCore(callback, state);
            }
            catch (Exception ex)
            {

                return base.BeginExecuteCore(callback, state);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public void Set(String lang)
        {
            try
            {
                // Set culture to use next
                new LanguageManage().SetLanguage(lang);
                // Return to the calling URL (or go to the site's home page)
                HttpContext.Response.Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception ex)
            {

            }
        }


        public JsonResult ResponesWithMessage(HttpStatusCode statusCode, string message)
        {

            return new JsonCustomResult(statusCode) { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult ResponesWithObject<T>(HttpStatusCode statusCode, T data)
        {

            return new JsonCustomResult(statusCode) { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}