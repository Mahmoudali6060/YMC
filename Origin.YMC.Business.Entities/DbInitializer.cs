using Origin.YMC.Business.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Origin.YMC.Business.Entities
{

    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public void Init()
        {
            InitRolesWithPermissions(new ApplicationDbContext());
            AddAdminCity(new ApplicationDbContext());
        }

        private static void InitRolesWithPermissions(ApplicationDbContext context)
        {
            if (!context.Roles.Any(x => x.Name == AppRoles.Interpreter))
            {
                var roleId_interpreter = Guid.NewGuid();
                context.Roles.Add(new ApplicationRole
                {
                    Id = roleId_interpreter,
                    Name = AppRoles.Interpreter,
                    IsEditable = false,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    RolePermission = new List<RolePermission>
                    {
                        new RolePermission
                        {
                            Id = Guid.NewGuid(),
                            PermissionEnumValue = (int)OriginYMCPermissions.ReadPromotions,
                            ApplicationRoleId = roleId_interpreter,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                        },
                        new RolePermission
                        {
                            Id = Guid.NewGuid(),
                            PermissionEnumValue = (int)OriginYMCPermissions.WritePromotions,
                            ApplicationRoleId = roleId_interpreter,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                        },
                        new RolePermission
                        {
                            Id = Guid.NewGuid(),
                            PermissionEnumValue = (int)OriginYMCPermissions.DeletePromotions,
                            ApplicationRoleId = roleId_interpreter,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                        },
                    }
                });

            }

            if (!context.Roles.Any(x => x.Name == AppRoles.Admin))
            {
                var roleId_admin = Guid.NewGuid();
                context.Roles.Add(new ApplicationRole
                {
                    Id = roleId_admin,
                    Name = AppRoles.Admin,
                    IsEditable = false,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    RolePermission = new List<RolePermission>
                {
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.ReadPromotions,
                        ApplicationRoleId = roleId_admin,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.WritePromotions,
                        ApplicationRoleId = roleId_admin,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.DeletePromotions,
                        ApplicationRoleId = roleId_admin,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                }
                });

            }

            if (!context.Roles.Any(x => x.Name == AppRoles.Doctor))

            {
                var roleId_doctor = Guid.NewGuid();
                context.Roles.Add(new ApplicationRole
                {
                    Id = roleId_doctor,
                    Name = AppRoles.Doctor,
                    IsEditable = false,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    RolePermission = new List<RolePermission>
                {
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.ReadPromotions,
                        ApplicationRoleId = roleId_doctor,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.WritePromotions,
                        ApplicationRoleId = roleId_doctor,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.DeletePromotions,
                        ApplicationRoleId = roleId_doctor,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                }
                });

            }
           
            if (!context.Roles.Any(x => x.Name == AppRoles.Patient))
            {
                var roleId_patient = Guid.NewGuid();

                context.Roles.Add(new ApplicationRole
                {
                    Id = roleId_patient,
                    Name = AppRoles.Patient,
                    IsEditable = false,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    RolePermission = new List<RolePermission>
                {
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.ReadPromotions,
                        ApplicationRoleId = roleId_patient,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.WritePromotions,
                        ApplicationRoleId = roleId_patient,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                    new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        PermissionEnumValue = (int)OriginYMCPermissions.DeletePromotions,
                        ApplicationRoleId = roleId_patient,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                    },
                }
                });

            }

            context.SaveChanges();
        }

        private static void AddAdminCity(ApplicationDbContext context)
        {
            var id = Guid.Parse("A8859A35-0898-4DDB-9E32-FAE5867C064A");
            if (context.City.Any(c => c.Id == id))
            {
                return;
            }
            context.City.Add(new Domain.Cities.City
            {
                Id = id,
                Name = "AdminCity",
                IsActive = false,
                CreationDate = DateTime.Now,
                CreatedBy = Guid.NewGuid()
            });
            context.SaveChanges();
        }

    }
}
