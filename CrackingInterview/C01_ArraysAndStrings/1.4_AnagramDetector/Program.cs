using System;

namespace AnagramDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            var s1 = "listen";
            var s2 = "silent";

            Console.WriteLine(AreStringsAnagram(s1, s2));

            Console.ReadLine();
        }

        static bool AreStringsAnagram(string s1, string s2)
        {
            if (s1.Length != s2.Length) return false;

            var total = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                total += s1[i];
                total -= s2[i];
            }

            return total == 0;
        }
    }
}
