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

namespace Timesheet.Web.Pages.Assignments
{
  public class EditModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public EditModel(TimesheetDbContext context)
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

      var assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.Id == id);
      if (assignment == null)
      {
        return NotFound();
      }
      Assignment = assignment;
      ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
      ViewData["LocationId"] = new SelectList(_context.Location.Include(l => l.Client), "Id", "FullName");
      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Attach(Assignment).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AssignmentExists(Assignment.Id))
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

    private bool AssignmentExists(int id)
    {
      return (_context.Assignment?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
