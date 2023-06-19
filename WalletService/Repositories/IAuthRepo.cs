#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletService.Models;
namespace WalletService.Repositories
{
    public interface IAuthRepo
    {
        void CreateAccount(Auth auth);
        bool SaveChanges();

        IEnumerable<Auth> GetAccounts();

        Auth? ValidateAccount(string Id, string code);
    }
}