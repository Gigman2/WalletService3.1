using System.Security.Cryptography;
using System.Text;

namespace WalletService.Utils
{
    public class HashValues
    {
        private string value;
        private byte[] tmpValue;
        private byte[] tmpHash;
        public HashValues(string initialValue)
        {
            value = initialValue;
        }

        public string Compute()
        {
            tmpValue = ASCIIEncoding.ASCII.GetBytes(value);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpValue);
            return ByteArrayToString(tmpHash);
        }

        private string ByteArrayToString(byte[] arrInput)
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