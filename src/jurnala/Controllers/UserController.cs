using Application.Interfaces.IServices;
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

        [HttpGet("{email}")]
        public async Task<IActionResult> GetSingleUserAsync(string email, CancellationToken ct)
        {
            DomainUser? userFound = await _userService.FindUserByEmailAsync(email, ct);
            if (userFound is null) return NotFound();
            return Ok(userFound.MapToDisplaySimpleUserDTO());
        }

        [HttpPost]
        [ActionName(nameof(PostUserAsync))]
        public async Task<IActionResult> PostUserAsync([FromBody] RegisterUserDTO user, CancellationToken ct)
        {
            DisplaySimpleUserDTO? userCreated = await _userService.InsertUserAsync(user,ct);
            return CreatedAtAction(nameof(PostUserAsync), userCreated);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUserAsync(string email, CancellationToken ct)
        {
            await _userService.RemoveUserAsync(email, ct);
            return NoContent();
        }
    }
}