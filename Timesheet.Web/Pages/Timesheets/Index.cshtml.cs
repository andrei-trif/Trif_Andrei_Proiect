using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;
using Timesheet.Web.Utils;

namespace Timesheet.Web.Pages.Timesheets
{
  public class IndexModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    private readonly int _currentYear;
    private readonly int _currentMonth;

    public IndexModel(TimesheetDbContext context)
    {
      _context = context;

      _currentYear = DateTime.Today.Year;
      _currentMonth = DateTime.Today.Month;
    }

    public DateTime StartDate;
    public DateTime EndDate;

    public DateTime PrevStartDate;
    public DateTime NextStartDate;

    public Employee Employee { get; set; } = default!;
    public IList<Assignment> Assignments { get; set; } = default!;

    public IList<Leave> Leaves { get; set; } = default!;
    public IList<Models.Timesheet> Timesheets { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? year, int? month)
    {
      if (year == null || month == null)
      {
        StartDate = FirstDayOfMonth(_currentYear, _currentMonth);
        EndDate = LastDayOfMonth(_currentYear, _currentMonth);
      }
      else
      {
        StartDate = FirstDayOfMonth((int)year, (int)month);
        EndDate = LastDayOfMonth((int)year, (int)month);
      }

      PrevStartDate = StartDate.AddMonths(-1);
      NextStartDate = StartDate.AddMonths(+1);


      if (_context.Employee != null) 
      {
        Employee = await _context.Employee
          .Include(e => e.Leaves.Where(t => t.StartDate >= StartDate && t.EndDate <= EndDate))
          .Include(e => e.Timesheets.Where(t => t.Date >= StartDate && t.Date <= EndDate))
            .ThenInclude(t => t.Assignment)
            .ThenInclude(a => a.Location)
            .ThenInclude(l => l.Client)
          .AsNoTracking()
          .FirstOrDefaultAsync(e => e.Id == User.GetEmployeeId());
      }

      if (Employee == null)
      {
        return NotFound();
      }

      return Page();
    }

    private static DateTime FirstDayOfMonth(int year, int month)
    {
      return new DateTime(year, month, 1);
    }

    private static DateTime LastDayOfMonth(int year, int month)
    {
      return new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
    }
  }
}
