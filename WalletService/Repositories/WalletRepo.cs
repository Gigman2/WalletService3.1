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

        public async Task<bool> DeleteWallet(Guid id)
        {
            var wallet = await dbContext.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return false;
            }
            dbContext.Remove(wallet);
            return (dbContext.SaveChanges() >= 0);
        }
        public async Task<int> TotalWalletsOwned(string owner)
        {
            var wallets = await dbContext.Wallets
                .Where(w => w.Owner == owner)
                .ToListAsync();

            return wallets.Count();
        }

        public async Task<bool> WalletsExist(string hash)
        {
            var exist = await dbContext.Wallets
                .Where(w => w.AccountHash == hash)
                .ToListAsync();
            return exist.Any();
        }


        public async Task<Wallet?> GetWalletById(Guid id)
        {
            var wallet = await dbContext.Wallets.FindAsync(id);
            return wallet;
        }

        public async Task<IEnumerable<Wallet>> GetWallets(int page, int pagesize)
        {
            var query = dbContext.Wallets
                .OrderBy(e => e.CreatedAt)
                .Skip((page - 1) * pagesize)
                .Take(pagesize);
            return await query.ToListAsync();
        }


        public async Task<Wallet?> GetOwnerWalletById(Guid id, string ownerId)
        {
            var wallet = await dbContext.Wallets
                .Where(w => w.Id == id && w.Owner == ownerId)
                .FirstOrDefaultAsync();
            return wallet;
        }

        public async Task<IEnumerable<Wallet>> GetOwnersWallets(string id)
        {
            return await dbContext.Wallets.Where(w => w.Owner == id).ToListAsync();
        }
    }
}