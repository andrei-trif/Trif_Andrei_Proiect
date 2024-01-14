using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;
using Timesheet.Web.Utils;

namespace Timesheet.Web.Pages.Leaves
{
  public class EditModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public EditModel(TimesheetDbContext context)
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

      Leave = leave;

      var leaveTypes = Enum.GetValues<LeaveType>().Select(e => new { Value = (int)e, Text = e.GetDescription() });
      ViewData["LeaveTypes"] = new SelectList(leaveTypes, "Value", "Text");

      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(Leave).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LeaveExists(Leave.Id))
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

    private bool LeaveExists(int id)
    {
      return (_context.Leave?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
