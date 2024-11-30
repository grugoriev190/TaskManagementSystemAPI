using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagementSystem.Models;
using TaskManagementSystem.Task;

namespace TaskManagementSystem.Controllers
{

	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class TasksController : Controller
	{
		private readonly TaskService taskService;

		public TasksController(TaskService taskService)
		{
			this.taskService = taskService;
		}

		[HttpPost]
		public IActionResult CreateTask(TaskRequest request)
		{
			var userId = User.FindFirst("id")?.Value;
			if (userId == null)
			{
				return Unauthorized();
			}

			if (!Enum.TryParse<TaskPriority>(request.Priority, true, out var priority))
			{
				return BadRequest("Invalid priority value. Allowed values: Low, Medium, High.");
			}

			taskService.CreateTask(Guid.Parse(userId), request.Title, request.Description, request.DueDate, priority);
			return Ok();
		}

		[HttpGet]
		public IActionResult GetTasks()
		{
			var userId = User.FindFirst("id")?.Value;
			if (userId == null)
			{
				return Unauthorized();
			}

			var tasks = taskService.GetUserTasks(Guid.Parse(userId));
			return Ok(tasks);
		}

		[HttpGet("{taskId}")]
		public IActionResult GetTaskById(Guid taskId)
		{
			var userId = User.FindFirst("id")?.Value;
			if (userId == null)
			{
				return Unauthorized();
			}

			var task = taskService.GetTaskById(Guid.Parse(userId), taskId);
			if (task == null)
			{
				return NotFound();
			}

			return Ok(task);
		}

		[HttpPut("{taskId}")]
		public IActionResult UpdateTask(Guid taskId, TaskRequest request)
		{
			var userId = User.FindFirst("id")?.Value;
			if (userId == null)
			{
				return Unauthorized();
			}

			var priority = Enum.TryParse<TaskPriority>(request.Priority, true, out var parsedPriority)
				? parsedPriority
				: TaskPriority.Medium;

			var status = Enum.TryParse<Models.TaskStatus>(request.Status, true, out var parsedStatus)
				? parsedStatus
				: Models.TaskStatus.Pending;

			var updatedTask = new Models.Task
			{
				Id = taskId,
				Title = request.Title,
				Description = request.Description,
				DueDate = request.DueDate,
				Priority = priority,
				Status = status,
				UserId = Guid.Parse(userId)
			};

			try
			{
				var existingTask = taskService.GetTaskById(Guid.Parse(userId), taskId);
				if (existingTask == null)
				{
					return NotFound($"Task with id {taskId} not found.");
				}

				taskService.UpdateTask(Guid.Parse(userId), updatedTask);
				return Ok(updatedTask);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = ex.Message });
			}
		}

		[HttpDelete("{taskId}")]
		public IActionResult DeleteTask(Guid taskId)
		{
			var userId = User.FindFirst("id")?.Value;
			if (userId == null)
			{
				return Unauthorized();
			}

			try
			{
				taskService.DeleteTask(Guid.Parse(userId), taskId);
				return Ok(new { message = "Task deleted successfully." });
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}
	}
}
