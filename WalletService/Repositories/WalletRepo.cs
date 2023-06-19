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
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext dbContext;

        public WalletRepo(AppDbContext context)
        {
            dbContext = context;
        }
        public void CreateWallet(Wallet wallet)
        {
            dbContext.Add(wallet);
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
                return false;
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

        public bool WalletsExist(string hash)
        {
            var exist = dbContext.Wallets
                .Where(w => w.AccountHash == hash)
                .ToList();
            return exist.Any();
        }


        public Wallet? GetWalletById(Guid id)
        {
            var wallet = dbContext.Wallets.Find(id);
            return wallet;
        }

        public IEnumerable<Wallet> GetWallets(int page, int pagesize)
        {
            return dbContext.Wallets
                .OrderBy(e => e.CreatedAt)
                .Skip((page - 1) * pagesize)
                .Take(pagesize)
                .ToList();
        }


        public Wallet? GetOwnerWalletById(Guid id, string ownerId)
        {
            var wallet = dbContext.Wallets
                .Where(w => w.Id == id && w.Owner == ownerId)
                .FirstOrDefault();
            return wallet;
        }

        public IEnumerable<Wallet> GetOwnersWallets(string id)
        {
            return dbContext.Wallets.Where(w => w.Owner == id);
        }
    }
}