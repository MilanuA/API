using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Developers")]
public class Developer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("country")]
    public string Country { get; set; }
    
    public List<GameDeveloper> GameDevelopers { get; set; } = new();
}


[Table("Game_Developers")]
public class GameDeveloper
{
    [Key]
    [Column("game_id")]
    public int GameId { get; set; }

    [Key]
    [Column("developer_id")]
    public int DeveloperId { get; set; }
    
    public Game? Game { get; set; }
    public Developer? Developer { get; set; }
}