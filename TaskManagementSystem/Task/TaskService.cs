using TaskManagementSystem.Models;

namespace TaskManagementSystem.Task
{
	public class TaskService
	{
		private readonly TaskRepository taskRepository;

		public TaskService(TaskRepository taskRepository)
		{
			this.taskRepository = taskRepository;
		}

		public void CreateTask(Guid userId, string title, string? description, DateTime? dueDate, TaskPriority priority)
		{
			var task = new Models.Task
			{
				Title = title,
				Description = description,
				DueDate = dueDate,
				Priority = priority,
				UserId = userId
			};

			taskRepository.AddTask(userId, task);
		}

		public IEnumerable<Models.Task> GetUserTasks(Guid userId)
		{
			return taskRepository.GetTasksByUser(userId);
		}

		public Models.Task? GetTaskById(Guid userId, Guid taskId)
		{
			return taskRepository.GetTaskById(userId, taskId);
		}

		public void UpdateTask(Guid userId, Models.Task updatedTask)
		{
			var existingTask = taskRepository.GetTaskById(userId, updatedTask.Id);
			if (existingTask == null)
			{
				throw new Exception("Task not found.");
			}

			if (existingTask.UserId != userId)
			{
				throw new Exception("Unauthorized to update this task.");
			}

			existingTask.Title = updatedTask.Title;
			existingTask.Description = updatedTask.Description;
			existingTask.DueDate = updatedTask.DueDate;
			existingTask.Priority = updatedTask.Priority;
			existingTask.Status = updatedTask.Status;
			existingTask.UpdatedAt = DateTime.UtcNow;

			taskRepository.UpdateTask(userId, updatedTask);
		}

		public void DeleteTask(Guid userId, Guid taskId)
		{
			var task = taskRepository.GetTaskById(userId, taskId);
			if (task == null)
			{
				throw new Exception("Task not found.");
			}

			if (task.UserId != userId)
			{
				throw new Exception("Unauthorized to delete this task.");
			}

			taskRepository.DeleteTask(userId, taskId);
		}

	}
}
