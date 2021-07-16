using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace TeamRedBackEnd.Services
{
    public class AuthService : IAuthService
    {
        readonly string JwtSecret;
        readonly int JwtLifespan;
        public AuthService(string JwtSecret, int JwtLifespan)
        {
            this.JwtSecret = JwtSecret;
            this.JwtLifespan = JwtLifespan;
        }
        public DataTransferObject.AuthData GetAuthData(string id)
        {
            DateTime ExpirationTime = DateTime.Now.AddSeconds(JwtLifespan);

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = ExpirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            string Token = TokenHandler.WriteToken(TokenHandler.CreateToken(TokenDescriptor));
            return new DataTransferObject.AuthData
            {
                Token = Token,
                TokenExpirationTime = ((DateTimeOffset)ExpirationTime).ToUnixTimeSeconds(),
                Id = id
            };
        }

        public DataTransferObject.AuthData GetAuthData(string id, int lifespan )
        {
            DateTime ExpirationTime = DateTime.Now.AddSeconds(lifespan);

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = ExpirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            string Token = TokenHandler.WriteToken(TokenHandler.CreateToken(TokenDescriptor));
            return new DataTransferObject.AuthData
            {
                Token = Token,
                TokenExpirationTime = ((DateTimeOffset)ExpirationTime).ToUnixTimeSeconds(),
                Id = id
            };
        }

    }
}
