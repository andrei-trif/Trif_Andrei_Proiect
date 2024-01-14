using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Clients
{
  public class IndexModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public IndexModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IList<Client> Client { get; set; } = default!;

    public async Task OnGetAsync()
    {
      if (_context.Client != null)
      {
        Client = await _context.Client.ToListAsync();
      }
    }
  }
}
