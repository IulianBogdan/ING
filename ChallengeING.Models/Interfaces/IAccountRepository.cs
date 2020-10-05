using ChallengeING.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeING.Models.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<List<AccountTransactionReport>> GetMonthlyReportForAccount(Guid accountId);
        Task<AccountDTO> GetAccount(Guid accountId);
        Task<GetAccountResponse> GetAllAcounts();
    }
}
