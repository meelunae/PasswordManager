using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace WeirdProject
{
    public class Randomness
    {
        const string allowed = @"0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*():;[]{}<>,.?/\|";
        private  static RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();
        public static int RandomInteger(int min, int max)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);

                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
        }
        public static string RandomChar(string str)
        {
            return str.Substring(RandomInteger(0, str.Length - 1), 1);
        }
        public static string RandomizeString(string str)
        {
            string result = "";
            while (str.Length > 0)
            {
                // Pick a random character.
                int i = RandomInteger(0, str.Length - 1);
                result += str.Substring(i, 1);
                str = str.Remove(i, 1);
            }
            return result;
        }
        public static string RandomPassword()
        {
            string passwordFinal = "";
            Console.WriteLine("\n How many characters do you want your generated password to be long? \n");
            int num_chars = int.Parse(Console.ReadLine());
            while (passwordFinal.Length < num_chars)
                passwordFinal += allowed.Substring(RandomInteger(0, allowed.Length - 1), 1);
            passwordFinal = RandomizeString(passwordFinal);
            Console.WriteLine($"Your generated password is {passwordFinal} \n");
            return passwordFinal;


        }
    }
}
