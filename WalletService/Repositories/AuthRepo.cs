#nullable enable

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WalletService.Data;
using WalletService.Models;
using WalletService.Utils;
using System.Threading.Tasks;
using System.Linq;

namespace WalletService.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext dbContext;

        public AuthRepo(AppDbContext context)
        {
            dbContext = context;
        }
        public void CreateAccount(Auth account)
        {
            dbContext.Add(account);
        }

        public async Task<Auth?> findAccount(string accountId, string accountCode)
        {
            string hashedCode = HashValues.Compute(accountCode);
            var account = await dbContext.Accounts
                .Where(w => w.accountID == accountId && w.accountHash == hashedCode)
                .FirstOrDefaultAsync();
            return account;
        }

        public IEnumerable<Auth> GetAccounts()
        {
            return dbContext.Accounts.ToList();
        }

        public bool SaveChanges()
        {
            return (dbContext.SaveChanges() >= 0);
        }
    }
}