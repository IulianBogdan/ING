using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeING.Models.Responses
{
    public class AccountDTO
    {
        public string resourceId { get; set; }
        public string product { get; set; }
        public string iban { get; set; }
        public string name { get; set; }
        public string currency { get; set; }

    }
}
