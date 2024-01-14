using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timesheet.Web.Data;
using Timesheet.Web.Models;
using Timesheet.Web.Utils;

namespace Timesheet.Web.Pages.Leaves
{
  public class CreateModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    public CreateModel(TimesheetDbContext context)
    {
      _context = context;
    }

    public IActionResult OnGet()
    {
      var leaveTypes = Enum.GetValues<LeaveType>().Select(e => new { Value = (int)e, Text = e.GetDescription() });      
      ViewData["LeaveTypes"] = new SelectList(leaveTypes, "Value", "Text");

      return Page();
    }

    [BindProperty]
    public Leave Leave { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Leave == null || Leave == null)
      {
        return Page();
      }

      Leave.EmployeeId = User.GetEmployeeId();

      _context.Leave.Add(Leave);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}
