using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;
using Timesheet.Web.Utils;

namespace Timesheet.Web.Pages.Leaves
{
  public class IndexModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public IndexModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IList<Leave> Leave { get; set; } = default!;

    public async Task OnGetAsync()
    {
      if (_context.Leave != null)
      {
        Leave = await _context.Leave.Where(l => l.EmployeeId == User.GetEmployeeId()).ToListAsync();
      }
    }
  }
}
