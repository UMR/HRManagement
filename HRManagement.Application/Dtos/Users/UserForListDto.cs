namespace HRManagement.Application.Dtos.Users
{
    public class UserForListDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; }

        public string Email { get; set; } 

        public string Password { get; set; }

        public DateTime Created { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public int? LastModifiedBy { get; set; }
    }
}
