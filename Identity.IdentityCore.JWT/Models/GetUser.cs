namespace Identity.IdentityCore.JWT.Models
{
    public class GetUser
    {
        public class UserResponse
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public bool IsEmailVerified { get; set; }
        }

        public async Task<UserResponse?> Handle(Guid id)
        {
            // Fetch the user by id and return the UserResponse
            return new UserResponse
            {
                Id = id,
                Email = "test@example.com",
                Name = "Test User",
                IsEmailVerified = true
            };
        }
    }
}
