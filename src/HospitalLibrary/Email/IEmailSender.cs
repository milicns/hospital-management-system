namespace HospitalLibrary.Email
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}