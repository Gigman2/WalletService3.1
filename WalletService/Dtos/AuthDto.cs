using System;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos
{
    public class AuthDto
    {
        [Required]
        public string accountID { get; set; }

        [Required]
        public bool isAdmin { get; set; }
    }

    public class AuthMainDto : AuthDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class AuthInsertDto : AuthDto
    {
        public Guid Id { get; set; }


        [Required]
        public string accountCode { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}