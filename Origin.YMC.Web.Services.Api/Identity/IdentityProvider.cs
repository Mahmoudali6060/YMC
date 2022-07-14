using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities;
using Origin.YMC.CrossCutting.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Origin.YMC.Web.Services.Api.Identity
{
        //***********For the Sake of the database*********
        //TODO: Get user from a cookie set on login 
        //TODO: Cache/Get Permission from the same cookie
        //***********For the Sake of the database*********

        public class AspNetIdentityProvider : IIdentityProvider
        {
            private readonly IPrincipal _principal;
            private readonly IUserComponent _userService;
            private List<OriginYMCPermissions> _userPermissions;


            #region Constructors

            public AspNetIdentityProvider(IPrincipal principal, IUserComponent userService)
            {
                if (principal == null) throw new ArgumentNullException("principal");
                _principal = principal;

                if (userService == null) throw new ArgumentNullException("userService");
                _userService = userService;
            }

            #endregion

            #region IIdentityProvider Members

            public string GetUserName()
            {
                return _principal.Identity.Name;
            }

            public Guid GetUserId()
            {
                if (_principal != null && _principal.Identity != null)
                {
                    var user = _userService.GetByUsername(GetUserName());
                    return user != null ? user.Id : Guid.Empty;
                }
                else
                {
                    return Guid.Empty;
                }
            }

            public List<Guid> GetUserRoles()
            {
                if (_principal != null && _principal.Identity != null)
                {
                   return _userService.GetUserRoles(GetUserName());                                        
                }
                return null;
            }

            public List<OriginYMCPermissions> GetUserPermissions()
            {
                if (_principal != null && _principal.Identity != null)
                {
                    var username = GetUserName();
                    return _userService.GetUserPermissions(username);             

                }
                return null;
            }

            /// <summary>
            /// Determines whether the specified permission has permission.
            /// </summary>
            /// <param name="permission">The permission.</param>
            /// <returns></returns>
            public bool HasPermission(OriginYMCPermissions permission)
            {
                return HasPermissions(new List<OriginYMCPermissions> { permission });
            }

            /// <summary>
            /// Determines whether the specified permissions has permissions.
            /// </summary>
            /// <param name="permissions">The permissions.</param>
            /// <returns></returns>
            public bool HasPermissions(List<OriginYMCPermissions> permissions)
            {
                //1- get current user identity, if it is null, then user is not authenticated or have no record in the DB
                //2- query the DB for the permissions assosicated to this user, by iterating over the user roles and for each role check its permissions
                //3- if the permission is there, then return true, otherwise return false            


                var username = GetUserName();
                if (username == null)
                    return false;

                if (_userPermissions == null)
                {
                    List<OriginYMCPermissions> lst = _userService.GetUserPermissions(username);
                    _userPermissions = new List<OriginYMCPermissions>();
                    //_userPermissions = lst.Select(a => a.Id).Cast<YallaPermission>().ToList();
                    foreach (var item in lst)
                    {
                        if (item != null)
                        {
                            _userPermissions.Add(item);
                        }
                    }
                }

                foreach (var permission in permissions)
                {
                    if (!_userPermissions.Contains(permission))
                        return false;
                }

                return true;
            }

            #endregion

        }    
}