using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Genres")]
public class Genre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
    
    public List<GameGenre> GameGenres { get; set; } = new();
}

[Table("Game_Genres")]
public class GameGenre
{
    [Key]
    [Column("game_id")]
    public int GameId { get; set; }

    [Key]
    [Column("genre_id")]
    public int GenreId { get; set; }
    
    public Game? Game { get; set; }
    public Genre? Genre { get; set; }
}