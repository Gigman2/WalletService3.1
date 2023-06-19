using System;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos
{

    public class AuthLoginDto
    {
        [Required]
        [RegularExpression("^233[0-9]{9}$", ErrorMessage = "Invalid account id. ")]
        public string accountID { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Code is invalid. ")]
        public string code { get; set; }
    }


    public class AuthDto
    {
        [Required]
        public string accountID { get; set; }

        [Required]
        public bool isAdmin { get; set; }
    }

    public class AuthCreateDto : AuthDto
    {

        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Code is invalid. ")]
        public string code { get; set; }
    }

    public class AuthValidDto : AuthDto
    {
        public string token { get; set; }

        public DateTime? CreatedAt { get; set; }
    }


    public class AuthMainDto : AuthDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class AuthInsertDto : AuthMainDto
    {

        [Required]
        public string accountHash { get; set; }

    }
}