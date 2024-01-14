using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Web.Models
{
  public class Client
  {
    public int Id { get; set; }

    [DisplayName("Nume")]
    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(100)]
    public string Name { get; set; }

    [DisplayName("Locatii")]
    public ICollection<Location> Locations { get; } = new List<Location>();
  }
}
