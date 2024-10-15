using Identity.IdentityCore.JWT.Infrastructure.Persistence;
using Identity.IdentityCore.JWT.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Identity.IdentityCore.JWT.Infrastructure.Infrastructure
{
    internal sealed class RegisterUsers
    {
        private readonly UsersDbContext _context;
        private readonly PasswordHasher _passwordHasher;

        // Constructor to inject dependencies
        public RegisterUsers(UsersDbContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // Define the Request record to handle registration data (email, password, etc.)
        public sealed record Request(string Email, string Password);


        // Generate a random token for email verification
        private string GenerateVerificationToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        // Extend the handle method to include email verification logic
        public async Task<User> Handle(Request request)
        {
            // Check if the email already exists
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new Exception("Email already registered.");
            }

            string passwordHash = _passwordHasher.HashPassword(request.Password);

            string emailVerificationToken = GenerateVerificationToken(); // Generate the verification token

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                IsEmailVerified = false, // Initial email verification status is false
                CreatedAt = DateTime.UtcNow,
                EmailVerificationToken = emailVerificationToken // Store the verification token
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Send verification email (email service integration required)
            await SendVerificationEmail(user.Email, emailVerificationToken);

            return user;
        }

        // Placeholder for email service (you'll need to integrate a real email sender)
        private Task SendVerificationEmail(string email, string token)
        {
            // This is where you integrate an email service like SendGrid, SMTP, etc.
            string verificationLink = $"https://yourapi.com/api/users/verify-email?token={token}";
            Console.WriteLine($"Send email to {email} with verification link: {verificationLink}");
            return Task.CompletedTask;
        }
    }
}
