using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Classes;

[Table("Reviews")]
public class Review
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("rating")]
    public decimal Rating { get; set; } 

    [Column("review_text")]
    public string ReviewText { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("reviewever_id")]
    public int RevieweverId { get; set; }
    
    public Reviewever? Reviewever { get; set; }
    public List<GameReviews> GameReviews { get; set; } = new();
}


[Table("Game_Reviews")]
public class GameReviews
{
    [Key]
    [Column("game_id")]
    public int GameId { get; set; }

    [Key]
    [Column("review_id")]
    public int ReviewId { get; set; }
    
    public Game? Game { get; set; }
    public Review? Review { get; set; }
}