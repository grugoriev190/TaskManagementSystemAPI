using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Auth;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{

    [ApiController]
	[Route("[controller]")]
	public class AuthController(AccountService accountService) : ControllerBase
	{
		[HttpPost("register")]
		public IActionResult Register([FromBody]RegisterUserReqest reqest)
		{
			accountService.Register(reqest.UserName, reqest.Email, reqest.Password);
			return NoContent();
		}

		[HttpPost("login")]

		public IActionResult Login([FromBody] LoginRequest loginReqest)
		{
			var token = accountService.Login(loginReqest.Identifier, loginReqest.Password);
			return Ok(token);
		}

		[HttpDelete("{userId}")]
		public IActionResult DeleteAccount(Guid userId)
		{
			var currentUserId = User.FindFirst("id")?.Value;
			if (currentUserId == null)
			{
				return Unauthorized();
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
			// Отримання ідентифікатора поточного користувача із токена
			var currentUserId = User.FindFirst("id")?.Value;
			if (currentUserId == null)
			{
				Console.WriteLine("Unauthorized: Token is missing or invalid.");
				return Unauthorized(new { message = "Token is missing or invalid." });
			}


			// Перевірка, чи користувач має право оновлювати акаунт (наприклад, свій власний акаунт)
			if (Guid.Parse(currentUserId) != userId)
			{
				return Forbid("You are not allowed to update this account.");
			}

			try
			{
				// Виклик сервісу для оновлення акаунта
				accountService.UpdateAccount(
					userId,
					request.UserName,
					request.Email,
					request.Password
				);
				return Ok(new { message = "Account updated successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

	}
}
