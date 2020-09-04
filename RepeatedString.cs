using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RepeatedString
{

    static long repeatedString(string s, long n)
    {
        long result = 0;

        var x = s.Where(l => l == 'a').Count() * (n / s.Length) + s.Substring(0, (int)(n % s.Length)).Where(l => l == 'a').Count();

        return result;
    }
    static long repeatedString2(string s, long n)
    {
        long result = 0;

        if ((s.Length >= 1 && s.Length <= 100) &&
            n >= 1 && n <= Math.Pow(10, 12))
        {

            //Substring
            string subStr = String.Empty;

            //repeat the string
            long numberOfRepeat = Convert.ToInt64(n / s.Length);

            //max capacity
            int maxCapacity = 0;
            if (numberOfRepeat > int.MaxValue)
            {
                maxCapacity = int.MaxValue;
                int times = (maxCapacity / 1000000);
                for (int i = 0; i < times; i++)
                {
                    subStr += new StringBuilder(1000000).Insert(0, s, 1000000).ToString();
                }

                maxCapacity = (int)(numberOfRepeat - int.MaxValue);
                subStr += new StringBuilder(maxCapacity).Insert(0, s, maxCapacity).ToString();
            }
            else
            {
                maxCapacity = (int)numberOfRepeat;
                subStr = new StringBuilder(maxCapacity).Insert(0, s, maxCapacity).ToString();
            }




            // subStr = new String(s, numberOfRepeat);
            // for (int i = 0; i < numberOfRepeat; i++)
            // {
            //     subStr += s;
            // }

            //calculate left elements
            if (n > subStr.Length)
            {
                long leftElements = Convert.ToInt64(n - subStr.Length);
                if (leftElements <= s.Length)
                {
                    subStr += s.Substring(0, (int)leftElements);
                }
            }


            Console.WriteLine(subStr);
            result = subStr.Where(l => l == 'a').Count();

        }

        return result;
    }

    public RepeatedString()
    {
        string s = "a";
        // string s = "abcac";
        // long n = 10;
        long n = 1000000000000;

        long result = repeatedString(s, n);

        Console.WriteLine(result);
    }
}