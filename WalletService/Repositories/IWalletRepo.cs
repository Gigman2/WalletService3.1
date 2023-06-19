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

        IEnumerable<Wallet> GetWallets(int page, int pagesize);
        IEnumerable<Wallet> GetOwnersWallets(string id);

        Wallet? GetOwnerWalletById(Guid id, string owner);

        Wallet? GetWalletById(Guid id);

        bool DeleteWallet(Guid id);

        bool WalletsExist(string hash);

        int TotalWalletsOwned(string owner);
    }
}