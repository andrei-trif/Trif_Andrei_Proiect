using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Locations
{
  public class EditModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public EditModel(TimesheetDbContext context)
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

      Location = location;
      ViewData["Clientid"] = new SelectList(_context.Client, "Id", "Name");

      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(Location).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LocationExists(Location.Id))
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

    private bool LocationExists(int id)
    {
      return (_context.Location?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
