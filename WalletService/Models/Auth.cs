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