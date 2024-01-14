using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Clients
{
  public class DeleteModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public DeleteModel(TimesheetDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Client == null)
      {
        return NotFound();
      }

      var client = await _context.Client.FirstOrDefaultAsync(m => m.Id == id);

      if (client == null)
      {
        return NotFound();
      }
      else
      {
        Client = client;
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Client == null)
      {
        return NotFound();
      }
      var client = await _context.Client.FindAsync(id);

      if (client != null)
      {
        Client = client;
        _context.Client.Remove(Client);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
