using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Platforms")]
public class Platform
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    public List<GamePlatform> GamePlatforms { get; set; } = new();
}

[Table("Game_Platforms")]
public class GamePlatform
{
    [Key]
    [Column("game_id")]
    public int GameId { get; set; }

    [Key]
    [Column("platform_id")]
    public int PlatformId { get; set; }
    
    public Game? Game { get; set; }
    public Platform? Platform { get; set; }
}