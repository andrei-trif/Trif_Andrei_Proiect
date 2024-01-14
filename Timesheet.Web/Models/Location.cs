using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Web.Models
{
  public class Location
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [DisplayName("Client")]
    public int Clientid { get; set; }

    public Client? Client { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(50)]
    [DisplayName("Locatie")]
    public string Name { get; set; }

    public string FullName => $"{Client.Name} - {Name}";

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(100)]
    [DisplayName("Strada")]
    public string StreetName { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(10)]
    [DisplayName("Nr.")]
    public string SteetNumber { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(10)]
    [DisplayName("Cod Postal")]
    public string SteetCode { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(100)]
    [DisplayName("Cladire")]
    public string Building { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(50)]
    [DisplayName("Oras")]
    public string City { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(2)]
    [DisplayName("Judet")]
    public string County { get; set; }

    [Required(ErrorMessage = "Campul {0} este obligatoriu.")]
    [MaxLength(50)]
    [DisplayName("Tara")]
    public string Country  { get; set; }

    [DisplayName("Repartizari")]
    public List<Assignment>? Assignments { get; set; }
  }
}
