using Microsoft.AspNet.Identity.EntityFramework;
using Origin.YMC.Business.Entities.Domain.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Origin.YMC.Business.Entities.Domain
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<Guid, UserLogin, UserRole, UserClaim>, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderEnum? GenderEnum { get; set; }

        public DateTime? BirthDate { get; set; }
      

        public int Grade { get; set; }

        [ForeignKey("City")]
        public Guid? CityId { get; set; }

        #region IEntity
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
        #endregion

        public virtual City City { get; set; }

        public int? HeardAboutUsId { get; set; }
        public int? SecurityQuestionId { get; set; }
        public string SecurityQuestionAnswer { get; set; }
        public string NonUsStateOrProvince { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ZipPostalCode { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,Guid> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationRole : IdentityRole<Guid, UserRole>, IEntity
    {
        /// <summary>
        /// False for the developers set values the ones which saved to the database form the seed function
        /// </summary>
        public bool? IsEditable { get; set; }

        #region IEntity
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
        #endregion

        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }

    public class RolePermission : EntityBase
    {
        public Guid ApplicationRoleId { get; set; }

        /// <summary>
        /// Values range are from YallaPermissions.cs
        /// </summary>
        public int PermissionEnumValue { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }
    }
    public class UserRole : IdentityUserRole<Guid>//, IEntity
    {

        //#region IEntity
        //public Guid Id { get; set; }
        //public DateTime CreationDate { get; set; }
        //public Guid CreatedBy { get; set; }
        //public DateTime? LastUpdatedAt { get; set; }
        //public Guid? LastUpdatedBy { get; set; }
        //public bool IsActive { get; set; }
        //#endregion

        public virtual ApplicationRole Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class UserLogin : IdentityUserLogin<Guid>//, IEntity
    {

        //#region IEntity
        //public Guid Id { get; set; }

        //public DateTime CreationDate { get; set; }
        //public Guid CreatedBy { get; set; }
        //public DateTime? LastUpdatedAt { get; set; }
        //public Guid? LastUpdatedBy { get; set; }
        //public bool IsActive { get; set; }
        //#endregion
    }

    public class UserClaim : IdentityUserClaim<Guid>//, IEntity
    {

        //#region IEntity
        //public Guid Id { get; set; }

        //public DateTime CreationDate { get; set; }
        //public Guid CreatedBy { get; set; }
        //public DateTime? LastUpdatedAt { get; set; }
        //public Guid? LastUpdatedBy { get; set; }
        //public bool IsActive { get; set; }
        //#endregion
    }


}
