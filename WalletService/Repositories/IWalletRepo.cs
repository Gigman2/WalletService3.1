#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletService.Models;
namespace WalletService.Repositories
{
    public interface IWalletRepo
    {
        void CreateWallet(Wallet wallet);
        bool SaveChanges();

        Task<IEnumerable<Wallet>> GetWallets(int page, int pagesize);
        Task<IEnumerable<Wallet>> GetOwnersWallets(string id);

        Task<Wallet?> GetOwnerWalletById(Guid id, string owner);

        Task<Wallet?> GetWalletById(Guid id);

        Task<bool> DeleteWallet(Guid id);

        Task<bool> WalletsExist(string hash);

        Task<int> TotalWalletsOwned(string owner);
    }
}