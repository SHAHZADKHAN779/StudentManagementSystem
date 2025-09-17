namespace StudentManagementSystem.Models
{
	public class StudentSummaryViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int SubjectsCount { get; set; }
		public int TotalMarks { get; set; }
		public int MaxMarks { get; set; }
		public double Percentage { get; set; }
	}
}
