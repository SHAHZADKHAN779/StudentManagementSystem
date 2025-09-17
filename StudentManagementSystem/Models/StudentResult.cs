using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
	public class StudentResult
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please select a student.")]
		[Display(Name = "Student")]
		public int StudentId { get; set; }

		[Required]
		[StringLength(100)]
		public string Subject { get; set; }

		[Required]
		[Range(0, 1000)]
		public int Marks { get; set; }

		[Required]
		[Range(1, 1000)]
		public int MaxMarks { get; set; } = 100;

		// Navigation
		public Student? Student { get; set; }
	}
}
