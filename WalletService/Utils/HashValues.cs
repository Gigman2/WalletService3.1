using System.Security.Cryptography;
using System.Text;

namespace WalletService.Utils
{
    public static class HashValues
    {
        public static string Compute(string value)
        {
            byte[] tmpValue;
            tmpValue = ASCIIEncoding.ASCII.GetBytes(value);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpValue);
            return HashValues.ByteArrayToString(tmpHash);
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

    }
}