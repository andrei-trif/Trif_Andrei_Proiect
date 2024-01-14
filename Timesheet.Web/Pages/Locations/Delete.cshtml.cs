using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Locations
{
  public class DeleteModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public DeleteModel(TimesheetDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public Location Location { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Location == null)
      {
        return NotFound();
      }

      var location = await _context.Location.FirstOrDefaultAsync(m => m.Id == id);

      if (location == null)
      {
        return NotFound();
      }
      else
      {
        Location = location;
      }

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Location == null)
      {
        return NotFound();
      }

      var location = await _context.Location.FindAsync(id);

      if (location != null)
      {
        Location = location;
        _context.Location.Remove(Location);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
