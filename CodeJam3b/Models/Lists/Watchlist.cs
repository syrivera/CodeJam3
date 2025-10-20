using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Lists
{

    [Table("watchlist")]
    public class Watchlist
    {
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Column("watchlist_id")]
        public required string WatchlistId { get; set; }

        [Column("movie_id")]
        public string? MovieId { get; set; }
        public CodeJam3b.Models.Movies.Movie? Movie { get; set; }

        //I like this implementation for a middle table, well done!
    }
}
