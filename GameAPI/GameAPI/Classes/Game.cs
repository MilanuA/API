using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Games")]
public class Game
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("release_date")]
    public DateTime ReleaseDate { get; set; }

    [Column("price")]
    public int Price { get; set; }

    [Column("engine_id")]
    public int EngineId { get; set; }

    [Column("publisher_id")]
    public int PublisherId { get; set; }
    
    public Engine? Engine { get; set; }
    public Publisher? Publisher { get; set; }
    public List<GameGenre> GameGenres { get; set; } = new();
    public List<GamePlatform> GamePlatforms { get; set; } = new();
    public List<GameDeveloper> GameDevelopers { get; set; } = new();
    public List<GameReviews> GameReviews { get; set; } = new();
}