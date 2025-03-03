using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadCompany2.MasterServer;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Country { get; set; }
    public DateTime Birthday { get; set; }
    public string Password { get; set; }
    public ICollection<Persona> Personas { get; set; }
}