using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Movies
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("release_year")]
        public int? ReleaseYear { get; set; }

        [Column("genre")]
        public string? Genre { get; set; }

        [Column("duration_mins")]
        public int? DurationMins { get; set; }

        [Column("avg_rating")]
        public double? AvgRating { get; set; }

        // rating_id is a uuid referencing Ratings.id
        [Column("rating_id")]
        public string? RatingId { get; set; }
        public Rating? Rating { get; set; }
    }
}
