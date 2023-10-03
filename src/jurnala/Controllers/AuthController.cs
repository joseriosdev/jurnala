using Application.Interfaces.IServices;
using Domain.Models.DTOs;
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
        /// <param name="u"></param>
        /// <returns>string</returns>
        /// <remarks>
        /// This is a "Remark"
        /// </remarks>
        /// <response code="404">Not Found response</response>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<string> PostAuthAsync([FromBody] object u)
        {
            await Task.Delay(10);

            if (true)
            {


            }
            return "Invalid user or password";
        }

        /// <summary>
        /// Validate password and creates a new user and provides a JWT.
        /// </summary>
        /// <param name="user">User To Insert DTO</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Anonymous Object</returns>
        /// <remarks>
        /// Had Issues with the 'CreatedAtAction' so I am returnin 200 when success!
        /// </remarks>
        /// <response code="200">Ok, User created and JWT generated</response>
        /// <response code="400">Bad Request, double check input</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> PostRegisterAsync([FromBody] InsertUserDTO user, CancellationToken ct)
        {
            try
            {
                DisplaySimpleUserDTO? userCreated = await _userService.CreateUserAsync(user, ct);
                string token = this.CreateJWT(
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
