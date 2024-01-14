using Microsoft.EntityFrameworkCore;
using Timesheet.Web.Models;

namespace Timesheet.Web.Data
{
  public class TimesheetDbContext : DbContext
  {
    public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options) {}

    public DbSet<Client> Client { get; set; } = default!;

    public DbSet<Location> Location { get; set; } = default!;

    public DbSet<Employee> Employee { get; set; } = default!;

    public DbSet<Assignment> Assignment { get; set; } = default!;

    public DbSet<Models.Timesheet> Timesheet { get; set; } = default!;

    public DbSet<Leave> Leave { get; set; } = default!;

  }
}
