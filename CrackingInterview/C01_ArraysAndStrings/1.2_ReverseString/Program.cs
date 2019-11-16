using System;

namespace ReverseString
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "halit";

            Console.WriteLine(Reverse(s));

            Console.ReadLine();
        }

        static string Reverse(string s)
        {
            var reversed = "";

            for (int i = s.Length - 1; i >= 0; i--)
            {
                reversed += s[i]; //ineffective space complexity
            }

            return reversed;
        }
    }
}
