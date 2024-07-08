using DigitsNFCToolkit.JSON;
using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace DigitsNFCToolkit
{
    public static class MRZUtils
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Machine-readable_passport
        /// </summary>
        /// <param name="MRZInfo"></param>
        /// <returns></returns>
        public static JSONObject ExtractMRZInfo(string MRZInfo)
        {
            string[] MRZParts = MRZInfo.Split('\n');
            if (MRZParts.Length != 2)
            {
                Debug.Log("MRZ info should have 2 lines");
                return null;
            }

            string firstLine = MRZParts[0];
            string secondLine = MRZParts[1];

            string pcode = firstLine.Substring(0, 1);
            string type = firstLine.Substring(1, 1);
            string country = firstLine.Substring(2, 3);
            string names = firstLine.Substring(5);

            string pnumber = secondLine.Substring(0, 9);
            string checkDigit1 = secondLine.Substring(9, 1);

            string nationality = secondLine.Substring(10, 3);

            string dateBirth = secondLine.Substring(13, 6);
            string checkDigit2 = secondLine.Substring(19, 1);

            string gender = secondLine.Substring(20, 1);

            string expirationDate = secondLine.Substring(21, 6);
            string checkDigit3 = secondLine.Substring(27, 1);

            string personalNumber = secondLine.Substring(28, 14);
            string checkDigit4 = secondLine.Substring(42, 1);

            string checkDigit5 = secondLine.Substring(43, 1);

            JSONObject resp = new()
            {
                { "pcode", pcode },
                { "type", type },
                { "country", country },
                { "names", names },

                { "pnumber", pnumber },
                { "checkDigit1", checkDigit1 },

                { "nationality", nationality },

                { "dateBirth", dateBirth },
                { "checkDigit2", checkDigit2 },

                { "gender", gender },

                { "expirationDate", expirationDate },
                { "checkDigit3", checkDigit3 },

                { "personalNumber", personalNumber },
                { "checkDigit4", checkDigit4 },

                { "checkDigit5", checkDigit5 }
            };

            return resp;
        }

        /// <summary>
        /// Returns the document number, including trailing '<' until length 9.
        /// </summary>
        /// <param name="documentNumber">the original document number</param>
        /// <returns>the documentNumber with at least length 9</returns>
        public static String FixDocumentNumber(String documentNumber)
        {
            StringBuilder maxDocumentNumber = new StringBuilder(documentNumber == null ? "" : documentNumber.Replace('<', ' ').Trim().Replace(' ', '<'));
            while (maxDocumentNumber.Length < 9)
            {
                maxDocumentNumber.Append('<');
            }
            return maxDocumentNumber.ToString();
        }

        public static char ParseGender(int gender)
        {
            switch (gender)
            {
                case 1:
                    return 'M';
                case 2:
                    return 'F';
                case 0:
                    return 'U';
                default:
                    return '-';
            }
        }

        public static string ConvertFromMrzDate(string mrzDate)
        {
            DateTime date = DateTime.ParseExact(mrzDate, "yyMMdd", CultureInfo.InvariantCulture);
            return date.ToString("dd.MM.yyyy");
        }
    }
}
