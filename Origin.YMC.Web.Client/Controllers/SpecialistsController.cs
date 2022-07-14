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

namespace Origin.YMC.Web.Client.Controllers
{
    public class SpecialistsController : BaseController
    {

        protected ApplicationUserManager _userManager;
        private readonly IUserComponent _userComponent;
        private readonly ICountryComponent _countryComponent;
        private readonly ICityComponent _cityComponent;
        private readonly IPatientComponent _patientComponent;
        private readonly IDoctorComponent _doctorComponent;
        private readonly IPaymentComponent _paymentComponent;
        private readonly ILogComponent _logComponent;
        private readonly IAttachmentComponent _attachmentComponent;
        private readonly ISocialComponent _socialComponent;
        private readonly IPartationComponent _partationComponent;
        private readonly ITestimonialComponent _testimonialComponent;
        private readonly ISpecialtyComponent _specialtyComponent;

        public SpecialistsController(IUserComponent userComponent
                             , ICountryComponent countryComponent
                             , ICityComponent cityComponent
                             , IPatientComponent patientComponent
                             , IDoctorComponent doctorComponent
                             , IPaymentComponent paymentComponent
                             , IAttachmentComponent attachmentComponent
                             , ILogComponent logComponent
                             , ISocialComponent socialComponent
                             , IPartationComponent partationComponent
                             , ITestimonialComponent testimonialComponent
                             , ISpecialtyComponent specialtyComponent) : base(attachmentComponent)
        {
            _userComponent = userComponent;
            _countryComponent = countryComponent;
            _cityComponent = cityComponent;
            _patientComponent = patientComponent;
            _doctorComponent = doctorComponent;
            _paymentComponent = paymentComponent;
            _logComponent = logComponent;
            _attachmentComponent = attachmentComponent;
            _socialComponent = socialComponent;
            _partationComponent = partationComponent;
            _testimonialComponent = testimonialComponent;
            _specialtyComponent = specialtyComponent;
        }


        // GET: Specialists
        public ActionResult Index()
        {
            ViewBag._specialties = _specialtyComponent.GetList(string.Empty).ToList();
            return View();
        }
    }
}