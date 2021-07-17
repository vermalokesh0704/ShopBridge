using System;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Net.NetworkInformation;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace shopbridge
{
    public class Utilities
    {
        private static string _numbers = "123456789";
        public Utilities()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Generates MD5 Hash for given string
        /// </summary>
        public string GenerateMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        /// <summary>
        /// Compares and verifies MD5 hash of two strings
        /// </summary>
        public bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GenerateMd5Hash(input);
            StringComparer comparer = StringComparer.Ordinal;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected Random rGen = new Random();
        protected string[] strCharacters = { "A","B","C","D","E","F","G",
    "H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y",
    "Z","1","2","3","4","5","6","7","8","9","0","a","b","c","d","e","f","g","h",
    "i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

        /// <summary>
        /// This method is used to generate random lowercase password with given length
        /// </summary>
        public string GenPassLowercase(int i)
        {
            int p = 0;
            string strPass = "";
            for (int x = 0; x <= i; x++)
            {
                p = rGen.Next(0, 35);
                strPass += strCharacters[p];
            }
            return strPass.ToLower();
        }

        /// <summary>
        /// This method is used to generate random uppercase password with given length
        /// </summary>
        public string GenPassWithCap(int i)
        {
            int p = 0;
            string strPass = "";
            for (int x = 0; x <= i; x++)
            {
                p = rGen.Next(0, 60);
                strPass += strCharacters[p];
            }
            return strPass.ToUpper();
        }
        /// <summary>
        /// This method is used to generate random string with given length
        /// </summary>
        public string GenRendomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Int64 GenRendomNumber(int length)
        {
            Random rn = new Random();
            StringBuilder builder = new StringBuilder(6);
            string numberAsString = "";
            int numberAsNumber = 0;

            for (var i = 0; i < length; i++)
            {
                builder.Append(_numbers[rn.Next(0, _numbers.Length)]);
            }

            numberAsString = builder.ToString();
            numberAsNumber = int.Parse(numberAsString);
            return numberAsNumber;
        }
        public string Encrypt_AES(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt_AES(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// AES 265 Bit Encryption
        /// </summary>
        /// <param name="plain_text"></param>
        /// <param name="encryption_key"></param>
        /// <param name="invect"></param>
        /// <returns>cipher text</returns>
        public string Aes_Encrypt_256(string plain_text, string encryption_key, string invect)
        {
            var sToEncrypt = plain_text;
            var myRijndael = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 128
            };

            var key = Encoding.ASCII.GetBytes(encryption_key);
            var IV = Encoding.ASCII.GetBytes(invect);
            var encryptor = myRijndael.CreateEncryptor(key, IV);
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            var encrypted = msEncrypt.ToArray();
            return (Convert.ToBase64String(encrypted));
        }
        /// <summary>
        /// Decrypt AES Cipher Text
        /// </summary>
        /// <param name="cipher_text"></param>
        /// <param name="encryption_key"></param>
        /// <param name="invect"></param>
        /// <returns>Plain Text</returns>
        public static string Aes_Decrypt_256(string cipher_text, string encryption_key, string invect)
        {
            var sEncryptedString = cipher_text;
            var myRijndael = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 128
            };

            var key = Encoding.ASCII.GetBytes(encryption_key);
            var IV = Encoding.ASCII.GetBytes(invect);

            var decryptor = myRijndael.CreateDecryptor(key, IV);
            var sEncrypted = Convert.FromBase64String(sEncryptedString);
            var fromEncrypt = new byte[sEncrypted.Length];
            var msDecrypt = new MemoryStream(sEncrypted);
            var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

            var strmReader = new StreamReader(csDecrypt);
            var str = strmReader.ReadToEnd();
            return str; // (Encoding.ASCII.GetString(str));
        }
        public string Encrypt_AES_WK(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                //encryptor.BlockSize = 64;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt_AES_WK(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "शून्य";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000000) + " अरब ";
                number %= 1000000000;
            }
            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " करोड़ ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " लाख ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " हजार ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " सौ ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0 && number < 100)
            {
                if (words != "") words += "";
                var unitsMap = new[]
                {
               "", "एक", "दो", "तीन", "चार", "पाँच", "छह", "सात", "आठ", "नौ", "दस",
            "ग्यारह", "बारह", "तेरह", "चौदह", "पन्द्रह", "सोलह", "सत्रह", "अठारह", "उन्नीस", "बीस",
            "इक्कीस", "बाईस", "तेईस", "चौबीस", "पच्चीस", "छब्बीस", "सत्ताईस", "अट्ठाईस", "उनतीस", "तीस",
            "इकतीस", "बत्तीस", "तैंतीस", "चौंतीस", "पैंतीस", "छत्तीस", "सैंतीस", "अड़तीस", "उनतालीस", "चालीस",
            "इकतालीस", "बयालीस", "तैंतालीस", "चौवालीस", "पैंतालीस", "छियालीस", "सैंतालीस", "अड़तालीस", "उनचास", "पचास",
            "इक्यावन", "बावन", "तिरेपन", "चौवन", "पचपन", "छप्पन", "सत्तावन", "अट्ठावन", "उनसठ", "साठ",
            "इकसठ", "बासठ", "तिरेसठ", "चौंसठ", "पैंसठ", "छियासठ", "सड़सठ", "अड़सठ", "उनहत्तर", "सत्तर",
            "इकहत्तर", "बहत्तर", "तिहत्तर", "चौहत्तर", "पचहत्तर", "छिहत्तर", "सतहत्तर", "अठहत्तर", "उनासी", "अस्सी",
            "इक्यासी", "बयासी", "तिरासी", "चौरासी", "पचासी", "छियासी", "सत्तासी", "अट्ठासी", "नवासी", "नब्बे",
            "इक्यानबे", "बानबे", "तिरानबे", "चौरानबे", "पंचानबे", "छियानबे", "सत्तानबे", "अट्ठानबे", "निन्यानबे"
            };
                //var tensMap = new[]
                //{
                //    "शून्य", "दस", "बीस", "तीस", "चालीस", "पचास", "साठ", "सत्तर", "अस्सी", "नब्बे"
                //};
                if (number < 100) words += unitsMap[number];
                else
                {

                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        /// <summary>
        /// Generates SHA256 Hash
        /// </summary>
        /// <param name="strData"></param>
        /// <returns>string</returns>
        public string GenerateSHA256FromString(string strData)
        {
            var message = Encoding.ASCII.GetBytes(strData);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        /// <summary>
        /// Get Client IP
        /// </summary>
        /// <param name="context"></param>
        /// <param name="allowForwarded"></param>
        /// <returns></returns>
        public static string GetRemoteIPAddress(HttpContext context, bool allowForwarded = true)
        {
            string root_ip = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();
            string ip_behindproxy = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            string framework_ip = null;
            if (ip_behindproxy == "" || ip_behindproxy == null)
                framework_ip = context.Connection.RemoteIpAddress.ToString();
            string client_ip = null;

            client_ip = root_ip + "," + framework_ip + ", " + ip_behindproxy;
            client_ip = client_ip.Trim().Trim(',');
            return client_ip;
        }


        /// <summary>
        /// convert date string DD-MM-YYYY to YYYY-MM-DD
        /// </summary>
        /// <param name="date"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ConvertDateDMYtoYMD(string date, string separator)
        {
            string d = date.Substring(0, 2);
            string m = date.Substring(3, 2);
            string y = date.Substring(6, 4);

            return y + separator + m + separator + d;
        }


        /// <summary>
        /// convert date string YYYY-MM-DD to DD-MM-YYYY
        /// </summary>
        /// <param name="date"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ConvertDateYMDtoDMY(string date, string separator)
        {
            string d = date.Substring(0, 2);
            string m = date.Substring(3, 2);
            string y = date.Substring(6, 4);

            return d + separator + m + separator + y;
        }

        public string GenBarcodeNumber(long number, int prefix_length, int suffix_length)
        {
            return GenRendomNumber(prefix_length).ToString() + number.ToString() + GenRendomNumber(suffix_length).ToString();
        }
    }
}
