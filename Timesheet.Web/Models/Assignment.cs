using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Web.Models
{
  public class Assignment
  {
    public int Id { get; set; }

    [Required]
    [DisplayName("Angajat")]
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    [Required]
    [DisplayName("Locatie")]
    public int LocationId { get; set; }

    public Location? Location { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [DisplayName("Necesar ore")]
    public int Hours { get; set; }
  }
}
