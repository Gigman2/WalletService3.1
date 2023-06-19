using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WalletService.Models
{
    public class Auth
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^233[0-9]{9}$", ErrorMessage = "Invalid account id. ")]
        public string accountID { get; set; }

        [Required]
        public bool isAdmin { get; set; }

        [Required]
        public string accountHash { get; set; }


        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }
    }
}