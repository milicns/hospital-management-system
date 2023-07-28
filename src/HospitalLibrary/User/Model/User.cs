using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.User.Dto;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.User.Model
{
    public abstract class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Ucid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole UserRole { get; set; }
        
        public UserDto ToDto()
        {
            return new UserDto
            {
                Id = Id,
                Email = Email,
                UserRole = UserRole,
                Gender = Gender
            };
        }
    }
}