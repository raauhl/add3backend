using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace add3backend.Models
{
    [Table("mints")]
    public class Mint
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("from_address")]
        public string? FromAddress { get; set; }

        [Column("to_address")]
        public string? ToAddress { get; set; }

        [Column("mint_amount")]
        public string? MintAmount { get; set; }

        [Column("token_symbol")]
        public string? TokenSymbol { get; set; }

        [Column("success_status")]
        public bool SuccessStatus { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Column("end_time")]
        public DateTime EndTime { get; set; }
    }
}

