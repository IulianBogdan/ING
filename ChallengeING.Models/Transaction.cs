using ChallengeING.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChallengeING.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Category CategoryId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
    }
}
