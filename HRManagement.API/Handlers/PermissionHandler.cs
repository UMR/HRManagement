using HRManagement.Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;

namespace HRManagement.API.Handlers
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserService _userService;

        public PermissionHandler(IUserService userService)
        {
            _userService = userService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                // no user authorizedd. Alternatively call context.Fail() to ensure a failure 
                // as another handler for this requirement may succeed
                return null;
            }

            var currentUser = context.User;
            var currentRequirement = requirement.Permission;

            if (currentRequirement == "Kaptan1")
            {
                context.Succeed(requirement);
            }
            

            return Task.CompletedTask;

        }

    }
}
