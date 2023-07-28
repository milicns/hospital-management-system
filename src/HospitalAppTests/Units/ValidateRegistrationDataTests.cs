
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.User.Model;
using HospitalLibrary.User.Repository;
using HospitalLibrary.ValidationService;
using Moq;
using Shouldly;

namespace HospitalAppTests.Units;

public class ValidateRegistrationDataTests
{
    
    [Theory]
    [MemberData(nameof(Data))]
    public void Check_patient_email_is_unique(CreatePatientDto createPatientDto, bool uniqueFlag)
    {
        IValidationService service = new ValidationService(RepositoryForValidations());
        bool isUnique = service.CheckIfEmailExists(createPatientDto.Email);
        isUnique.ShouldBe(uniqueFlag);

    }
    
    [Theory]
    [MemberData(nameof(Data))]
    public void Check_patient_ucid_is_unique(CreatePatientDto createPatientDto, bool uniqueFlag)
    {
        IValidationService service = new ValidationService(RepositoryForValidations());
        bool isUnique = service.CheckIfUcidExists(createPatientDto.Ucid);
        isUnique.ShouldBe(uniqueFlag);

    }
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { PatientWithUniqueUcidAndEmail(), false },
            new object[] { PatientWithNotUniqueUcidAndEmail(), true },
        };


    
    public static CreatePatientDto PatientWithUniqueUcidAndEmail()
    {
        return new CreatePatientDto { Name = "Milos",Surname = "Petrovic",Ucid = 1234, Email = "milos@gmail.com"};
    }
    
    public static CreatePatientDto PatientWithNotUniqueUcidAndEmail()
    {
        return new CreatePatientDto { Name = "Milos",Surname = "Milic",Ucid = 123, Email = "milic@gmail.com"};
    }

    private IUserRepository RepositoryForValidations()
    {
        var stubRepository = new Mock<IUserRepository>();
        var users = new List<User>();
        users.Add(new Patient{Id = 1,Email = "milic@gmail.com",Ucid = 123});
        stubRepository.Setup(m => m.GetAll()).Returns(users);
        return stubRepository.Object;
    }
    
    
}