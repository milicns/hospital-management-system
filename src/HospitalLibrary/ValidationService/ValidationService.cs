using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Auth.Dto;
using HospitalLibrary.Exceptions;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.User.Dto;
using HospitalLibrary.User.Model;
using HospitalLibrary.User.Repository;

namespace HospitalLibrary.ValidationService
{
    public class ValidationService : IValidationService
    {
        private readonly IUserRepository _userRepository;

        public ValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CheckIfEmailExists(string email)
        {
             return GetAll().Any(u => u.Email == email);
             
        }
        
        public bool CheckIfUcidExists(int ucid)
        {
            return GetAll().Any(u => u.Ucid == ucid);
        }

        public void ValidateUniqueness(CreatePatientDto createPatientDto)
        {
            if (CheckIfEmailExists(createPatientDto.Email))
            {
                throw new EmailExistsException("User with this email already exists");
            }
            if (CheckIfUcidExists(createPatientDto.Ucid))
            {
                throw new UcidExistsException("User with this UCID already exists");
            }
        }

        private IEnumerable<User.Model.User> GetAll()
        {
            return _userRepository.GetAll();
        }
        
        public UserDto ValidateLoginData(LoginDto loginDto)
        {
            var user = FindUserByEmailAndPassword(loginDto.Email, loginDto.Password);
            var userDto = user?.ToDto();
            if (IsPatient(user))
            {
                var patient = user as Patient.Model.Patient;
                userDto.Blocked = patient.Blocked;
            }
            return userDto;
        }

        private User.Model.User FindUserByEmailAndPassword(string email, string password)
        {
            return GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        
        private bool IsPatient(User.Model.User user)
        {
            return user?.UserRole == UserRole.Patient;
        }
        
    }
}