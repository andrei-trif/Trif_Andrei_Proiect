using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Employees
{
  public class DeleteModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public DeleteModel(TimesheetDbContext context)
    {
      _context = context;
    }

    [BindProperty]
    public Employee Employee { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Employee == null)
      {
        return NotFound();
      }

      var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);

      if (employee == null)
      {
        return NotFound();
      }
      else
      {
        Employee = employee;
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Employee == null)
      {
        return NotFound();
      }
      var employee = await _context.Employee.FindAsync(id);

      if (employee != null)
      {
        Employee = employee;
        _context.Employee.Remove(Employee);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}
