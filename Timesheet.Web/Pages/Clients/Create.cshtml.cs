using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Clients
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
      return Page();
    }

    [BindProperty]
    public Client Client { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Client == null || Client == null)
      {
        return Page();
      }

      _context.Client.Add(Client);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}
