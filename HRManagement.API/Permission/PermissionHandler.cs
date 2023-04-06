using HRManagement.API.Permission;
using HRManagement.Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;

namespace HRManagement.API.Permission
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;

        public PermissionAuthorizationHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            var permissions =  context.User.Claims.Where(x => x.Type == "Permission" &&
                                                            x.Value == requirement.Permission &&
                                                            x.Issuer == "LOCAL AUTHORITY");


            var permission = await _permissionService.GetPermissionsAsync();


            if (permission.Where(p => p.Name == requirement.Permission) != null)
            {
                context.Succeed(requirement);
                return;
            }

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
