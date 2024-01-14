using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Data;
using Timesheet.Web.Models;
using Timesheet.Web.Utils;

namespace Timesheet.Web.Pages.Timesheets
{
  public class CreateModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public CreateModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public DateTime Date { get; set; }

    public IActionResult OnGet(int year, int month, int day)
    {
      Timesheet = new Models.Timesheet 
      { 
        Date = new DateTime(year, month, day), 
        EmployeeId = User.GetEmployeeId() 
      };

      ViewData["AssignmentId"] = new SelectList(
        _context.Assignment
            .Include(a => a.Employee)
            .Include(a => a.Location)
            .ThenInclude(l => l.Client)
           .Where(a => a.EmployeeId == User.GetEmployeeId()), "Id", "Location.FullName");

      return Page();
    }

    [BindProperty]
    public Models.Timesheet Timesheet { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Timesheet == null || Timesheet == null)
      {
        return Page();
      }

      _context.Timesheet.Add(Timesheet);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}
