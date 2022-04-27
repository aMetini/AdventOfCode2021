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
    public class Day4 // Solution is regarding implementation and using data structures to represent your game board and loop through the numbers to check that the rows/columns are marked and finally calculate the end result.
    {
        private List<int> numbers;
        private readonly List<List<List<int>>> boards = new();

        [SetUp]
        public void Setup()
        {
            var input = File.ReadAllLines("Day4.txt").ToList();

            // Calculate the # of boards by reading the input and taking away one line to account for the list of numbers drawn
            // (i.e. the first line in the txt file.
            // Divide the lines by 6 (to account for 5 lines of #s on a gameboard and an empty separation space)
            var noBoards = (input.Count - 1) / 6;

            // Parse your file data to a list
            numbers = input[0].Split(",").Select(int.Parse).ToList();

            for (int i = 0; i < noBoards; i++)
            {
                // Build a board by creating a List of a list of numbers/ints
                var board = new List<List<int>>();

                // We add rows to each board
                for (var row = 0; row < 5; row++)
                {
                    board.Add(input[2 + 6 * i + row].Split().Where(x => x.Trim() != "").Select(int.Parse).ToList());
                }

                boards.Add(board);
            }
        }

        [Test]
        public void PartI()
        {
            Assert.That(Game(), Is.EqualTo(74320));

            int Game()
            {
                // Determines winning board
                // Loop through all of the numbers  and then loop through all of the boards in the first
                foreach(var number in numbers)
                {
                    foreach (var board in boards)
                    {
                    // Start marking #s that are called in the game
                        for (int i = 0; i < 5; i++)
                            for (var j = 0; j < 5; j++)
                                if (board[i][j] == number)
                                    board[i][j] = -1;
                    }
                    foreach (var board in boards)
                    {
                        for (var i = 0; i < 5; i++)
                        {
                            // Determine if a board is winning by adding up each row to equal -5 or each element in a column
                            if (board[i].Sum() == -5 || board.Select(x => x[i]).Sum() == -5)
                                // Return the winning board and gather all of the unmarked #s to add.  Then multiply with the last winning #
                                return board.SelectMany(x => x).Where(x => x != -1).Sum() * number;
                        }
                    }
                }

                return 0;
            }
        }

        [Test]
        public void PartII()
        {
            // Determine losing board by lowest posisble finalScore
            var finalScore = 0;

            // Mark all of the element in the loop at the beginning
            foreach (var number in numbers)
            {
                // This marks each selected # on the board
                foreach (var board in boards)
                {
                    for (var p = 0; p < 5; p++)
                        for (var s = 0; s < 5; s++)
                            if (board[p][s] == number)
                                board[p][s] = -1;
                }

                // Keep track of winning boards
                var winningBoards = new List<List<List<int>>>();

                foreach (var board in boards)
                {
                    for (var p = 0; p < 5; p++)
                        if (board[p].Sum() == -5 || board.Select(x => x[p]).Sum() == -5)
                        {
                            // Keep track of the final score.  This score will be updated every time that there is a winning board.
                            // Therefore, the final score will be the score of the last winning board.
                            finalScore = board.SelectMany(x => x).Where(x => x != -1).Sum() * number;
                            winningBoards.Add(board);
                        }
                }

                // Remove winning boards for each round from the list of boards to find the last winning board
                foreach (var board in winningBoards)
                {
                    boards.Remove(board);
                }
            }

            // Use the final final score to check against the result
            Assert.That(finalScore, Is.EqualTo(17884));


        }

    }
}
