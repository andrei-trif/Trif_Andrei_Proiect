using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Assignments
{
  public class CreateModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public CreateModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IActionResult OnGet()
    {
      ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
      ViewData["LocationId"] = new SelectList(_context.Location.Include(l => l.Client), "Id", "FullName");
      return Page();
    }

    [BindProperty]
    public Assignment Assignment { get; set; } = default!;


    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Assignment == null || Assignment == null)
      {
        return Page();
      }

      _context.Assignment.Add(Assignment);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}
