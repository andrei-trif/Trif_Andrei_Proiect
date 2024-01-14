using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Clients
{
  public class EditModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public EditModel(TimesheetDbContext context)
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

      Client = client;
      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(Client).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ClientExists(Client.Id))
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

    private bool ClientExists(int id)
    {
      return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
