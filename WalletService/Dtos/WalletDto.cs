using System;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos
{
    public class WalletQueryDto
    {
        public int pagesize { get; set; }

        public int page { get; set; }
    }
    public class WalletCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^(momo|card)$", ErrorMessage = "Type must be either 'momo' or 'card'.")]
        public string Type { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        [RegularExpression("^(visa|mastercard|mtn|vodafone|airteltigo)$", ErrorMessage = "Invalid Account Scheme provided. ")]
        public string AccountScheme { get; set; }
    }

    public class WalletMainDto : WalletCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Owner { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class WalletInsertDto : WalletCreateDto
    {
        public Guid Id { get; set; }

        public string AccountHash { get; set; }

        [Required]
        public string Owner { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}