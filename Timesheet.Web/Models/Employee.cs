using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheet.Web.Models
{
  public class Employee
  {
    public int Id { get; set; }

    [DisplayName("Prenume")]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    public string FirstName { get; set; }

    [DisplayName("Nume")]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    [NotMapped]
    [EmailAddress]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    public string Email { set; get; }

    [DisplayName("Repartizari")]
    public List<Assignment>? Assignments { get; set; }

    [DisplayName("Pontaje")]
    public List<Timesheet>? Timesheets { get; set; }

    [DisplayName("Concedii")]
    public List<Leave>? Leaves { get; set; }
  }
}
