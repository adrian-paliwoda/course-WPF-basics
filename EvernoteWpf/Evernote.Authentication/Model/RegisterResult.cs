namespace Evernote.Authentication.Model
{
    public class RegisterResult
    {
        public string Email { get; set; }
        public string ExpiresIn { get; set; }
        public string idToken { get; set; }
        public string Kind { get; set; }
        public string LocalId { get; set; }
        public string RefreshToken { get; set; }
    }
}