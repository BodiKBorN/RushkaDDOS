using System;
using System.Text;

namespace RushkaDDOS
{
    public static class GenerateNameHelper
    {
        private static readonly Random Random = new();

        private const string Letters = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string LettersAndDigits = Letters + Digits;

        public static string GeneratePrefixName(this string inputString, int length = 20)
            => GenerateName(length) + inputString.ToLower();

        public static string GeneratePostfixName(this string inputString, int length = 20)
            => inputString.ToLower() + GenerateName(length);

        public static string GenerateName(int length = 20)
        {
            length = length > 0
                ? length
                : 64;

            var generateString = new StringBuilder();

            for (var i = 0; i < length; i++)
                generateString.Append(GetRandomValueFromString(LettersAndDigits));

            return generateString.ToString();
        }

        private static char GetRandomValueFromString(string str)
            => str[Random.Next(0, str.Length)];
    }
}