using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HospitalLibrary.User.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HospitalLibrary.Auth.Interface
{
    public interface IJwtHandler
    {
        string GenerateJwt(UserDto userDto);
    }
}