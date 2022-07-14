using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities;
using Origin.YMC.Business.Entities.Domain;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Implementation
{
    public class UserComponent : IUserComponent
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        public UserComponent(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }
        public ApplicationUser GetByUsername(string username)
        {
            return _userRepository.Query().SingleOrDefault(x => x.UserName == username);
        }

        public List<OriginYMCPermissions> GetUserPermissions(string username)
        {
            var user = GetByUsername(username);

            var userRoles = user.Roles.Select(x => x.Role);
            var rolePermissions = userRoles.SelectMany(x => x.RolePermission);
            var permissions = rolePermissions.Select(x =>
                (OriginYMCPermissions)x.PermissionEnumValue).Distinct();

            return permissions.ToList();
        }

        public List<Guid> GetUserRoles(string username)
        {
            var user = GetByUsername(username);
            return user.Roles.Select(x => x.RoleId).ToList();
        }

        public  ApplicationUser CheckNameExist(string userName)
        {
            return _userRepository.Query().FirstOrDefault(x => x.UserName == userName);
        }

        public  ApplicationUser EmailExist(string email)
        {
            return _userRepository.Query().FirstOrDefault(x => x.Email == email);
        }

        public  ApplicationUser MobileExist(string mobile)
        {
            return _userRepository.Query().FirstOrDefault(x => x.Mobile == mobile);
        }
    }
}
