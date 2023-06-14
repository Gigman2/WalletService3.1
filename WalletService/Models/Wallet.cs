using System;
using System.ComponentModel.DataAnnotations;
namespace WalletService.Models
{
    public class Wallet
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^(momo|card)$", ErrorMessage = "Type must be either 'momo' or 'card'.")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Scheme")]
        [RegularExpression("^(visa|mastercard|mtn|vodafone|airteltigo)$", ErrorMessage = "Invalid Account Scheme provided. ")]
        public string AccountScheme { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Owner phone is invalid. ")]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }
    }
}