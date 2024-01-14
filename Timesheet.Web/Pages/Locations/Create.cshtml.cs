using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Locations
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
      ViewData["Clientid"] = new SelectList(_context.Client, "Id", "Name");
      return Page();
    }

    [BindProperty]
    public Location Location { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Location == null || Location == null)
      {
        return Page();
      }

      _context.Location.Add(Location);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}
