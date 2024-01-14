using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Timesheet.Web.Data;

namespace Timesheet.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddDbContext<TimesheetDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("TimesheetDbContext") 
            ?? throw new InvalidOperationException("Connection string 'TimesheetDbContext' not found.")));

      builder.Services.AddDbContext<IdentityDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TimesheetDbContext")
          ?? throw new InvalidOperationException("Connection string 'TimesheetDbContext' not found.")));

      // Add services to the container.
      builder.Services.AddDefaultIdentity<IdentityUser>(options =>
      {
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
      }).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();

      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
        options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
        options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("Employee"));
      });

      builder.Services.AddRazorPages(options =>
      {
        options.Conventions.AuthorizeFolder("/Timesheets");
        options.Conventions.AuthorizeFolder("/Leaves");

        options.Conventions.AuthorizeFolder("/Clients", "ManagerPolicy");
      });

      var app = builder.Build();

      // Register middleware
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
                  app.UseAuthentication();;
      app.UseAuthorization();
      app.MapRazorPages();

      app.Run();
    }
  }
}