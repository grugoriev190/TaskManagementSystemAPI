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
	}
}
