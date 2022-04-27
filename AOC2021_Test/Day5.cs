using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021_Test
{
    [TestFixture]
    public class Day5
    {
        private List<(int X1, int Y1, int X2, int Y2)> vents;

        [SetUp]
        public void Setup()
        {
            vents = File.ReadAllLines("Day5.txt")
                .Select(v =>
                    (int.Parse(v.Split(" -> ")[0].Split(",")[0]),
                    int.Parse(v.Split(" -> ")[0].Split(",")[1]),
                    int.Parse(v.Split(" -> ")[1].Split(",")[0]),
                    int.Parse(v.Split(" -> ")[1].Split(",")[1])))
                    .ToList();

        }

        [Test]
        public void PartI()
        {

        }
    }
}
