using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Lists
{

    [Table("watched")]
    public class Watched
    {
        // Primary key is a uuid
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Column("fav_id")]
        public string? FavId { get; set; }

        [Column("diary_id")]
        public string? DiaryId { get; set; }

        // schema lists user_id as string
        [Column("user_id")]
        public string? UserId { get; set; }

        [Column("movie_id")]
        public string? MovieId { get; set; }
        public CodeJam3b.Models.Movies.Movie? Movie { get; set; }
    }
}
