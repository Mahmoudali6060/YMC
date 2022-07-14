using Microsoft.AspNet.Identity;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.CrossCutting.Framework.ServiceLocator;
using Origin.YMC.Web.Client.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Origin.YMC.Web.Client.Helper
{
    public static class IdentityExtensionsHelper
    {
        private static IDoctorComponent _doctorServices;
        private static IPatientComponent _patientServices;
         static IdentityExtensionsHelper()
        {
            _doctorServices = ServiceLocatorFactory.CreateServiceLocator().GetService<IDoctorComponent>();
            _patientServices = ServiceLocatorFactory.CreateServiceLocator().GetService<IPatientComponent>(); 

        }

        public static Guid GetDoctorId(this IIdentity identity, string userId)
        {
            return _doctorServices.GetDoctorId(Guid.Parse(userId));
        }
        public static string GetDoctorImage(this IIdentity identity)
        {
            return   _doctorServices.GetDoctorProfileImage( Guid.Parse( identity.GetUserId() )) ;
        }
        //public static bool CheckDoctorCompleteProfile(this IIdentity identity, string userId)
        //{
        //    return _doctorServices.CheckUserCompleteProfile(Guid.Parse(userId));
        //}

        public static Guid GetPatientId(this IIdentity identity, string userId)
        {
            return _patientServices.GetPatientId(Guid.Parse(userId));
        }

        //public static bool CheckPatientCompleteProfile(this IIdentity identity, string userId)
        //{
        //    return _patientServices.CheckUserCompleteProfile(Guid.Parse(userId));
        //}
    }
}