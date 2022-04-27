using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;

namespace AOC2021_Test
{
    [TestFixture]
    public class Day3
    {
        // Saving List into strings will make it easier to read (i.e. treat numbers like a string)
        List<string> binaryNumbers;
        int numberOfBits;

        [SetUp]
        public void SetUp()
        {
            // Read all lines of numbers from txt file to a list of strings
            binaryNumbers = File.ReadAllLines("Day3.txt").ToList();
            // Save # of bits 
            numberOfBits = binaryNumbers[0].Length;
        }

        [Test]
        public void Part1()
        {
            // Use an enumerable range in order to get an array of numbers from index 0 until the last bit to get an index of each bit.
            // Use Linq to query for the character in position x for each of the numbers. This will get all of the numbers in the first column and so on...
            // Each column in number (i.e. char string) is a bit.
            // We count the number of bits that are equal to one and do the same for 0.  
            var gammaRate = new string(Enumerable.Range(0, numberOfBits)
                                                        .Select( x=> binaryNumbers.Count(c => c[x] == '1') >
                                                                        binaryNumbers.Count(c => c[x] == '0') ? '1' : '0')
                                                        .ToArray());
            // Since epsilon rate is the opposite of gamma rate, we can reuse the gammaRate array with opposite values.  A 1 in gamma rate will equal 0 in 
            //epsilon rate.
            var epsilonRate = new string(gammaRate.Select(i => i == '1' ? '0' : '1')
                                                         .ToArray());
            // Get the power consumption by multiplying gammaRate to epsilonRate. Use Convert.ToInt32 since we made two string arrays.  
            // Also, indicate 2 as the base since we are working with binary numbers.
            var powerConsumption = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2); 


            Assert.That(powerConsumption, Is.EqualTo(3687446));
        }

        [Test]
        public void Part2()
        {
            var oxygenGeneratorRating = GetOxygenGeneratorRating();
            var co2ScrubberRating = GetCo2ScrubberRating();

            var lifeSupportRating = oxygenGeneratorRating * co2ScrubberRating;

            Assert.That(lifeSupportRating, Is.EqualTo(4406844));
        }

         private int GetOxygenGeneratorRating()
        {
            var numbers = binaryNumbers.ToList();

            for (int i = 0; i < numberOfBits; i++)
            {
                var mostCommonNo = numbers.Count(c => c[i] == '1') >= numbers.Count(c => c[i] == '0') ? '1' : '0';
                numbers.RemoveAll(x=> x[i] != mostCommonNo);
                if (numbers.Count == 1) break;
            }
            return Convert.ToInt32(numbers.First(), 2);
        }

        private int GetCo2ScrubberRating()
        {
            var numbers = binaryNumbers.ToList();

            for (int i = 0; i < numberOfBits; i++)
            {
                var leastCommonNo = numbers.Count(c => c[i] == '1') < numbers.Count(c => c[i] == '0') ? '1' : '0';
                numbers.RemoveAll(x => x[i] != leastCommonNo);
                if (numbers.Count == 1) break;
            }
            return Convert.ToInt32(numbers.First(), 2);
        }

    }
}
