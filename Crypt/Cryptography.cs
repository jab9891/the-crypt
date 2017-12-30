using System.Security.Cryptography;
using System.IO;

namespace Cryptography
{
    class Cryptography
    {
        private enum CryptProc {ENCRYPT, DECRYPT};

        /// <summary>
        /// Performs either an encryption or decrytion
        /// </summary>
        /// <param name="plain">Unencrypted byte array
        /// <param name="password">Password to be used
        /// <param name="iterations">Number of iterations hash algorithm uses
        /// <param name="cryptproc">Process to be performed
        /// <returns>Results of process in the form of a byte array</returns>
        /// 

        private static byte[] CryptBytes(byte[] plain, string password, int iterations, CryptProc cryptproc,byte[] SALT)
        {
            //Create our key from the password provided
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, SALT, "SHA512", iterations);
            //We'll be using 3DES
            TripleDES des = TripleDES.Create();
            des.Key = pdb.GetBytes(24);
            des.IV = pdb.GetBytes(8);
            MemoryStream memstream = new MemoryStream();
            ICryptoTransform cryptor = (cryptproc == CryptProc.ENCRYPT) ? des.CreateEncryptor() : des.CreateDecryptor();
            CryptoStream cryptostream = new CryptoStream(memstream, cryptor, CryptoStreamMode.Write);
            cryptostream.Write(plain, 0, plain.Length); //write finished product to our MemoryStream
            cryptostream.Close();
            return memstream.ToArray();
        }

        /// <summary>
        /// Encrypts byte arrays
        /// </summary>
        /// <param name="plain">Unencrypted byte array
        /// <param name="password">Password to be used
        /// <param name="iterations">Number of iterations hash
        /// algorithm uses
        /// <returns>Encypted byte array</returns>
        public static byte[] EncryptBytes(byte[] plain, string password, int iterations,byte[] SALT)
        {
            return CryptBytes(plain, password, iterations, CryptProc.ENCRYPT, SALT);
        }

        /// <summary>
        /// Decrypts byte arrays
        /// </summary>
        /// <param name="plain">Unencrypted byte array
        /// <param name="password">Password to be used
        /// <param name="iterations">Number of iterations hash algorithm uses
        /// <returns>Decrypted byte array</returns>
        /// 
        public static byte[] DecryptBytes(byte[] plain, string password, int iterations,byte[] SALT)
        {
            return CryptBytes(plain, password, iterations, CryptProc.DECRYPT, SALT);
        }
    }
}
