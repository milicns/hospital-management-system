
using HospitalLibrary.Auth.Dto;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.User.Model;
using HospitalLibrary.User.Repository;
using HospitalLibrary.ValidationService;
using Moq;
using Shouldly;

namespace HospitalAppTests.Units;

public class LoginDataValidationTets
{

    [Fact]
    public void Validate_login_data_success()
    {
        IValidationService validationService = new ValidationService(RepositoryForValidations());
        var loginDto = new LoginDto{Email = "milic@gmail.com", Password = "asd"};
        validationService.ValidateLoginData(loginDto).ShouldNotBeNull();
    }

    [Fact]
    public void Validate_login_data_fail()
    {
        IValidationService validationService = new ValidationService(RepositoryForValidations());
        var loginDto = new LoginDto { Email = "milic@gmail.com", Password = "as" };
        validationService.ValidateLoginData(loginDto).ShouldBeNull();
    }
    
    private IUserRepository RepositoryForValidations()
    {
        var stubRepository = new Mock<IUserRepository>();
        var users = new List<User>();
        users.Add(new Patient { Id = 1, Email = "milic@gmail.com", Password = "asd", BloodType = BloodType.AbNegative,Blocked = true});
        stubRepository.Setup(m => m.GetAll()).Returns(users);
        return stubRepository.Object;
    }
}