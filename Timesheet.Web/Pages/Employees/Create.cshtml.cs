using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Timesheet.Web.Data;
using Timesheet.Web.Models;

namespace Timesheet.Web.Pages.Employees
{
  public class CreateModel : PageModel
  {
    private readonly TimesheetDbContext _context;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;

    public CreateModel(
        TimesheetDbContext context,
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore)
    {
      _context = context;
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
    }

    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public Employee Employee { get; set; } = default!;


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.Employee == null || Employee == null)
      {
        return Page();
      }

      _context.Employee.Add(Employee);
      await _context.SaveChangesAsync();

      var user = new IdentityUser(Employee.Email);
      
      await _userStore.SetUserNameAsync(user, Employee.Email, CancellationToken.None);
      await _emailStore.SetEmailAsync(user, Employee.Email, CancellationToken.None);
      
      var result = await _userManager.CreateAsync(user, "Pass123!");
      if (result.Succeeded) {
        await _userManager.AddToRoleAsync(user, "Employee");
        await _userManager.AddClaimAsync(user, new Claim("employee-id", Employee.Id.ToString()));
      }

      return RedirectToPage("./Index");
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
      if (!_userManager.SupportsUserEmail)
      {
        throw new NotSupportedException("The default UI requires a user store with email support.");
      }
      return (IUserEmailStore<IdentityUser>)_userStore;
    }
  }
}
