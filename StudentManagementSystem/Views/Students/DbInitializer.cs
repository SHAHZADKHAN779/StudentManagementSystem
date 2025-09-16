using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
	public static class DbInitializer
	{
		public static void Seed(AppDbContext context)
		{
			// Agar table me already data hai to kuch mat karo
			if (context.Students.Any())
				return;

			var students = new List<Student>
			{
				new Student{Name="Student1", Email="student1@gmail.com", Phone="03111111111", Class="Nursery", RollNo=78585},
				new Student{Name="Student2", Email="student2@gmail.com", Phone="03222222222", Class="Kindergarten", RollNo=12589},
				new Student{Name="Student3", Email="student3@gmail.com", Phone="03333333333", Class="Class1", RollNo=25845},
				new Student{Name="Student4", Email="student4@gmail.com", Phone="03444444444", Class="Class2", RollNo=25752},
				new Student{Name="Student5", Email="student5@gmail.com", Phone="03555555555", Class="Class3", RollNo=25456}
			};

			context.Students.AddRange(students);
			context.SaveChanges();
		}
	}
}
