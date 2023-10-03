using Application.Interfaces.IServices;
using Domain.Models;
using Domain.Models.DTOs;
using Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Web.REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        /// <summary>
        /// Authenticate / Login. Validates user existence in the system, then generate a JWT.
        /// </summary>
        /// <param name="user">User object with Email and Password</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>string</returns>
        /// <response code="401">Unauthorized</response>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> PostAuthAsync([FromBody] AuthUserDTO user, CancellationToken ct)
        {
            DomainUser? dbUser = await _userService.FindUserByEmailAsync(user.Email!, ct);

            if (dbUser is not null)
            {
                if (user.Password!.GetSha256() == dbUser.Password)
                {
                    string token = CreateJWT(
                        dbUser.FullName!, dbUser.Email!, dbUser.Role.ToString(), dbUser.Id.ToString());
                    return Ok(token);
                }
                else
                    return Unauthorized("Incorrect Password");
            }
            else
            {
                return Unauthorized("User does not exists, please register first.");
            }
        }

        /// <summary>
        /// Validate password and creates a new user and provides a JWT.
        /// </summary>
        /// <param name="user">User To Insert DTO</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Anonymous Object</returns>
        /// <remarks>
        /// I had issues with the 'CreatedAtAction' so I am returnin 200 when success!
        /// </remarks>
        /// <response code="200">Ok, User created and JWT generated</response>
        /// <response code="400">Bad Request, double check input</response>
        /// <response code="409">Conflict, user email already used</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public async Task<IActionResult> PostRegisterAsync([FromBody] RegisterUserDTO user, CancellationToken ct)
        {
            DomainUser? checkUserExistence = await _userService.FindUserByEmailAsync(user.Email!, ct);

            if (checkUserExistence is not null)
                return Conflict("User already exists with email: " + user.Email);

            try
            {
                DisplaySimpleUserDTO? userCreated = await _userService.CreateUserAsync(user, ct);
                string token = CreateJWT(
                    userCreated!.FullName!, userCreated.Email!, userCreated.Role!, userCreated.Id!);
                return Ok(new { Token = token, Response = userCreated });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateJWT(string name, string email, string role, string id)
        {
            string secret = _config.GetSection("JurnalaSecretKey").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] byteKey = Encoding.UTF8.GetBytes(secret);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Email, email),
                        new Claim("Role", role),
                        new Claim("Id", id),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}
