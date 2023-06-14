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

        Wallet GetWalletById(Guid id);

        bool DeleteWallet(Guid id);

        Task<bool> WalletsExist(string hash);

        int TotalWalletsOwned(string owner);
    }
}