namespace HospitalLibrary.Auth.Dto
{
    public class AuthResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public string UserRole { get; set; }
    }
}