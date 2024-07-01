using System;
using System.Collections.Generic;

public class CharCodeComparer
{
    public (List<int> CharCodes1, List<int> CharCodes2) GetCharCodes(string input1, string input2)
    {
        List<int> charCodes1 = GetCharCodesFromString(input1);
        List<int> charCodes2 = GetCharCodesFromString(input2);

        return (charCodes1, charCodes2);
    }

    private List<int> GetCharCodesFromString(string input)
    {
        List<int> charCodes = new List<int>();

        foreach (char c in input)
        {
            charCodes.Add(c);
        }

        return charCodes;
    }
}

//// Usage example:
//class Program
//{
//    static void Main()
//    {
//        var comparer = new CharCodeComparer();
//        string input1 = "Hello";
//        string input2 = "World";

//        var (charCodes1, charCodes2) = comparer.GetCharCodes(input1, input2);

//        Console.WriteLine("Char codes for input1:");
//        foreach (var code in charCodes1)
//        {
//            Console.WriteLine(code);
//        }

//        Console.WriteLine("Char codes for input2:");
//        foreach (var code in charCodes2)
//        {
//            Console.WriteLine(code);
//        }
//    }
//}
