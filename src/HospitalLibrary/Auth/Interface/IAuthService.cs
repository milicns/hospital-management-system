using HospitalLibrary.Auth.Dto;
using HospitalLibrary.User.Dto;

namespace HospitalLibrary.Auth.Interface
{
    public interface IAuthService
    { 
        public string Authenticate(UserDto userDto);
    }
}