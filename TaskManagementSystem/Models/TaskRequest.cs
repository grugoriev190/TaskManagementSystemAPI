namespace TaskManagementSystem.Models
{
	public class TaskRequest
	{
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime? DueDate { get; set; }
		public string Status { get; set; } = "Pending";
		public string Priority { get; set; } = "Low";
	}
}
