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

        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Code is invalid. ")]
        public string code { get; set; }
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