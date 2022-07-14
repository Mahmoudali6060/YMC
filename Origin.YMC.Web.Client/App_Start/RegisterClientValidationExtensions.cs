using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Origin.YMC.Web.Client.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace Origin.YMC.Web.Client.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}