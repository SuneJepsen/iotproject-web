using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Crypto
{
    public class RijndaelManaged : ICryptography
    {
        public string Encrypt(string plainText, string passPhrase)
        {
            return plainText;
        }

        public string Decrypt(string cipherText, string passPhrase)
        {
            return cipherText;
        }
    }
}
