using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Employees
{
  public class IndexModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public IndexModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IList<Employee> Employee { get; set; } = default!;

    public async Task OnGetAsync()
    {
      if (_context.Employee != null)
      {
        Employee = await _context.Employee.ToListAsync();
      }
    }
  }
}
