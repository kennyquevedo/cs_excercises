using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SockMerchant
{
    public SockMerchant()
    {

    }

    public void SockMerchant_StartProcess()
    {
        // int n = Convert.ToInt32(Console.ReadLine());
        // int[] ar = Array.ConvertAll(Console.ReadLine().Split(' '), arTemp => Convert.ToInt32(arTemp));

        // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = 7;
        int[] ar = new int[7] { 1, 2, 1, 2, 1, 3, 2 };

        // int n = 10;
        // int[] ar = new int[10] { 1, 1, 3, 1, 2, 1, 3, 3, 3, 3 };        

        // int n = 10;
        // int[] ar = new int[10] { 1, 1, 3, 1, 2, 1, 3, 3, 3, 3 };        

        // int result = SockMerchant_Process(n, ar);
        int result = SockMerchant_Process_Discu(n, ar);


        // textWriter.WriteLine(result);

        // textWriter.Flush();
        // textWriter.Close();
    }

    static int SockMerchant_Process_Discu(int n, int[] ar)
    {
        List<int> colors = new List<int>();
        int pairs = 0;

        for (int i = 0; i < n; i++)
        {
            if (!colors.Contains(ar[i]))
            {
                colors.Add(ar[i]);
            }
            else
            {
                pairs++;
                colors.Remove(ar[i]);
            }
        }

        Console.WriteLine(pairs);
        return pairs;
    }

    static int SockMerchant_Process(int n, int[] ar)
    {
        int matchingPairs = 0;
        if (n > 1 && n <= 100)
        {
            //group numbers
            var numbersGroup = ar.GroupBy(p => p);

            //get same numbers by group  
            foreach (var num in numbersGroup.Select(n => n.Key))
            {
                var sameNumbers = ar.Where(n => n == num);
                if (sameNumbers.Count() > 2)
                {
                    if (sameNumbers.Count() % 2 == 0)
                    {
                        for (int i = 0; i < sameNumbers.Count(); i += 2)
                        {
                            matchingPairs++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < sameNumbers.Count() - 1; i += 2)
                        {
                            matchingPairs++;
                        }
                    }
                }
                else if (sameNumbers.Count() == 2)
                {
                    matchingPairs++;
                }
            }

            Console.WriteLine(matchingPairs.ToString());
        }

        return matchingPairs;
    }
}