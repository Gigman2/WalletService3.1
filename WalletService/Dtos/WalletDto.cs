using System;
namespace WalletService.Dtos
{
    public class WalletCreateDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string AccountNumber { get; set; }

        public string AccountScheme { get; set; }

        public string Owner { get; set; }
    }

    public class WalletMainDto : WalletCreateDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class WalletInsertDto : WalletCreateDto
    {
        public Guid Id { get; set; }

        public string AccountHash { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}