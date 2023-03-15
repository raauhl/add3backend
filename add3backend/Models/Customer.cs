using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace add3backend.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("wallet_address")]
        public string? WalletAddress { get; set; }

        [Column("token_symbol")]
        public string? TokenSymbol { get; set; }

        [Column("token_name")]
        public string? TokenName { get; set; }

        [Column("user_balance")]
        public string? UserBalance { get; set; }
    }
}

