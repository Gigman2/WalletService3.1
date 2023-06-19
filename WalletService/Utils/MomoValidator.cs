using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.Utils
{
    public class MomoValidator
    {
        private const string MomoPattern = @"^233[0-9]{9}$";

        // Validate a mobile money number using regex pattern
        public static bool Validate(string scheme, string number)
        {
            string[] providers = { "mtn", "tigo", "airtel", "vodafone" };

            if (!providers.Contains(scheme))
                return false;
            // Remove any non-digit characters from the mobile money number
            string momoNumber = Regex.Replace(number, @"[^\d]", "");

            // Validate the mobile money number using regex pattern
            if (!Regex.IsMatch(momoNumber, MomoPattern))
                return false;


            return true;
        }

    }
}