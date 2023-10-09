using Application.Interfaces.IServices;
using Domain.Constants;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Models.Exception;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace jurnala.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get single user based on email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>User to Display Object</returns>
        /// <response code="200">Found and return the correct user</response>
        /// <response code="400">Bad Request, double check input</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{email}")]
        [ActionName(nameof(GetSingleUserAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DisplaySimpleUserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSingleUserAsync(string email, CancellationToken ct)
        {
            DomainUser? userFound = await _userService.FindUserByEmailAsync(email, ct);
            if (userFound is null) return NotFound();
            return Ok(userFound.MapToDisplaySimpleUserDTO());
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>User to Display Object</returns>
        /// <response code="201">Created, User created</response>
        [HttpPost]
        [ActionName(nameof(PostUserAsync))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostUserAsync([FromBody] RegisterUserDTO user, CancellationToken ct)
        {
            DisplaySimpleUserDTO? userCreated = await _userService.InsertUserAsync(user,ct);
            return CreatedAtAction(nameof(PostUserAsync), userCreated);
        }

        /// <summary>
        /// Delete an user with the provided email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>User to Display Object</returns>
        /// <response code="201">Created, User created</response>
        [HttpDelete("{email}")]
        [ActionName(nameof(DeleteUserAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync(string email, CancellationToken ct)
        {
            try
            {
                await _userService.RemoveUserAsync(email, ct);
            }
            catch(Exception ex)
            {
                return BadRequest(new JurnalaError()
                {
                    StatusCode = JurnalaStatusCodes.CODE_400,
                    Message = ex.Message,
                    ErrorType = JurnalaStatusCodes.BAD_REQUEST
                });
            }
            return NoContent();
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> PutUserAsync(string email, [FromBody] UpdateUserDTO user, CancellationToken ct)
        {
            UpdateUserDTO? userToReturn = await _userService.EditUserAsync(email, user, ct);
            return Ok();
        }
    }
}