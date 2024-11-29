using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.Controllers
{

	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class TaskController : Controller
	{
		[HttpGet]
		public IActionResult GetTask()
		{
			return Ok(new List<object>
			{
				new
				{
					Name = "SOME TASK",
					Duration = TimeSpan.FromHours(2)
				},
				new
				{
					Name = "SOME TASK 2 ",
					Duration = TimeSpan.FromHours(3)
				}

			});
		}
	}
}
