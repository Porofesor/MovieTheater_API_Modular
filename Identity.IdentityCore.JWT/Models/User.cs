namespace Identity.IdentityCore.JWT.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string UsertName { get; set; }  = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
