using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Reviewever")]
public class Reviewever
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
    
    public List<Review> Reviews { get; set; } = new();
}