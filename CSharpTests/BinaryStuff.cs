using NUnit.Framework;

namespace CSharpTests
{
    [TestFixture]
    public class BinaryStuff
    {
        [Test]
        public void Number1InBinaryAndHex()
        {
            int binary1 = 0b1;
            int hex1 = 0x1;

            Assert.AreEqual(1, binary1);
            Assert.AreEqual(1, hex1);
        }

        [Test]
        public void First4BitsEqual15()
        {
            int first4hex = 0xF;
            int first4binary = 0b1111;

            Assert.AreEqual(15, first4binary);
            Assert.AreEqual(15, first4hex);
        }

        [Test]
        public void First5BitsEqual31()
        {
            int first5binary = 0b11111;
            int first5hex = 0x1F;

            Assert.AreEqual(31, first5binary);
            Assert.AreEqual(31, first5hex);
        }

        [Test]
        public void First8BitsEqual255()
        {
            int first8hex = 0xFF;
            int first8binary = 0b11111111;

            Assert.AreEqual(31, first8hex);
            Assert.AreEqual(31, first8binary);
        }

        [Test]
        public void First16BitsEqual65535()
        {
            int first16hex = 0xFFFF;
            int first16binary = 0b1111111111111111;

            Assert.AreEqual(65535, first16hex);
            Assert.AreEqual(65535, first16binary);
        }

        [Test]
        public void First31BitsEqualMaxValue()
        {
            int maxValueBinary = 0b01111111111111111111111111111111;
            int maxValueHex = 0x7FFFFFFF;
            
            Assert.AreEqual(int.MaxValue, 2147483647);
            Assert.AreEqual(int.MaxValue, maxValueBinary);
            Assert.AreEqual(int.MaxValue, maxValueHex);
        }

        [Test]
        public void MaxValuePlus1EqualMinValue()
        {
            int maxValueHex = 0x7FFFFFFF;

            Assert.AreEqual(int.MinValue, maxValueHex + 1);
        }

        [Test]
        public void SingleSignedBitEqualMinValue()
        {
            unchecked { 
                int minValueBinary = (int)0b10000000000000000000000000000000;
                int minValueHex = (int)0x80000000;

                Assert.AreEqual(int.MinValue, -2147483648);
                Assert.AreEqual(int.MinValue, minValueBinary);
                Assert.AreEqual(int.MinValue, minValueHex);
            }
        }

        [Test]
        public void AllBitsEqualMinus1()
        {
            unchecked {
                int minus1binary = (int)0b11111111111111111111111111111111;
                int minus1hex = (int)0xFFFFFFFF;

                Assert.AreEqual(-1, minus1binary);
                Assert.AreEqual(-1, minus1hex);
            }
        }

        [Test]
        public void AllBitsAndOperator40Equal40()
        {
            unchecked
            {
                int allBits = (int)0xFFFFFFFF;

                Assert.AreEqual(40, allBits & 40);
            }
        }

        [Test]
        public void SimpleAndOperator()
        {
            Assert.AreEqual(1, 3 & 5);
        }

        [Test]
        public void CullingAndShifting()
        {

            int usefulForAndOperator = 0b00010100111010101101011011101010;
            int culler = 0b00011100000000000000000000000000;

            int andResult = usefulForAndOperator & culler;
            int shiftResult = andResult >> 26;

            Assert.AreEqual(0b00010100000000000000000000000000, andResult);
            Assert.AreEqual(5, shiftResult);
        }

        [Test]
        public void ShiftingNegativeValue31TimesNeedsToCastToUnsignedToWork()
        {
            unchecked
            {
                int allBits = (int)0xFFFFFFFF;

                Assert.AreEqual(-1, allBits >> 31); // Might expect this to be 1, but it actually is -1
                Assert.AreEqual(1, ((uint)allBits) >> 31);
            }
        }

        [Test]
        public void ShiftingMaxValueTo1With30Shifts()
        {
            Assert.AreEqual(1, int.MaxValue >> 30); // Even though int is 32-bits the first bit is the sign bit so we don't shift 31 times but only 30 times
        }

        [Test]
        public void LeftShifting1ToDouble()
        {
            int binary1 = 0b1;
            Assert.AreEqual(2, binary1 << 2);
        }

        [Test]
        public void ShiftingAbove31BitsOnlyUsesFirstFiveBits()
        {
            Assert.AreEqual(0, 32 & 31);
            Assert.AreEqual(3, 3 << 32);
            Assert.AreEqual(3, 3 >> 32);
        }
    }
}
