namespace HRManagement.Application.Dtos.Roles
{
    public class AssignRolesToUserDto
    {
        public int UserId { get; set; }

        public List<int> RoleIds { get; set; }
    }
}
