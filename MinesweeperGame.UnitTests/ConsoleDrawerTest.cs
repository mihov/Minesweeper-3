using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MinesweeperGame.UnitTests
{
    [TestClass]
    public class ConsoleDrawerTest
    {
        [TestMethod]
        public void TestShowWelcome()
        {
            var concoleDrawer = new ConsoleDrawer();
            var entrance = "Hi, Enter row and column: ";

            using (var sw = new StringWriter())
            {               
                Console.SetOut(sw);

                // Act
                concoleDrawer.ShowWelcome(entrance);

                // Assert
                var result = sw.ToString();
                Assert.AreEqual(result, entrance + "\n\r\n");            
            }
        }

        [TestMethod]
        public void TestShowGameEnd()
        {
            var concoleDrawer = new ConsoleDrawer();
            var entrance = "Bye, You lose";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                concoleDrawer.ShowGameEnd(entrance);

                // Assert
                var result = sw.ToString();
                Assert.AreEqual(result, "\n" + entrance + "\n\r\n");              
            }
        }

        [TestMethod]
        public void TestMessage()
        {
            var concoleDrawer = new ConsoleDrawer();
            var entrance = "This is Message test";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                concoleDrawer.Message(entrance);

                // Assert
                var result = sw.ToString();
                Assert.AreEqual(result, entrance + "\r\n");
            }
        }
    }
}
