using System;
using System.Security.Cryptography;
using System.Text;

namespace Messenger.Services.Utility
{
    public static class Utilities
    {
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);

            var hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static string ComputeToken()
        {
            return ComputeHash(DateTime.Now.ToString("O") + new Random().Next(int.MinValue,int.MaxValue), 
                new MD5CryptoServiceProvider());
        }


    }
}