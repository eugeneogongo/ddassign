using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DDWebservice.Models;
using Microsoft.IdentityModel.Tokens;

namespace DDWebservice.BLL
{
    public class JWTService
    {
        private string SecretKey { get; } = "FN4rnejdE4jNDKW495jhhdDFRRR34d=="; // This secret key should be in WebConfig.

        /// <summary>
        /// Generates token .
        /// Validates whether the given model is valid, then gets the symmetric key.
        /// Encrypt the token and returns it.
        /// <remarks>At least one claim is Required</remarks>
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Generated token.</returns>
        public string GenerateToken(JWTModel model)
        {
            
            if (model?.Claims == null || model.Claims.Length == 0)
                throw new ArgumentException("Arguments to create token are not valid.");
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), model.SecurityAlgorithm)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }
        /// <summary>
        /// Generates a SymmetricKey
        /// </summary>
        /// <returns>Symmetric Key</returns>

        private SecurityKey GetSymmetricSecurityKey()
        {
            var symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}