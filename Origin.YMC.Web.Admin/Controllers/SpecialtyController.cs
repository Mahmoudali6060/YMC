using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Origin.YMC.Business.Components.Implementation;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Components.ViewModels;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Business.Entities.Domain.Specialties.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Origin.YMC.Web.Admin.Controllers
{
    public class SpecialtyController : OriginBaseController
    {
        private readonly ISpecialtyComponent _specialtyComponent;
        private readonly IAttachmentComponent _attachmentComponent;
        public SpecialtyController(ISpecialtyComponent specialtyComponent,
                                   IAttachmentComponent attachmentComponent)
        {
            _specialtyComponent = specialtyComponent;
            _attachmentComponent = attachmentComponent;
        }

        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SpecialtyViewModels model, HttpPostedFileBase[] AttachmentImage)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // upload Files
                    if (AttachmentImage != null)
                    {
                        for (int i = 0; i < AttachmentImage.Length; i++)
                        {
                            var AttachmentPath = AttachmentImage[i];
                            if (AttachmentPath != null)
                            {
                                if (AttachmentPath.ContentLength > 0)
                                {
                                    model.AttachmentId = UploadFile(AttachmentPath);
                                }
                            }
                        }
                    }


                    if (model.Id != null && model.Id != Guid.Empty)
                        _specialtyComponent.Update(model);
                    else
                        _specialtyComponent.Create(model);

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpStatusCodeResult(404, $"model is not valid ,Resone {string.Join("-", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList())}");
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(404, $"model is not valid ,Resone {ex.Message}");
            }

        }

        [HttpPost]
        public ActionResult PageData(IDataTablesRequest request)
        {
            var orderColums = request.Columns.FirstOrDefault(x => x.Sort != null);
            var orderColumsName = string.Empty;
            var orderColumsDirect = (int)DataTables.AspNet.Core.SortDirection.Ascending;
            if (orderColums != null)
            {
                orderColumsName = orderColums.Name;
                orderColumsDirect = (int)orderColums.Sort.Direction;
            }
            var data = _specialtyComponent.GetPageData(request.Search.Value, request.Start, request.Length, orderColumsName, orderColumsDirect);
            var response = DataTablesResponse.Create(request, data.TotalDatalenght, data.FilterDatalenght, data.Items);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    var mdel = _specialtyComponent.Get(id);
                    mdel.Id = mdel.Id;
                    return Json(mdel, JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(404, "not found this index");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }

        }

        [HttpGet]
        public ActionResult Deactive_Activate(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    _specialtyComponent.Deactive_Activate(id);
                    return Json("ok", JsonRequestBehavior.AllowGet);
                }
                return new HttpStatusCodeResult(404, "not found this index");
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(404, ex.Message);
            }

        }

        private Guid UploadFile(HttpPostedFileBase fileBase)
        {

            var _fileName = DateTime.Now.ToLongDateString() + "_" + Path.GetFileName(fileBase.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/User_Files"), _fileName);
            var extension = Path.GetExtension(fileBase.FileName);
            fileBase.SaveAs(path);
            string fl = path.Substring(path.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string newpath = split[1];
            string imagepath = "/Content/User_Files/" + newpath;

            Guid id_;
            _attachmentComponent.Add(new Attachment
            {
                GeneratedFileName = imagepath,
                FileExtenstion = extension,
                FileSize = fileBase.ContentLength,
                AttachmentTypeId = (int)AttachmentTypeEnum.Image,
                OriginalFileName = fileBase.FileName,
            }, out id_);
            return id_;
        }
    }
}