using Identity.IdentityCore.JWT.Infrastructure.Persistence;
using Identity.IdentityCore.JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.IdentityCore.JWT.Infrastructure.Infrastructure
{
    internal sealed class LoginUsers
    {
        private readonly UsersDbContext _context;
        private readonly PasswordHasher _passwordHasher;
        private readonly TokenProvider _tokenProvider;

        // Constructor to inject dependencies
        public LoginUsers(UsersDbContext context, PasswordHasher passwordHasher, TokenProvider tokenProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
        }

        // Request record to handle login input (email, password)
        public sealed record Request(string Email, string Password);

        // Method to handle user login logic
        public async Task<string> Handle(Request request)
        {
            // Fetch user by email
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            // Check if user exists and email is verified
            if (user == null || !user.IsEmailVerified)
            {
                throw new Exception("User not found or email is not verified.");
            }

            // Verify the password using the password hasher
            bool verified = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            // Throw an exception if the password is incorrect
            if (!verified)
            {
                throw new Exception("The password is incorrect.");
            }

            string token = _tokenProvider.Create(user);
            // Return the user (optionally, you can generate a JWT token here)
            return token;
        }
    }
}