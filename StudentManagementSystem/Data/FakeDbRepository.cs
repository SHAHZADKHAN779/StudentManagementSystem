using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagementSystem.Controllers;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
	public class FakeDbRepository : IStudentRepository
	{
		private List<Student> AllStudents = new List<Student>()
		{
			new Student{Id=1, Name="Student1", Email="student1@gmail.com", Phone="03111111111",	Class="Nursery", RollNo=78585},
			new Student{Id=2, Name="Student2", Email="student2@gmail.com", Phone="03222222222", Class="Kg", RollNo=12589},
			new Student{Id=3, Name="Student3", Email="student3@gmail.com", Phone="03333333333", Class="Class1", RollNo=25845},
			new Student{Id=4, Name="Student4", Email="student4@gmail.com", Phone="03444444444", Class="Class2", RollNo=25752},
			new Student{Id=5, Name="Student5", Email="student5@gmail.com", Phone="03555555555", Class="Class3", RollNo=25456},
		};

		public List<Student> GetAll() => AllStudents.OrderBy(student => student.Id).ToList();

		public Student? GetById(int id) => AllStudents.FirstOrDefault(student => student.Id == id);

		public void Add(Student student)
		{
			if (AllStudents.Count == 0)
			{
				student.Id = 1;
			}
			else
			{
				student.Id = AllStudents.Max(student => student.Id)+1;
			}
			AllStudents.Add(student);
		}

		public void Update (Student student)
		{
			var existing = GetById(student.Id);
			if (existing == null)
			{
				return;
			}
			else
			{
				existing.Name = student.Name;
				existing.Email = student.Email;
				existing.Phone = student.Phone;
				existing.Class = student.Class;
				existing.RollNo = student.RollNo;
			}

		}

		public void Delete (int id)
		{
			var delete = GetById(id);
			if (delete == null)
			{
				return;
			}
			else
			{
				AllStudents.Remove(delete);
			}
		}

		}
	}

