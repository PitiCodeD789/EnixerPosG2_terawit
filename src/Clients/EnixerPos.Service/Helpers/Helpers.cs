using EnixerPos.Api.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace EnixerPos.Service.Helpers
{
    public static class Helper
    {
        public static string BaseUrl { get; set; } = StaticValue.BaseUrl + ":30000/api/";

        public static bool CheckEmailFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            try
            {
                MailAddress m = new MailAddress(email);
                if (Regex.IsMatch(email, "^[\\w\\.@]{0,64}$"))
                {
                    return true;
                }
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool CheckDigitaAndLength(string digit, int length)
        {
            string pattern = @"^\d{" + length + @"}$";
            return Regex.IsMatch(digit, pattern);
        }

        public static bool CheckDigita(string digit)
        {
            string pattern = @"^(\d)+$";
            return Regex.IsMatch(digit, pattern);
        }

        public static bool CheckNonSpecialChar(string word)
        {
            string pattern = @"^(\w)+$";
            return Regex.IsMatch(word, pattern);
        }
    }
}
