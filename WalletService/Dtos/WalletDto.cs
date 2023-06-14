using System;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos
{
    public class WalletCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string AccountScheme { get; set; }

        [Required]
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