using System.Security.Cryptography;


namespace Identity.IdentityCore.JWT.Infrastructure.Infrastructure
{
    /// <summary>
    /// Provides methods for hashing and verifying passwords using PBKDF2 (Rfc2898DeriveBytes).
    /// </summary>
    internal sealed class PasswordHasher
    {
        // Constants for the hasher
        private const int SaltSize = 22;  // Size of the salt in bytes
        private const int KeySize = 32;   // Size of the key in bytes (256 bits)
        private const int Iterations = 10000; // Number of PBKDF2 iterations (can be increased for higher security)

        /// <summary>
        /// Hashes the provided password using PBKDF2.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>The hashed password with the salt included (Base64 encoded).</returns>
        public string HashPassword(string password)
        {
            // Generate a random salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                // Use Rfc2898DeriveBytes to hash the password
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(KeySize);

                    // Combine the salt and hash into a single array
                    byte[] hashBytes = new byte[SaltSize + KeySize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

                    // Convert the result to a Base64-encoded string and return
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        /// <summary>
        /// Verifies the provided password against the stored hash.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="storedHash">The stored password hash (Base64 encoded) that contains both the salt and hash.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        public bool VerifyPassword(string password, string storedHash)
        {
            // Convert the Base64 encoded hash back to a byte array
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt from the stored hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Hash the provided password using the same salt and iterations
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);

                // Compare the newly generated hash with the stored hash
                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[SaltSize + i] != hash[i])
                    {
                        return false; // Passwords do not match
                    }
                }
            }

            return true; // Passwords match
        }
    }
}
