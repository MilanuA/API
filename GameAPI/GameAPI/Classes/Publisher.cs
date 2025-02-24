using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Publishers")]
public class Publisher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("country")]
    public string Country { get; set; }
    
    public List<Game> Games { get; set; } = new();
}