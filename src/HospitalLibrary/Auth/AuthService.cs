
using HospitalLibrary.Auth.Interface;
using HospitalLibrary.User.Dto;

namespace HospitalLibrary.Auth
{
    public class AuthService : IAuthService
    {
       
        private readonly IJwtHandler _jwtHandler;
        
        public AuthService(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        public string Authenticate(UserDto userDto)
        {
            return _jwtHandler.GenerateJwt(userDto);
        }
    }
}