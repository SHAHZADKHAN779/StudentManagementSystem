using StudentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StudentManagementSystem.Data
{
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json").Build();

			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}
