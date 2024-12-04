using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagementSystem.Auth;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController(AccountService accountService) : ControllerBase
	{
		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterUserRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.UserName) ||
				string.IsNullOrWhiteSpace(request.Email) ||
				string.IsNullOrWhiteSpace(request.Password))
			{
				return BadRequest(new { message = "All fields are required." });
			}

			try
			{
				accountService.Register(request.UserName, request.Email, request.Password);
				return Ok($"User registred.");
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Identifier) ||
				string.IsNullOrWhiteSpace(request.Password))
			{
				return BadRequest(new { message = "Identifier and password are required." });
			}

			try
			{
				var token = accountService.Login(request.Identifier, request.Password);
				return Ok(new { token });
			}
			catch (Exception ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}

		[HttpDelete("{userId}")]
		public IActionResult DeleteAccount(Guid userId)
		{
			var currentUserId = User.FindFirst("id")?.Value;
			if (currentUserId == null)
			{
				return Unauthorized(new { message = "Token is missing or invalid." });
			}

			if (Guid.Parse(currentUserId) != userId)
			{
				return Forbid("You are not allowed to delete this account.");
			}

			try
			{
				accountService.DeleteAccount(userId);
				return Ok(new { message = "Account deleted successfully." });
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}

		[HttpPut("{userId}")]
		public IActionResult UpdateAccount(Guid userId, [FromBody] UpdateUserRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.UserName) ||
				string.IsNullOrWhiteSpace(request.Email) ||
				string.IsNullOrWhiteSpace(request.Password))
			{
				return BadRequest(new { message = "All fields are required." });
			}

			var currentUserId = User.FindFirst("id")?.Value;
			if (currentUserId == null)
			{
				return Unauthorized(new { message = "Token is missing or invalid." });
			}

			if (Guid.Parse(currentUserId) != userId)
			{
				return Forbid("You are not allowed to update this account.");
			}

			try
			{
				accountService.UpdateAccount(userId, request.UserName, request.Email, request.Password);
				return Ok(new { message = "Account updated successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
