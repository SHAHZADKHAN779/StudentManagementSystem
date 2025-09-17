using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
	public class ResultsController : Controller
	{
		private readonly AppDbContext _context;

		public ResultsController(AppDbContext context)
		{
			_context = context;
		}

		// Index: list all results with optional filtering
		public async Task<IActionResult> Index(int? studentId)
		{
			var resultsQuery = _context.StudentResults
									   .Include(r => r.Student)
									   .AsQueryable();

			if (studentId.HasValue)
				resultsQuery = resultsQuery.Where(r => r.StudentId == studentId.Value);

			var results = await resultsQuery
									.OrderBy(r => r.Student.Name)
									.ThenBy(r => r.Subject)
									.ToListAsync();

			ViewData["Students"] = new SelectList(_context.Students.OrderBy(s => s.Name), "Id", "Name", studentId);

			return View(results);
		}

		// Create GET
		[HttpGet]
		public IActionResult Create()
		{
			ViewData["Students"] = new SelectList(_context.Students.OrderBy(s => s.Name), "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(StudentResult result)
		{
			// Custom validation: Marks <= MaxMarks
			if (result.Marks > result.MaxMarks)
			{
				ModelState.AddModelError("Marks", "Marks cannot be greater than Max Marks.");
			}

			// Check ModelState.IsValid
			if (!ModelState.IsValid)
			{
				// Let's see what errors we have
				var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				// You can log these errors or display them for debugging
				// For now, we'll add a temporary debug message (remove in production)
				ViewBag.ErrorMessages = errors;
			}
			else
			{
				_context.StudentResults.Add(result);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["Students"] = new SelectList(_context.Students.OrderBy(s => s.Name), "Id", "Name", result.StudentId);
			return View(result);
		}

		// Edit GET
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var result = await _context.StudentResults.FindAsync(id);
			if (result == null) return NotFound();

			ViewData["Students"] = new SelectList(_context.Students.OrderBy(s => s.Name), "Id", "Name", result.StudentId);
			return View(result);
		}

		// Edit POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, StudentResult result)
		{
			if (id != result.Id) return NotFound();

			if (result.Marks > result.MaxMarks)
				ModelState.AddModelError("Marks", "Marks cannot be greater than Max Marks.");

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(result);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _context.StudentResults.AnyAsync(r => r.Id == result.Id))
						return NotFound();
					else
						throw;
				}
			}

			ViewData["Students"] = new SelectList(_context.Students.OrderBy(s => s.Name), "Id", "Name", result.StudentId);
			return View(result);
		}

		// Delete GET
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();

			var result = await _context.StudentResults
									   .Include(r => r.Student)
									   .FirstOrDefaultAsync(r => r.Id == id);

			if (result == null) return NotFound();

			return View(result);
		}

		// Delete POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var result = await _context.StudentResults.FindAsync(id);
			if (result != null)
			{
				_context.StudentResults.Remove(result);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		// Details
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var result = await _context.StudentResults
									   .Include(r => r.Student)
									   .FirstOrDefaultAsync(r => r.Id == id);

			if (result == null) return NotFound();

			return View(result);
		}
	}
}
