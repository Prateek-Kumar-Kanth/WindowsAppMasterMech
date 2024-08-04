using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MasterMechPrj
{
    public class MasterMechUtil
    {

        public static string msConnStr = "";

        public static string msUserName = "";
        private static string sFinYear;
        public static DateTime mdLastLogin;
        public static string msUserType;


        //For Organisation where this software is being installed
        public static string msCompName="";
        public static string msStreetAdd = "";
        public static string msArea = "";
        public static string msCity = "";
        public static string msState = "";
        public static string msPincode = "";
        public static string  msCountry = "";
        public static string msGSTNo = "";
        public static string msContact = "";
        public static string msPAN = "";
        public static string msTAN = "";
        public static string msDOEstab = "";
        public static string msServerName = "";
        public static string msDatabase = "";
        public static string msUserID = "";
        public static string msPassword = "";
        public static string msConfirmPass = "";

        public static string sFY
        {
            set
            {
                if (Regex.IsMatch(value, @"^(\d{4})-\d{2}$", RegexOptions.IgnoreCase))
                    sFinYear = value;
                else
                    throw new ArgumentException(String.Format("{0} is not a valid value for", value), sFY);
            }
            get
            {
                return sFinYear;
            }
        }
        static public string FYrText(string isFYr)
        {
            int lnNextCalYr = 0;
            lnNextCalYr = int.Parse(isFYr.Substring(2, 2));
            lnNextCalYr++;
            return (isFYr + "-" + lnNextCalYr.ToString());
        }
        public static string Encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static SelectList FYList()
        {
            List<SelectListItem> FYList = new List<SelectListItem>();
            int lnCount = 0;

            //In ComboBox we are showing financial year
            int lnCurrYear = DateTime.Now.Year;
            int lnYear = lnCurrYear - 5;

            for (lnCount = 0; lnCount < 10; lnCount++)
            {
                FYList.Add(new SelectListItem
                {
                    Text = lnYear.ToString() + "-" + (lnYear + 1).ToString().Substring(2),
                    Value = lnYear.ToString() + "-" + (lnYear++ + 1).ToString().Substring(2)
                    //    Value = (lnYear - 1).ToString()
                });
            }

            return new SelectList(FYList, "Value", "Text"); ;
        }
        public static string CurrFY()
        {
            if (DateTime.Now.Month >= 4)
                return (DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString().Substring(2));
            else
                return ((DateTime.Now.Year - 1).ToString() + "-" + DateTime.Now.Year.ToString().Substring(2));

        }
        public enum OPMode
        {
            New,
            Open,
            Delete
        }
        public enum FormType
        {
            User,
            Customer,
            Item
        }
    }
}
