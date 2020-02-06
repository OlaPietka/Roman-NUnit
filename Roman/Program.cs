using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roman
{
    public class RomanUtils
    {
        public static bool LowerCase;

        public static string ToRoman(int number) //Metoda do przeliczania liczby arabskiej na liczbę rzysmką
        {
            if ((number < 0) || (number > 3999)) //Warunek by liczba arabska wpisana miescila sie w zakresie <1,3999>
                throw new ArgumentOutOfRangeException();

            string roman = "";

            while (number > 0 && number < 4000)
            {
                if (number >= 1000)
                {
                    roman += "M";
                    number -= 1000;
                }
                else if (number >= 900)
                {
                    roman += "CM";
                    number -= 900;
                }
                else if (number >= 500)
                {
                    roman += "D";
                    number -= 500;
                }
                else if (number >= 400)
                {
                    roman += "CD";
                    number -= 400;
                }
                else if (number >= 100)
                {
                    roman += "C";
                    number -= 100;
                }
                else if (number >= 90)
                {
                    roman += "XC";
                    number -= 90;
                }
                else if (number >= 50)
                {
                    roman += "L";
                    number -= 50;
                }
                else if (number >= 40)
                {
                    roman += "XL";
                    number -= 40;
                }
                else if (number >= 10)
                {
                    roman += "X";
                    number -= 10;
                }
                else if (number >= 9)
                {
                    roman += "IX";
                    number -= 9;
                }
                else if (number >= 5)
                {
                    roman += "V";
                    number -= 5;
                }
                else if (number >= 4)
                {
                    roman += "IV";
                    number -= 4;
                }
                else if (number >= 1)
                {
                    roman += "I";
                    number -= 1;
                }
                else
                    return "Something bad happend";
            }
            return roman;
        }

        private static Dictionary<char, int> RomanDictionary = new Dictionary<char, int>() //"Słownik" do przypisania wartości liczbowych do danych znaków
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        public static int FromRoman(string roman) //Metoda przeliczająca liczbę rzymską na liczbę arabską
        {
            if (LowerCase)
            {
                int number = 0;
                int stack = 0;

                for (int i = 0; i < roman.Length; i++)
                {
                    if (i == roman.Length - 1) //Ostatnia cyfra
                    {
                        number += RomanDictionary[roman[i]];
                        break;
                    }

                    int index = RomanDictionary[roman[i]];
                    int index_plus_1 = RomanDictionary[roman[i + 1]];

                    if (index == index_plus_1)
                        stack += 1;
                    else
                        stack = 0;

                    if (stack >= 3) //Jezli powatrzaja sie znaki 4 lub wiecej razy
                        throw new ArgumentException();

                    if (index == index_plus_1 && (((index + index_plus_1) / 10 == 1) || ((index + index_plus_1) / 10 == 10) || ((index + index_plus_1) / 10 == 100))) 
                        throw new ArgumentException(); //Nie moze byc 5, 50 i 500 wiecej niz 1 raz obok siebie

                    if (i + 1 < roman.Length && index < index_plus_1)
                    {
                        if (index_plus_1 - index == index) //Jezeli wieksza odjac mniejsza jest rowne mniejsza, np. DM (1000-500=500)
                            throw new ArgumentException();

                        number -= index;
                    }
                    else
                        number += index;
                }
                return number;
            }
            else
                throw new ArgumentException();
        }

        public static string IfLowerCase(string input)
        {
            return input.ToUpper();
        }
    }
}