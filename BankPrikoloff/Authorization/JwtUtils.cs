using BusinessLogic.Helpers;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using BusinessLogic.Authorization;
using Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace BankPrikoloff.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly AppSettings _appSettings;

        public JwtUtils(
            IRepositoryWrapper wrapper, 
            IOptions<AppSettings> appSettings)
        {
            _wrapper = wrapper;
            _appSettings = appSettings.Value; 
        }

        public string GenerateJwtToken(User account)
        {
            throw new NotImplementedException();
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            var tokenIsUnique = (await _wrapper.User.FindByCondition(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token))).Count == 0;

            if (!tokenIsUnique)
            {
                return await GenerateRefreshToken(ipAddress);
            }
            return refreshToken;
            
        }

        public int? ValidateJwtToken(string token)
        {
            if(token == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return accountId;
            }
            catch
            {
                return null;
            }
        }
    }
}
