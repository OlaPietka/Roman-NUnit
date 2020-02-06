using System;
using NUnit.Framework;
using Roman;

namespace UnitTestRoman
{
    [TestFixture]
    public class UnitTest1
    {

        [SetUp]
        public void Setup_False_If_LowerCase_Are_Disabled()
        {
            RomanUtils.LowerCase = false;
        }

        [Test]
        public void If_LowerCase_Are_Disabled_Throw_Exception()
        {
            
            Assert.That(() => RomanUtils.FromRoman("cd"),
              Throws.TypeOf<ArgumentException>());
        }
    }
}
