using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.Utils
{
    public class MomoValidator
    {
        private const string MomoPattern = @"^(?:233[235][3457]\d{7}|0[235][3457]\d{7})$";

        public static bool Validate(string scheme, string number)
        {
            string[] providers = { "mtn", "tigo", "airtel", "vodafone" };

            if (!providers.Contains(scheme))
                return false;

            string momoNumber = Regex.Replace(number, @"[^\d]", "");

            if (!Regex.IsMatch(momoNumber, MomoPattern))
                return false;


            return true;
        }

    }
}