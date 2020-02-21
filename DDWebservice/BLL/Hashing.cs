namespace DDWebservice.BLL
{/// <summary>
/// The Hashing used is Bcryt
/// More information can be found here: https://en.wikipedia.org/wiki/Bcrypt
/// </summary>
    public class Hashing
    {
        /// <summary>
        /// Generates a Random Hash
        /// </summary>
        /// <returns>Random Hash</returns>
        private static string GenerateRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        /// <summary>
        /// Returns an Hashed Password
        /// </summary>
        /// <param name="password">Plain password</param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GenerateRandomSalt());
        }
        /// <summary>
        /// Verify the hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}