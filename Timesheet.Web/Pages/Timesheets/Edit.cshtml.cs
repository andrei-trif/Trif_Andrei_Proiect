using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;

namespace Timesheet.Web.Pages.Timesheets
{
  public class EditModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public EditModel(TimesheetDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public Models.Timesheet Timesheet { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Timesheet == null)
      {
        return NotFound();
      }

      var timesheet = await _context.Timesheet.FirstOrDefaultAsync(m => m.Id == id);
      if (timesheet == null)
      {
        return NotFound();
      }
      Timesheet = timesheet;

      ViewData["AssignmentId"] = new SelectList(
       _context.Assignment
           .Include(a => a.Employee)
           .Include(a => a.Location)
           .ThenInclude(l => l.Client)
          .Where(a => a.EmployeeId == timesheet.EmployeeId), "Id", "Location.FullName");

      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(Timesheet).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TimesheetExists(Timesheet.Id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return RedirectToPage("./Index");
    }

    private bool TimesheetExists(int id)
    {
      return (_context.Timesheet?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
