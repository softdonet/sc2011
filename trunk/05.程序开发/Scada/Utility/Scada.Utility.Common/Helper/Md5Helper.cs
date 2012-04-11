using System;
using System.Text;
using System.Security.Cryptography;



namespace Scada.Utility.Common.Helper
{

    public class Md5Helper
    {

        public static string Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] hash = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return BitConverter.ToString(hash).Replace("-", "");
        }


    }
}
