using System;

public class EnumToStringTest
{
    // Enum   
    public enum Weekday
    {
        Monday = 0, Tuesday = 1, Wednesday = 2, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7
    }

    public EnumToStringTest()
    {
        Enum wkday = Weekday.Friday;
        Console.WriteLine("Enum string is '{0}'", wkday.ToString());
        Console.ReadKey();
    }
}