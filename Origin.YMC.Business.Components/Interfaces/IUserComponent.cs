using Origin.YMC.Business.Entities;
using Origin.YMC.Business.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IUserComponent
    {
        ApplicationUser GetByUsername(string username);

        List<OriginYMCPermissions> GetUserPermissions(string username);

        List<Guid> GetUserRoles(string username);

        ApplicationUser CheckNameExist(string userName);

        ApplicationUser EmailExist(string email);

        ApplicationUser MobileExist(string mobile);
    }
}
