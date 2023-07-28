using HospitalLibrary.User.Model;

namespace HospitalLibrary.User.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        public Gender Gender { get; set; }
        public bool Blocked { get; set; }

    }
}