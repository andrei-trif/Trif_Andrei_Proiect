using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Leaves
{
  public class DeleteModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public DeleteModel(TimesheetDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public Leave Leave { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Leave == null)
      {
        return NotFound();
      }

      var leave = await _context.Leave.FirstOrDefaultAsync(m => m.Id == id);

      if (leave == null)
      {
        return NotFound();
      }
      else
      {
        Leave = leave;
      }

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Leave == null)
      {
        return NotFound();
      }
      var leave = await _context.Leave.FindAsync(id);

      if (leave != null)
      {
        Leave = leave;
        _context.Leave.Remove(Leave);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
