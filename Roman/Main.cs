using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roman
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Convert roman to integer: ");
            string roman = Console.ReadLine();

            Console.WriteLine("Convert integer to roman: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("From roman: " + RomanUtils.FromRoman(roman));
            Console.WriteLine("To roman: " + RomanUtils.ToRoman(number));
        }
    }
}
