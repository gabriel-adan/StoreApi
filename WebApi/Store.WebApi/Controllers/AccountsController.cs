using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;
using Store.WebApi.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserAccountRepository userAccountRepository;

        public AccountsController(IConfiguration configuration, IUserAccountRepository userAccountRepository) : base ()
        {
            this.configuration = configuration;
            this.userAccountRepository = userAccountRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = userAccountRepository.LogIn(model.UserName, model.Password);
                    if (user != null)
                    {
                        string secretTokenKey = configuration.GetSection("SecretTokenKey").Value;
                        byte[] secretKey = Encoding.ASCII.GetBytes(secretTokenKey);
                        string validIssuer = configuration.GetSection("ValidIssuer").Value;
                        string validAudience = configuration.GetSection("ValidAudience").Value;

                        var JWToken = new JwtSecurityToken(
                            issuer: validIssuer,
                            audience: validAudience,
                            claims: new Claim[] {
                                new Claim("User", user.UserName),
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim("UserName", user.FullName),
                                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString())
                            },
                            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                            expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                        );
                        var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                        return Ok(new { access_token = token });
                    }
                    else
                    {
                        return NotFound(new { ErrorMessage = "Usuario o contraseña inválido." });
                    }
                }
                return NotFound(new { ErrorMessage = "Error al iniciar sesión." });
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}