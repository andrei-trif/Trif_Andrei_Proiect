using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;

namespace Timesheet.Web.Pages.Timesheets
{
  public class DeleteModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public DeleteModel(TimesheetDbContext context)
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

      var timesheet = await _context.Timesheet
        .Include(t => t.Assignment)
          .ThenInclude(a => a.Location)
            .ThenInclude(l => l.Client)
        .FirstOrDefaultAsync(m => m.Id == id);

      if (timesheet == null)
      {
        return NotFound();
      }
      else
      {
        Timesheet = timesheet;
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Timesheet == null)
      {
        return NotFound();
      }
      var timesheet = await _context.Timesheet.FindAsync(id);

      if (timesheet != null)
      {
        Timesheet = timesheet;
        _context.Timesheet.Remove(Timesheet);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
