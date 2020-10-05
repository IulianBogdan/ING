using ChallengeING.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeING.Models
{
    public class AccountTransactionReport
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
    }
}
