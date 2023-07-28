using HospitalLibrary;
using HospitalLibrary;
using HospitalLibrary.Email;
using Moq;

namespace HospitalAppTests.Units;

public class EmailTests
{
    [Fact]
    public void Send_welcome_email()
    {
        var mockEmailService = new Mock<IEmailSender>();
        var message = CreateMessage();
        mockEmailService.Object.SendEmail(message);
        mockEmailService.Verify(e => e.SendEmail(message));
        
    }

    private Message CreateMessage()
    {
        return new Message(new string[] { "milicvasilije9@gmail.com" }, "Welcome", "Welcome to our hospital.");
    }

}