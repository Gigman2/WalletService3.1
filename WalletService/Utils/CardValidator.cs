using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.Utils
{
    public class CardValidator
    {
        private const string CreditCardPattern = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9]{2})[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";

        // Validate a credit card number using regex pattern
        public static bool Validate(string scheme, string number)
        {
            string[] providers = { "visa", "mastercard" };

            if (!providers.Contains(scheme))
                return false;
            // Remove any non-digit characters from the card number
            string cardNumber = Regex.Replace(number, @"[^\d]", "");

            // Validate the card number using regex pattern
            if (!Regex.IsMatch(cardNumber, CreditCardPattern))
                return false;


            return true;
        }

    }
}