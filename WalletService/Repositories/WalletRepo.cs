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
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext dbContext;

        public WalletRepo(AppDbContext context)
        {
            dbContext = context;
        }
        public void CreateWallet(Wallet wallet)
        {
            if (wallet == null)
            {
                throw new ArgumentNullException(nameof(wallet), "Invalid payload sent");
            }

            dbContext.Add(wallet);
        }

        public Wallet GetWalletById(Guid id)
        {
            var wallet = dbContext.Wallets.Find(id);
            if (wallet == null)
            {
                throw new ArgumentNullException(nameof(wallet), "Wallet not found");
            }
            return wallet;
        }

        public IEnumerable<Wallet> GetWallets()
        {
            return dbContext.Wallets.ToList();
        }

        public bool SaveChanges()
        {
            return (dbContext.SaveChanges() >= 0);
        }

        public bool DeleteWallet(Guid id)
        {
            var wallet = dbContext.Wallets.Find(id);
            if (wallet == null)
            {
                throw new ArgumentNullException("Wallet not found");
            }
            dbContext.Remove(wallet);
            return (dbContext.SaveChanges() >= 0);
        }
        public int TotalWalletsOwned(string owner)
        {
            var wallets = dbContext.Wallets
                .Where(w => w.Owner == owner)
                .ToList();

            return wallets.Count();
        }

        public async Task<bool> WalletsExist(string hash)
        {
            var exist = await dbContext.Wallets
                .Where(w => w.AccountHash == hash)
                .ToListAsync();
            return exist.Any();
        }
    }
}