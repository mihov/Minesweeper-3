using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MinesweeperGame;
using MinesweeperGame.Interfaces;

namespace MinesweeperGame.UnitTests
{
    [TestClass]
    public class ConsoleInputTest
    {
        [TestMethod]
        public void TestGetCommand()
        {
            var consoleInput = new ConsoleInput();
            var input = "1 1";
            var entrance = "Enter row and column: ";

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(input))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    var expexted = consoleInput.GetCommand();

                    // Assert
                    var result = sw.ToString();
                    Assert.AreEqual(expexted, input);
                    Assert.AreEqual(result, entrance);
                }
            }
        }

        [TestMethod]
        public void TestGetUserName()
        {
            var consoleInput = new ConsoleInput();
            var input = "Ivan Ivanov";
            var entrance = "Please enter your name for the top scoreboard: ";

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(input))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    var expexted = consoleInput.GetUserName();

                    // Assert
                    var result = sw.ToString();
                    Assert.AreEqual(expexted, input);
                    Assert.AreEqual(result, entrance);
                }
            }
        }
    }
}
