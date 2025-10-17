using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Lists
{
    [Table("diary")]
    public class Diary
    {
    [Key]
    [Column("id")]
    public required string Id { get; set; }

    [Column("diary_id")]
    public required string DiaryId { get; set; }

    [Column("movie_id")]
    public string? MovieId { get; set; }
    public CodeJam3b.Models.Movies.Movie? Movie { get; set; }

    [Column("rating_id")]
    public string? RatingId { get; set; }
    public CodeJam3b.Models.Movies.Rating? Rating { get; set; }
    }
}
