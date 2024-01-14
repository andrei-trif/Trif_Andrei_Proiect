using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Web.Models
{
  public class Leave
  {
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [DisplayName("Tip")]
    public LeaveType LeaveType { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [DisplayName("Data de inceput")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [DisplayName("Data de sfarsit")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
  }

  public enum LeaveType
  {
    [Description("Concediu de odihna")]
    Paid = 0,
    [Description("Concediu medical")]
    Medical = 1,
    [Description("Zi nationala libera")]
    PublicHoliday = 2,
  }
}
