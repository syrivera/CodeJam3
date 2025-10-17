using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeJam3b.Models.Users
{
    [Table("user")]
    public class User
    {
        // Primary key: user_id (uuid)
        [Key]
        [Column("user_id")]
        public required string UserId { get; set; }

        // external/string identifier
        [Column("id")]
        public string? Id { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("watched_id")]
        public string? WatchedId { get; set; }

        [Column("list_id")]
        public string? ListId { get; set; }

        [Column("watchlist_id")]
        public string? WatchlistId { get; set; }

        [Column("diary_id")]
        public string? DiaryId { get; set; }

        // Navigation properties (optional)
        public CodeJam3b.Models.Lists.Watchlist? Watchlist { get; set; }
        public CodeJam3b.Models.Lists.Watched? Watched { get; set; }
        public CodeJam3b.Models.Lists.Diary? Diary { get; set; }
    }
}
