using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Assignments
{
  public class IndexModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public IndexModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IList<Assignment> Assignment { get; set; } = default!;

    public async Task OnGetAsync()
    {
      if (_context.Assignment != null)
      {
        Assignment = await _context.Assignment
        .Include(a => a.Employee)
        .Include(a => a.Location)
          .ThenInclude(l => l.Client)
        .ToListAsync();
      }
    }
  }
}
