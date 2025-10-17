using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Lists
{

    [Table("fav")]
    public class Fav
    {
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Column("fav_id")]
        public required string FavId { get; set; }

        [Column("movie_id")]
        public string? MovieId { get; set; }
        public CodeJam3b.Models.Movies.Movie? Movie { get; set; }
    }
}
