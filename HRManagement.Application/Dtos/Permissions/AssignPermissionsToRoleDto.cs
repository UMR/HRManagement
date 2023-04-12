namespace HRManagement.Application.Dtos.Permissions
{
    public class AssignPermissionsToRoleDto
    {
        public int RoleId { get; set; }

        public List<int> PermissionIds { get; set; }
    }
}
