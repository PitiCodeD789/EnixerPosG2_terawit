using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.Api.ViewModels.Helpers
{
    public class Generetor
    {
        static Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
