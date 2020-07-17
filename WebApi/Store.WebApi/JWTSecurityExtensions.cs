using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Store.WebApi
{
    public static class JWTSecurityExtensions
    {
        public static IServiceCollection AddJWTSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            string secretTokenKey = configuration.GetSection("SecretTokenKey").Value;
            string validIssuer = configuration.GetSection("ValidIssuer").Value;
            string validAudience = configuration.GetSection("ValidAudience").Value;
            byte[] secretKey = Encoding.ASCII.GetBytes(secretTokenKey);
            services.AddAuthentication(auth => {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token => {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}
