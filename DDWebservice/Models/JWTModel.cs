using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DDWebservice.Models
{
    public class JWTModel
    {
        #region Public Methods
        public int ExpireMinutes { get; set; } = 10080; // 7 days.
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        public Claim[] Claims { get; set; }

        public static JWTModel GetJWTContainerModel( string email)
        {
            return new JWTModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }
        #endregion
    }
}