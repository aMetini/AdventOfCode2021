using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;

namespace AOC2021_Test
{
    [TestFixture]
    public class Day2
    {
        // List of tuples called navCommands wherein the first value is value Direction (string) and second value is Position (int)
        List<(string Direction, int PositionValue)> navCommands;

        [SetUp]
        public void SetUp()
        {
           navCommands = File.ReadAllLines("Day2.txt") 
                // Split txt file lines into two parts
                .Select(x => x.Split())
                // Part 1 (index 0) is the string direction and Part 2 (index 1) is the int position value
                .Select(x => (x[0], int.Parse(x[1])))
                // The parsed out commands are then stored into a list
                .ToList();
        }

        [Test]
        public void Part1()
        {
            var horizontalPosition = 0;
            var depthPosition = 0;

           foreach (var (direction, posValue) in navCommands)
            {
                if (direction == "forward") horizontalPosition += posValue;
                if (direction == "down") depthPosition += posValue;
                if (direction == "up") depthPosition -= posValue;
            }

           Assert.That(horizontalPosition * depthPosition, Is.EqualTo(2070300));
        }

        [Test]
        public void Part2()
        {
            var horizontalPosition = 0;
            var depthPosition = 0;
            var aim = 0;

            foreach (var (direction, posValue) in navCommands)
            {
                if (direction == "forward")
                {
                    horizontalPosition += posValue;
                    depthPosition += aim * posValue;
                }

                if (direction == "down") aim += posValue;
                if (direction == "up") aim -= posValue;
            }

            Assert.That(horizontalPosition * depthPosition, Is.EqualTo(2078985210));
        }
    }
}
