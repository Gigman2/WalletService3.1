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

        IEnumerable<Wallet> GetWallets();
        IEnumerable<Wallet> GetWalletsByAccount(string id);

        Wallet? GetWalletById(Guid id);

        bool DeleteWallet(Guid id);

        Task<bool> WalletsExist(string hash);

        int TotalWalletsOwned(string owner);
    }
}