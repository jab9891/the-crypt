using System.Security.Cryptography;
using System.IO;

namespace Cryptography
{
    class Cryptography
    {
        private enum CryptProc {ENCRYPT, DECRYPT};

        private static byte[] CryptBytes(byte[] plain, string password, int iterations, CryptProc cryptproc,byte[] SALT)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, SALT, "SHA512", iterations);
            TripleDES des = TripleDES.Create();
            des.Key = pdb.GetBytes(24);
            des.IV = pdb.GetBytes(8);
            MemoryStream memstream = new MemoryStream();
            ICryptoTransform cryptor = (cryptproc == CryptProc.ENCRYPT) ? des.CreateEncryptor() : des.CreateDecryptor();
            CryptoStream cryptostream = new CryptoStream(memstream, cryptor, CryptoStreamMode.Write);
            cryptostream.Write(plain, 0, plain.Length); 
            cryptostream.Close();
            return memstream.ToArray();
        }

        public static byte[] EncryptBytes(byte[] plain, string password, int iterations,byte[] SALT)
        {
            return CryptBytes(plain, password, iterations, CryptProc.ENCRYPT, SALT);
        }

        public static byte[] DecryptBytes(byte[] plain, string password, int iterations,byte[] SALT)
        {
            return CryptBytes(plain, password, iterations, CryptProc.DECRYPT, SALT);
        }
    }
}
