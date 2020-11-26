
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Todo_Serverless.DTOs;
using Todo_Serverless.Models;




namespace Todo_Serverless.Services.AccessTokens
{

    /// <summary>
    /// Validates a incoming request and extracts any <see cref="ClaimsPrincipal"/> contained within the bearer token.
    /// </summary>
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string BEARER_PREFIX = "Bearer ";
        private readonly string _issuerToken;
        private readonly string _audience;
        private readonly string _issuer;

        public AccessTokenProvider(string issuerToken, string audience, string issuer)
        {
            _issuerToken = issuerToken;
            _audience = audience;
            _issuer = issuer;
        }



        public string GenerateToken(LoginDTO user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentException(nameof(user));

                var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");

                var expiresIn = DateTime.UtcNow.AddMinutes(20);

                var symmetricKey = Convert.FromBase64String(_issuerToken);
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                            {
                            new Claim(ClaimTypes.NameIdentifier, user.Email),
                            new Claim(ClaimTypes.Name, "Felix Gift"),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("DatabaseName",databaseName),
                            new Claim(ClaimTypes.Expiration, expiresIn.Ticks.ToString())
                        }),

                    Expires = expiresIn,
                    Audience = _audience,
                    Issuer = _issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature),

                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return token;

            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public AccessTokenResult ValidateToken(HttpRequest request)
        {
            try
            {
                // Get the token from the header
                if (request != null &&
                    request.Headers.ContainsKey(AUTH_HEADER_NAME) &&
                    request.Headers[AUTH_HEADER_NAME].ToString().StartsWith(BEARER_PREFIX))
                {
                    var token = request.Headers[AUTH_HEADER_NAME].ToString().Substring(BEARER_PREFIX.Length);

                    var symmetricKey = Convert.FromBase64String(_issuerToken);
                    var validationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _issuer,
                        ValidAudience = _audience,
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                    };

                    // Validate the token
                    var handler = new JwtSecurityTokenHandler();
                    var simplePrinciple = handler.ValidateToken(token, validationParameters, out var securityToken);
                    
                    return AccessTokenResult.Success(simplePrinciple);
                }
                else
                {
                    return AccessTokenResult.NoToken();
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                return AccessTokenResult.Expired();
            }
            catch (Exception ex)
            {
                return AccessTokenResult.Error(ex);
            }
        }
    }
}
