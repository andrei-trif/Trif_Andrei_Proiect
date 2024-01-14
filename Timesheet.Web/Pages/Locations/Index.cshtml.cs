using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Locations
{
  public class IndexModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public IndexModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IList<Location> Location { get; set; } = default!;

    public async Task OnGetAsync()
    {
      if (_context.Location != null)
      {
        Location = await _context.Location
        .Include(l => l.Client).ToListAsync();
      }
    }
  }
}
