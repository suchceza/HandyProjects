using System;
using System.Linq;

namespace StringHasUniqueCharacters
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "halit";

            if (IsUniqueChar_BookSolution(s) && IsUniqueChar_MySolution(s))
            {
                Console.WriteLine("string has all unique chars");
            }
            else
            {
                Console.WriteLine("string has duplicate chars");
            }

            Console.ReadLine();
        }

        static bool IsUniqueChar_MySolution(string s)
        {
            return s.Distinct().Count() == s.Length;
        }

        static bool IsUniqueChar_BookSolution(string s)
        {
            var charSet = new bool[256];
            for (int i = 0; i < s.Length; i++)
            {
                int value = s[i];
                if (charSet[value])
                {
                    return false;
                }

                charSet[value] = true;
            }

            return true;
        }
    }
}
