using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace add3backend.Models
{
    public class Login
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("wallet_address")]
        public string? WalletAddress { get; set; }

        [Column("login_time")]
        public DateTime LoginTime { get; set; }
    }
}