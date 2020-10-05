using ChallengeING.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeING.Models
{
    public class Account
    {
        [Key]
        public Guid ResourceId { get; set; }
        public Product Product { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
