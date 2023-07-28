using HospitalLibrary.Auth.Dto;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.User.Dto;

namespace HospitalLibrary.ValidationService
{
    public interface IValidationService
    {
       
        bool CheckIfEmailExists(string email);
        bool CheckIfUcidExists(int ucid);
        void ValidateUniqueness(CreatePatientDto createPatientDto);
        UserDto ValidateLoginData(LoginDto loginDto);
    }
}