using Origin.YMC.Business.Entities;
using System;
using System.Collections.Generic;

namespace Origin.YMC.CrossCutting.Framework
{
    public interface IIdentityProvider
    {
        string GetUserName();

        Guid GetUserId();

        List<Guid> GetUserRoles();
        List<OriginYMCPermissions> GetUserPermissions();

        bool HasPermission(OriginYMCPermissions permission);

        bool HasPermissions(List<OriginYMCPermissions> permissions);
    }
}
