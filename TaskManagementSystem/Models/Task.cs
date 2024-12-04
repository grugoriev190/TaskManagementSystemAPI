using System.Text.Json.Serialization;

namespace TaskManagementSystem.Models
{
	public class Task
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime? DueDate { get; set; }
		public TaskStatus Status { get; set; } = TaskStatus.Pending;
		public TaskPriority Priority { get; set; } = TaskPriority.Medium;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public Guid UserId { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public User User { get; set; } = null!;
	}

	public enum TaskStatus { Pending, InProgress, Completed }
	public enum TaskPriority { Low, Medium, High }

}
