﻿namespace TaskManagementSystem.Models
{
	public class User
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string UserName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}

}
