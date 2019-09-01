using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MapIt.Helpers
{
    public class AuthHelper
    {
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                var objDESCrypto = new TripleDESCryptoServiceProvider();
                var objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        public static string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                var objDESCrypto = new TripleDESCryptoServiceProvider();
                var objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        public static string GetMD5Hash(string value)
        {
            try
            {
                MD5 md5Hasher = MD5.Create();
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        public static string RandomString(int length)
        {
            try
            {
                const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
                string result = "";
                Random randomObj = new Random();

                result = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[randomObj.Next(s.Length)]).ToArray());

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        public static string RandomCode(int lenght)
        {
            try
            {
                int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                string result = "";
                Random randomObj = new Random();

                for (int i = 0; i < lenght; i++)
                {
                    result += numbers[randomObj.Next(0, 9)];
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }
    }
}
