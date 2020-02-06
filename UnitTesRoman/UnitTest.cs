using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Roman;


namespace UnitTestRoman
{
    [TestFixture]
    public class UnitTest
    {

        static readonly string FileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [SetUp]
        public void Setup_True_If_LowerCase_Are_Enabled()
        {
            RomanUtils.LowerCase = true;
        }

        public static IEnumerable<object[]> Roman_and_Numbers()
        {
            string path = $@"{FileLocation}\roman numerals.csv";
            return File.ReadAllLines(path)
                .Select(l => l.Split(','))
                .Select(a => new object[] { int.Parse(a[0]), a[1] });
        }

        [Test, TestCaseSource(nameof(Roman_and_Numbers))]
        public void Roman_Should_Be_Equal(int number, string roman)
        {
            Assert.AreEqual(RomanUtils.ToRoman(number), RomanUtils.IfLowerCase(roman));
        }


        [Test, TestCaseSource(nameof(Roman_and_Numbers))]
        public void Number_Should_Be_Equal(int number, string roman)
        {
            Assert.AreEqual(number, RomanUtils.FromRoman(RomanUtils.IfLowerCase(roman)));
        }

        public static string[] Romans()
        {
            string path = $@"{FileLocation}\romans.csv";
            return File.ReadAllLines(path).ToArray();
        }

        [Test, TestCaseSource(nameof(Romans))]
        public void Roman_To_Number_To_Roman_Should_Be_Equal(string roman)
        {
            roman = RomanUtils.IfLowerCase(roman);
            string result = RomanUtils.ToRoman(RomanUtils.FromRoman(roman));
            Assert.AreEqual(result, roman);
        }

        public static string[] Numbers()
        {
            string path = $@"{FileLocation}\numbers.csv";
            return File.ReadAllLines(path).ToArray();
        }

        [Test, TestCaseSource("Numbers")]
        public void Number_To_Roman_To_Number_Should_Be_Equal(string number)
        {
            string result = Convert.ToString(RomanUtils.FromRoman(RomanUtils.ToRoman(Convert.ToInt32(number))));
            Assert.AreEqual(result, number);
        }

        [Test]
        public void Out_Of_Range_Greater_Than_3999()
        {
            Random rnd = new Random();

            int number = rnd.Next(4000, 5000);

            Assert.That(() => RomanUtils.ToRoman(number),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Out_Of_Range_Less_Than_1()
        {
            Random rnd = new Random();

            int number = rnd.Next(-1000, 1);

            Assert.That(() => RomanUtils.ToRoman(number),
               Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        static string[] Invalid_Romans = new string[]
        {
            "CDCCCC",
            "MMMMM",
            "XDD",
            "IIII",
            "IVIVVV",
            "DVV",
            "LC",
            "VX"
        };

        [TestCaseSource("Invalid_Romans")]
        public void Wrong_Roman_Format(string invalid_roman)
        {
            invalid_roman = RomanUtils.IfLowerCase(invalid_roman);

            Assert.That(() => RomanUtils.FromRoman(invalid_roman),
               Throws.TypeOf<ArgumentException>());
        }

        [TestCase("sdfsd")]
        [TestCase("ferf")]
        [TestCase("???")]
        [TestCase("! ffe23")]
        public void Wrong_Characters_Input_For_Roman(string WrongRoman)
        {
            WrongRoman = RomanUtils.IfLowerCase(WrongRoman);

            Assert.That(() => RomanUtils.FromRoman(WrongRoman),
               Throws.TypeOf<System.Collections.Generic.KeyNotFoundException> ());
        }

        [Test]
        public void Wrong_Characters_Input_For_Number()
        {
            Assert.That(() => RomanUtils.ToRoman(Convert.ToInt32("? ")),
               Throws.TypeOf<System.FormatException>());
        }
    }
}