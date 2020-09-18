using System;
using System.Collections.Generic;
using System.Linq;

public class BirthdayCakeCandles
{
    /*
         * Complete the 'birthdayCakeCandles' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts INTEGER_ARRAY candles as parameter.
         */

    public static int birthdayCakeCandles(List<int> candles)
    {
        int tallestCandle = candles.Max();
        int totalTallestCandle = candles.Where(candle => candle == tallestCandle).Count();

        return totalTallestCandle;
    }

    public BirthdayCakeCandles()
    {
        int[] candles = new int[] { 4, 4, 1, 3 };

        birthdayCakeCandles(candles.ToList());
    }

}