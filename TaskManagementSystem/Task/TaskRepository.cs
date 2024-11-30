using System.Xml.Linq;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Task
{
	public class TaskRepository
	{
		private static IDictionary<Guid, List<Models.Task>> UserTasks = new Dictionary<Guid, List<Models.Task>>();

		public void AddTask(Guid userId, Models.Task task)
		{
			if (!UserTasks.ContainsKey(userId))
			{
				UserTasks[userId] = new List<Models.Task>();
			}
			UserTasks[userId].Add(task);
		}

		public IEnumerable<Models.Task> GetTasksByUser(Guid userId)
		{
			return UserTasks.TryGetValue(userId, out var tasks) ? tasks : Enumerable.Empty<Models.Task>();
		}

		public Models.Task? GetTaskById(Guid userId, Guid taskId)
		{
			return UserTasks.TryGetValue(userId, out var tasks)
				? tasks.FirstOrDefault(t => t.Id == taskId)
				: null;
		}

		public void UpdateTask(Guid userId, Models.Task task)
		{
			var existingTask = GetTaskById(userId, task.Id);
			if (existingTask != null)
			{
				task.UpdatedAt = DateTime.UtcNow;
				UserTasks[userId].Remove(existingTask);
				UserTasks[userId].Add(task);
			}
		}

		public void DeleteTask(Guid userId, Guid taskId)
		{
			var task = GetTaskById(userId, taskId);
			if (task != null)
			{
				UserTasks[userId].Remove(task);
			}
		}

	}
}
