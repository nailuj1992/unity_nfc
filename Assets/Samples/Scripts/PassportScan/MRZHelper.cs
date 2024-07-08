using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public static class MRZHelper
    {
        public static string ExtractMRZ(string input)
        {
            string cleanedInput = Regex.Replace(input, @"[\s\r\n]", "").Replace("«", "<").Replace("(", "<").ToUpper();
            string pattern =
                @"(P)([A-Z<])([A-Z0-9]{3})([0-9A-Z<]{1,39})([A-Z0-9<]{10})([A-Z0158]{3})([0-9OISLB]{7})([FMCK<])([0-9OISLB]{7})([A-Z0-9<]{1,14})([0-9OISLB<CK])([0-9OISLB])";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(cleanedInput);
            if (match.Success)
            {
                string documentType = match.Groups[1].Value;
                string documentTypeExtraDigit = match.Groups[2].Value;
                string countryCode = match.Groups[3].Value;
                string name = match.Groups[4].Value;
                string documentNumber = match.Groups[5].Value;
                string nationality = match.Groups[6].Value;
                string dateOfBirth = match.Groups[7].Value;
                string gender = match.Groups[8].Value;
                string expiryDate = match.Groups[9].Value;
                string additionalInfo = match.Groups[10].Value;
                string overallCheckDigits1 = match.Groups[11].Value;
                string overallCheckDigits2 = match.Groups[12].Value;

                StringBuilder resultBuilder = new StringBuilder();
                resultBuilder.Append(documentType);
                resultBuilder.Append(CorrectSingleDelimiter(documentTypeExtraDigit));
                resultBuilder.Append(CorrectAlpha(countryCode));
                //TODO
                resultBuilder.Append(CorrectAlpha(CorrectMultiDelimiter(name)).PadRight(39, '<'));
                //TODO
                resultBuilder.Append('\n');
                //TODO
                resultBuilder.Append(documentNumber);
                resultBuilder.Append(CorrectAlpha(nationality));
                resultBuilder.Append(CorrectNumber(dateOfBirth));
                resultBuilder.Append(CorrectSingleDelimiter(gender));
                resultBuilder.Append(CorrectNumber(expiryDate));
                //TODO
                resultBuilder.Append(CorrectMultiDelimiter(additionalInfo).PadRight(14, '<'));
                resultBuilder.Append(CorrectNumber(CorrectSingleDelimiter(overallCheckDigits1)));
                resultBuilder.Append(CorrectNumber(overallCheckDigits2));

                return resultBuilder.ToString();
            }

            return null;
        }

        private static string CorrectAlpha(string input)
        {
            return Regex.Replace(input, @"[0185]", m => m.Value switch
            {
                "0" => "O",
                "1" => "I",
                "8" => "B",
                "5" => "S",
                _ => m.Value
            });
        }

        private static string CorrectNumber(string input)
        {
            return Regex.Replace(input, @"[ODILSB]", m => m.Value switch
            {
                "O" or "D" => "0",
                "I" or "L" => "1",
                "S" => "5",
                "B" => "8",
                _ => m.Value
            });
        }

        private static string CorrectSingleDelimiter(string input)
        {
            return Regex.Replace(input, @"[CK]", m => m.Value switch
            {
                "C" => "<",
                "K" => "<",
                _ => m.Value
            });
        }

        private static string CorrectMultiDelimiter(string input)
        {
            return Regex.Replace(input, @"(?<=<)(C|K)+(?=<|$)|(?<=^|<)(C|K)+(?=<)", m => m.Value.Replace("C", "<").Replace("K", "<"));
        }
    }
}
