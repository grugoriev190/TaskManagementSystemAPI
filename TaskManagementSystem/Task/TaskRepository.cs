using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Task
{
	public class TaskRepository
	{
		private readonly AppDbContext dbContext;

		public TaskRepository(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public void AddTask(Guid userId, Models.Task task)
		{
			dbContext.Tasks.Add(task);
			dbContext.SaveChanges();
		}

		public IEnumerable<Models.Task> GetTasksByUser(Guid userId)
		{
			return dbContext.Tasks.Where(t => t.UserId == userId).ToList();
		}

		public Models.Task? GetTaskById(Guid taskId)
		{
			return dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);
		}

		public void UpdateTask(Guid userId, Models.Task task)
		{
			var existingTask = dbContext.Tasks.Local.FirstOrDefault(t => t.Id == task.Id);
			if (existingTask != null)
			{
				// Якщо є, від'єднуємо її, щоб не було конфлікту
				dbContext.Entry(existingTask).State = EntityState.Detached;
			}

			dbContext.Tasks.Update(task);
			dbContext.SaveChanges();
		}

		public void DeleteTask(Guid taskId)
		{
			var task = GetTaskById(taskId);
			if (task != null)
			{
				dbContext.Tasks.Remove(task);
				dbContext.SaveChanges();
			}
		}

		public IEnumerable<Models.Task> FilterTasks(Guid userId, Models.TaskStatus? status, DateTime? dueDate, TaskPriority? priority)
		{
			return dbContext.Tasks
			.Where(t => t.UserId == userId &&
						(!status.HasValue || t.Status == status) &&
						(!dueDate.HasValue || t.DueDate == dueDate) &&
						(!priority.HasValue || t.Priority == priority))
			.ToList();
		}


	}
}
