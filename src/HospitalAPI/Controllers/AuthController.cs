
using HospitalLibrary.Auth.Dto;
using HospitalLibrary.Auth.Interface;
using HospitalLibrary.Exceptions;
using HospitalLibrary.ValidationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidationService _validationService;

        public AuthController(IAuthService authService, IValidationService validationService)
        {
            _authService = authService;
            _validationService = validationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto userForAuthentication)
        {
            var user = _validationService.ValidateLoginData(userForAuthentication);
            if (user == null)
                return Unauthorized(new ErrorObject { Message = "Account with that email or password doesn't exists" });
            if (user.Blocked)
                return Unauthorized(new ErrorObject { Message = "Your account is blocked from logging in. Contact administrator for more informations." });
            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = _authService.Authenticate(user), UserRole = user.UserRole.ToString()});
        }
    }
}