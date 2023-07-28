using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalLibrary.Auth.Interface;
using HospitalLibrary.User.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HospitalLibrary.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        
        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }
        
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        
        private ClaimsIdentity SetClaims(UserDto userDto)
        {
            var claims = new ClaimsIdentity(
            
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDto.Email),
                    new Claim(ClaimTypes.Role, userDto.UserRole.ToString()),
                    new Claim(ClaimTypes.PrimarySid, userDto.Id.ToString()),
                    new Claim(ClaimTypes.Gender, userDto.Gender.ToString())
                }
            );
            return claims;
        }
        
        private SecurityTokenDescriptor GenerateTokenOptions(SigningCredentials signingCredentials, ClaimsIdentity claims)
        {
            var tokenOptions = new SecurityTokenDescriptor{
                Subject = claims,
                Audience = _jwtSettings["validAudience"],
                Issuer = _jwtSettings["validIssuer"],
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                SigningCredentials = signingCredentials};
            return tokenOptions;
        }
        
        public string GenerateJwt(UserDto userDto)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = SetClaims(userDto);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenOptions));
            return token;
        }
    }
}