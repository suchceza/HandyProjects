using System;

namespace SpaceReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "halid ziya ali";

            Console.WriteLine(ReplaceSpaces(s));

            Console.ReadLine();
        }

        static string ReplaceSpaces(string s)
        {
            var spaceCount = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ') spaceCount++;
            }

            if (spaceCount == 0) return s;

            var newStrLen = s.Length + spaceCount * 2;
            var newStr = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    newStr += "%20";
                    continue;
                }

                newStr += s[i];
            }

            return newStr;
        }
    }
}
