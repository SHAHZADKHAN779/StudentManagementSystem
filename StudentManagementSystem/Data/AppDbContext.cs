using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext (DbContextOptions<AppDbContext>options) : base (options)
		{

		}

		public DbSet <Student> Students { get; set; }

		public DbSet<StudentResult> StudentResults { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// optional: FK config (EF usually detects it)
			modelBuilder.Entity<StudentResult>()
				.HasOne(r => r.Student)
				.WithMany(s => s.Results)
				.HasForeignKey(r => r.StudentId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
