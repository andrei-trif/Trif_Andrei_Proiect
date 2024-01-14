using System.Security.Claims;

namespace Timesheet.Web.Utils
{
  public static class ClaimsPrincipalExtension
  {
    public static int GetEmployeeId(this ClaimsPrincipal claimsPrincipal)
    {
      const string claimType = "employee-id";

      var claim = claimsPrincipal.FindFirstValue(claimType);  
      
      return int.Parse(claim);
    }
  }
}
