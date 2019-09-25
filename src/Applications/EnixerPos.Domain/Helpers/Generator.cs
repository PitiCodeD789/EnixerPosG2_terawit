using EnixerPos.Api.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EnixerPos.Domain.Helpers
{
    public class Generator
    {
        static Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                return builder.ToString();

            }
        }

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
    }
}
