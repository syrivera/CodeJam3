using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Movies
{
    [Table("ratings")]
    public class Rating
    {
        // Primary key: id (uuid)
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Column("rating_id")]
        public required string RatingId { get; set; } // why both id and ratingid

        [Column("user_id")]
        public string? UserId { get; set; }
        public CodeJam3b.Models.Users.User? User { get; set; }

        [Column("review")]
        public string? Review { get; set; }

        [Column("movie_name")]
        public string? MovieName { get; set; }

        [Column("stars")]
        public int? Stars { get; set; }
    }
}
