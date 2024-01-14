using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Web.Models
{
  public class Timesheet
  {
    public int Id { get; set; }

    [DisplayName("Punct de lucru")]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    public int AssignmentId { get; set; }

    public Assignment? Assignment { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    public int EmployeeId { get; set; }

    [DisplayName("Ore lucrate")]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]

    public int Hours { get; set; }

    [DisplayName("Data")]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [DataType(DataType.Date)]

    public DateTime Date { get; set; }
  }
}
