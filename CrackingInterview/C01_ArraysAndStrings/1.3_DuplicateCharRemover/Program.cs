using System;

namespace DuplicateCharRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "Yigit Ali";

            Console.WriteLine(RemoveDuplicates_MySolution(s));

            var strArr = s.ToCharArray();
            RemoveDuplicates_BookSolution(strArr);
            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write(strArr[i]);
            }

            Console.ReadLine();
        }

        static string RemoveDuplicates_MySolution(string s)
        {
            var charList = new bool[256];
            var newString = string.Empty;

            for (int i = 0; i < s.Length; i++)
            {
                var val = s[i];
                if (charList[val])
                {
                    continue;
                }
                charList[val] = true;
                newString += s[i];
            }

            return newString;
        }

        static void RemoveDuplicates_BookSolution(char[] strArr)
        {
            if (strArr == null) return;

            int len = strArr.Length;
            if (len < 2) return;

            int tail = 1;

            for (int i = 1; i < len; ++i)
            {
                int j;
                for (j = 0; j < tail; ++j)
                {
                    if (strArr[i] == strArr[j]) break;
                }

                if (j == tail)
                {
                    strArr[tail] = strArr[i];
                    ++tail;
                }
            }

            strArr[tail] = ' ';
        }
    }
}
