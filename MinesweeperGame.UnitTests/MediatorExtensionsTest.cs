using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MinesweeperGame.UnitTests
{
    /// <summary>
    /// Unit test for the class MediatorExtensions.cs
    /// </summary>
    [TestClass]
    public class MediatorExtensionsTest
    {
        [TestMethod]
        public void TestIsValidCommand()
        {
            var trueCommands = new string[3] {"top", "restart", "exit"};
            var falseCommands = new string[3] { "tOp", "rEstart", "eexit" };

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
    }
}
