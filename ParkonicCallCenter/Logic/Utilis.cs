using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ParkonicCallCenter.Logic
{
    public static class Utilis
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string AWSToken { get; set; }
        public static string UserFullName { get; set; }
   //     public static string LocationID { get; set; }
        public static List<CallTypeMaster> CallTypes { get; set; }
        //  public static string UserType { get; set; }


        //SHA256
        //private static byte[] GetHash(string inputString)
        //{
        //    using (HashAlgorithm algorithm = SHA256.Create())
        //        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        //}

        //public static string GetHashString(string inputString)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (byte b in GetHash(inputString))
        //        sb.Append(b.ToString("X2"));

        //    return sb.ToString();
        //}

        //public static DateTime ConvertTransactionidToDateTime(string TransID, string AccessPointID)
        //{
        //    return (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
        //        .AddMilliseconds(Convert.ToDouble(TransID.Substring(AccessPointID.Length)))
        //        .ToLocalTime();
        //}
        public static string GetArabicPlateFromEng(string EnPlate)
        {
            StringBuilder keywordAr = new StringBuilder();
            foreach (char en in EnPlate.ToCharArray())
                keywordAr.Append(GetArabicAlphabateFromEng(en.ToString()));
            return keywordAr.ToString();
        }

        private static string GetArabicAlphabateFromEng(string en)
        {
            switch (en)
            {
                case "A": return "ا";
                case "B": return "ب";
                case "T": return "ت";
                // case "S": return "ث";
                case "J": return "ج";
                // case "H": return "ح";
                //case "K": return "خ";
                case "D": return "د";
                //case "Z": return "ذ";
                case "R": return "ر";
                case "Z": return "ز";
                case "S": return "س";
                //case "S": return "ش";
                //case "S": return "ص";
                // case "D": return "ض";
                // case "T": return "ط";
                //case "Z": return "ظ";
                //case "A": return "ع";
                case "G": return "غ";
                case "F": return "ف";
                case "Q": return "ق";
                case "K": return "ك";
                case "L": return "ل";
                case "M": return "م";
                case "N": return "ن";
                case "H": return "ه";
                case "W": return "و";
                case "Y": return "ي";

                case "0": return "٠";
                case "9": return "٩";
                case "8": return "٨";
                case "7": return "٧";
                case "6": return "٦";
                case "5": return "٥";
                case "4": return "٤";
                case "3": return "٣";
                case "2": return "٢";
                case "1": return "١";

                default: return "";
            }
        }


    }
}
