using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
	public class StudentsController : Controller
	{
		private readonly AppDbContext _context;

		public StudentsController (AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index(string search, string SortOrder)
		{
			ViewBag.NameSort = SortOrder == "Ascending" ? "Descending" : "Ascending";

			var students = _context.Students.ToList();

			if (!string.IsNullOrEmpty(search))
				{
				students = students.Where(student => student.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||student.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||student.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
			}

		
			switch (SortOrder)
			{
				case "Ascending":
					students = students.OrderBy(student => student.Name).ToList();
					break;

				case "Descending":
					students = students.OrderByDescending(student => student.Name).ToList();
					break;
			}
			return View(students);
		}

		[HttpGet]

		public IActionResult Create() 
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Student student)
		{
			if (ModelState.IsValid)
			{
				_context.Students.Add(student);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(student);
			}
		}

		[HttpGet]

		public IActionResult Edit(int id)
		{
			var edit = _context.Students.Find(id);
			if (edit == null)
			{
				return NotFound();
			}

			else
			{
				return View(edit);
			}
		}

		[HttpPost]
		public IActionResult Edit (Student student)
		{
			if (ModelState.IsValid)
			{
				_context.Students.Update(student);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			else
			{
				return View(student);
			}
		}

		[HttpGet]
		public IActionResult Delete (int id)
		{
			var delete = _context.Students.Find(id);
			if (delete == null)
			{
				return NotFound();
			}
			else
			{
				return View(delete);
			}
		}

		[HttpPost, ActionName("Delete")]

		public IActionResult DeleteConfirmed(int id)
		{
			var delete = _context.Students.Find(id);
			if (delete == null)
			{
				return NotFound();
			}
			else
			{
				_context.Students.Remove(delete);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
		}

		public IActionResult Details(int id)
		{
			var details = _context.Students.Find(id);
			if (details == null)
			{
				return NotFound();
			}
			else
			{
				return View(details);
			}
		}

		public async Task<IActionResult> Summary()
		{
			var students = await _context.Students.Include(s => s.Results).ToListAsync();

			var model = students.Select(s => new StudentSummaryViewModel
			{
				Id = s.Id,
				Name = s.Name,
				SubjectsCount = s.Results.Count,
				TotalMarks = s.Results.Sum(r => r.Marks),
				MaxMarks = s.Results.Sum(r => r.MaxMarks),
				Percentage = (s.Results.Sum(r => r.MaxMarks) > 0) ?
					Math.Round((double)s.Results.Sum(r => r.Marks) / s.Results.Sum(r => r.MaxMarks) * 100, 2) : 0
			}).ToList();

			return View(model);
		}


	}
}
