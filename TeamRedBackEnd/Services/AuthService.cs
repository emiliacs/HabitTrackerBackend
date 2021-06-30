using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace TeamRedBackEnd.Services
{
    public class AuthService : IAuthService
    {
        string JwtSecret;
        int JwtLifespan;
        public AuthService(string JwtSecret, int JwtLifespan)
        {
            this.JwtSecret = JwtSecret;
            this.JwtLifespan = JwtLifespan;
        }
        public ViewModels.AuthData GetAuthData(string id)
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
            return new ViewModels.AuthData
            {
                Token = Token,
                TokenExpirationTime = ((DateTimeOffset)ExpirationTime).ToUnixTimeSeconds(),
                Id = id
            };
        }

        public ViewModels.AuthData GetAuthData(string id, int lifespan )
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
            return new ViewModels.AuthData
            {
                Token = Token,
                TokenExpirationTime = ((DateTimeOffset)ExpirationTime).ToUnixTimeSeconds(),
                Id = id
            };
        }

        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return actualPassword == hashedPassword;
        }

    }
}
