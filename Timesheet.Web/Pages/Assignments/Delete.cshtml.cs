using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Assignments
{
  public class DeleteModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public DeleteModel(TimesheetDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public Assignment Assignment { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Assignment == null)
      {
        return NotFound();
      }

      var assignment = await _context.Assignment
        .Include(a => a.Employee)
        .Include(a => a.Location)
          .ThenInclude(l => l.Client)
        .FirstOrDefaultAsync(m => m.Id == id);

      if (assignment == null)
      {
        return NotFound();
      }
      else
      {
        Assignment = assignment;
      }

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Assignment == null)
      {
        return NotFound();
      }
      var assignment = await _context.Assignment.FindAsync(id);

      if (assignment != null)
      {
        Assignment = assignment;
        _context.Assignment.Remove(Assignment);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
