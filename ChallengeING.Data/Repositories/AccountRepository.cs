using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeING.Models;
using ChallengeING.Models.Interfaces;
using ChallengeING.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace ChallengeING.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly SqlDBContext _context;

        public AccountRepository(SqlDBContext context)
            : base(context)
        {
            this._context = context;
        }

        public async Task<AccountDTO> GetAccount(Guid accountId)
        {
            var account = await base.GetAsync(accountId);

            return new AccountDTO
            {
                resourceId = account.ResourceId.ToString(),
                product = account.Product.Name.ToString(),
                iban = account.IBAN.ToString(),
                name = account.Name.ToString(),
                currency = account.Currency.ToString()
            };
        }

        public async Task<GetAccountResponse> GetAllAcounts()
        {
            var dbAccounts = base.GetAll();

            return new GetAccountResponse
            {
                accounts = await dbAccounts
                .Select(x => new AccountDTO
                {
                    resourceId = x.ResourceId.ToString(),
                    product = x.Product.Name.ToString(),
                    iban = x.IBAN.ToString(),
                    name = x.Name.ToString(),
                    currency = x.Currency.ToString()
                })
                .ToListAsync()
            };
        }

        public async Task<List<AccountTransactionReport>> GetMonthlyReportForAccount(Guid accountId)
        {
            var result = _context.Accounts.Where(X => x.ResourceId == accountId)
                                            .Include(Transactions)
                                                .SelectMany(x => x.Transactions)
                                                    .Where(x => x.TransactionDate.Month == DateTime.Now.Month - 1)
                                                        .GroupBy(g => g.CategoryId)
                                                            .Select(r => new AccountTransactionReport
                                                            {
                                                                CategoryName = r.Key.ToString(),
                                                                TotalAmount = r.Sum(s => s.Amount),
                                                                Currency = account.Currency
                                                            })
                                                                    .ToList();
            return result;
        }
    }
}
