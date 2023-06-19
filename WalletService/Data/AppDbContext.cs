using Microsoft.EntityFrameworkCore;
using WalletService.Models;
namespace WalletService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Auth> Accounts { get; set; }
    }
}