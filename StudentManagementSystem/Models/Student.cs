using StudentManagementSystem.Controllers;
using StudentManagementSystem.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
	public class Student
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name Required")]
		[StringLength(100, ErrorMessage = "Name Length Cant Exceed 100 Characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email Required")]
		[EmailAddress(ErrorMessage = "Correct Email Format Required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone Required")]
		[Phone(ErrorMessage = "Correct Phone Format Required.")]
		public string Phone { get; set; }

		public string Class { get; set; }

		public int RollNo { get; set; }


	}
}
