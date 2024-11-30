using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TaskManagementSystem.Models;

namespace TaskManagementSystem
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Models.Task> Tasks { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Налаштування User
			modelBuilder.Entity<User>()
				.HasKey(u => u.Id);

			modelBuilder.Entity<User>()
				.HasMany(u => u.Tasks)
				.WithOne(t => t.User)
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// Налаштування Task
			modelBuilder.Entity <Models.Task >()
				.HasKey(t => t.Id);

			modelBuilder.Entity<Models.Task>()
				.Property(t => t.Priority)
				.HasConversion<string>();
		}
	}
}
