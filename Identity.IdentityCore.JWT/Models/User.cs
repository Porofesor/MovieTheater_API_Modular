namespace Identity.IdentityCore.JWT.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? EmailVerificationToken { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
