#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using WalletService.Data;
using WalletService.Models;
using WalletService.Utils;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public Auth? ValidateAccount(string accountId, string accountCode)
        {
            var account = dbContext.Accounts.Where(w => w.AccountID == accountId && w.AccountCode == accountCode).FirstOrDefault();
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