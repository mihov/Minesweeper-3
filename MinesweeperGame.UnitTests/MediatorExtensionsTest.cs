using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MinesweeperGame.UnitTests
{
    /// <summary>
    /// Unit test for the class MediatorExtensions.cs
    /// Full Tested
    /// </summary>
    [TestClass]
    public class MediatorExtensionsTest
    {
        [TestMethod]
        public void TestIsValidCommand()
        {
            var trueCommands = new string[4] { "top", "restart", "exit", "fulldelete" };
            var falseCommands = new string[4] { "tOp", "rEstart", "eexit", "fulldeletee" };

            foreach (var cmd in trueCommands)
            {
                Assert.AreEqual(true, MediatorExtensions.IsValidCommand(cmd));
            }

            foreach (var cmd in falseCommands)
            {
                Assert.AreEqual(false, MediatorExtensions.IsValidCommand(cmd));
            }
        }

        [TestMethod]
        public void TestIsMoveEntered()
        {
            var refX = 0; 
            var refY = 0;
            var minX = 0;
            var minY = 0;
            var maxX = MediatorExtensions.MINES_FIELD_ROWS - 1;
            var maxY = MediatorExtensions.MINES_FIELD_COLS - 1;

            var trueListOfXandYvalues = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(minX, maxY),
                new Tuple<int, int>(maxX, minY),
                new Tuple<int, int>(minX + maxX / 2 , minY + maxY)
            };

            var falseListOfXandYvalues = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(minX - 1, "x"),
                new Tuple<int, string>(maxX + 1, String.Empty),
                new Tuple<int, string>(maxX + 1, String.Empty),
            };

            foreach (var tup in trueListOfXandYvalues)
            {
                string currentString = Convert.ToString(tup.Item1 + " " + tup.Item2);
                Assert.AreEqual(true, MediatorExtensions.IsMoveEntered(currentString, ref refX, ref refY));
            }

            foreach (var tup in falseListOfXandYvalues)
            {
                string currentString = Convert.ToString(tup.Item1 + "  " + tup.Item2);
                Assert.AreEqual(false, MediatorExtensions.IsMoveEntered(currentString, ref refX, ref refY));
            }
        }

        [TestMethod]
        public void TestHasExplodedOne()
        {
            string[,] matrix = new string[7, 7]
                {
                    {" "," "," ","*","*","*"," "},
                    {" ","*"," "," "," ","*"," "},
                    {" ","*"," ","*","*","*"," "},
                    {" ","*"," "," "," "," "," "},
                    {" ","*"," ","*","*","*"," "},
                    {" ","*","*","*","*","*"," "},
                    {"*"," "," "," "," "," "," "},
                };


            var trueListOfXandYvalues = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(0, 3),
                new Tuple<int, int>(0, 5),
            };

            foreach (var tup in trueListOfXandYvalues)
            {
                Assert.AreEqual(true, MediatorExtensions.HasExploded(matrix, tup.Item1, tup.Item2));
            }

            var falseListOfXandYvalues = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 0),
                new Tuple<int, int>(0, 2),
                new Tuple<int, int>(6, 6)
            };

            foreach (var tup in falseListOfXandYvalues)
            {
                Assert.AreEqual(false, MediatorExtensions.HasExploded(matrix, tup.Item1, tup.Item2));
            }
        }

        [TestMethod]
        public void TestHasExplodedTwo()
        {
            string[,] matrix = new string[3, 3]
                {
                    {" "," ","*"},
                    {" ","*"," "},
                    {" ","*"," "},
                };


            var trueListOfXandYvalues = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 2),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 1),
            };

            foreach (var tup in trueListOfXandYvalues)
            {
                Assert.AreEqual(true, MediatorExtensions.HasExploded(matrix, tup.Item1, tup.Item2));
            }

            var falseListOfXandYvalues = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 0),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 0),
                new Tuple<int, int>(2, 2),
            };

            foreach (var tup in falseListOfXandYvalues)
            {
                Assert.AreEqual(false, MediatorExtensions.HasExploded(matrix, tup.Item1, tup.Item2));
            }
        }

        [TestMethod]
        public void TestIsWinner()
        {
            string[,] matrix = new string[7, 7]
                {
                    {" "," "," ","*","*","*"," "},
                    {" ","*"," "," "," ","*"," "},
                    {" ","*"," ","*","*","*"," "},
                    {" ","*"," "," "," "," "," "},
                    {" ","*"," ","*","*","*"," "},
                    {" ","*","*","*","*","*"," "},
                    {"*"," "," "," "," "," "," "},
                };
            Assert.AreEqual(true, MediatorExtensions.IsWinner(matrix, 20));
        }
    }
}
