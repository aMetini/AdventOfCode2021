using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Test
{
    [TestFixture]
    public class Day1
    {
        private List<int> _depths;

        [SetUp]
        public void Setup()
        {
            _depths = File.ReadAllLines("Day1.txt").Select(x => int.Parse(x)).ToList();
        }

        [Test]
        public void Part1()
        {
            var result = 0;

            for (int i = 1; i < _depths.Count; i++)
            {
                if (_depths[i] > _depths[i - 1]) result++;
            }

            Assert.That(result, Is.EqualTo(1195));
        }

        [Test]
        public void Part2()
        {
            var result = 0;

            // Start from position 3 to refer to previous 3 measurements.  First comparison will be N/A since there is no previous measurement
            for (int i = 3; i < _depths.Count; i++)
            {
                if (_depths[i] + _depths[i - 1] + _depths[i - 2] > _depths[i - 1] + _depths[i - 2] + _depths[i - 3])
                {
                    result++;
                }
            }

            Assert.That(result, Is.EqualTo(1235));
        }
    }
}