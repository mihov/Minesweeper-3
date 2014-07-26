using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

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

        [TestMethod]
        public void TestPrintScoreBoardEmpty()
        {
            var concoleDrawer = new ConsoleDrawer();

            var DATA_ROOT = "..\\..\\..\\MinesweeperGame.Demo\\players.xml";
            var scoreBoard = new ScoreBoard(DATA_ROOT);
            Repository dataRepository = new Repository();
          
            // remove all test and get all test
            dataRepository.EmptyFile(DATA_ROOT);
            var expectedResult = "\r\nScoreboard empty!\r\n\r\n";

            IList<KeyValuePair<int, IList<string>>> highScores = scoreBoard.GetHighScores(MediatorExtensions.NUMBER_OF_SHOWED_SCORES);


            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                concoleDrawer.PrintScoreBoard(highScores);

                // Assert
                var result = sw.ToString();
                Assert.AreEqual(result, expectedResult);
            }
        }

        [TestMethod]
        public void TestPrintScoreBoardNotEmpty()
        {
            var concoleDrawer = new ConsoleDrawer();

            var DATA_ROOT = "..\\..\\..\\MinesweeperGame.Demo\\players.xml";
            var scoreBoard = new ScoreBoard(DATA_ROOT);
            Repository dataRepository = new Repository();

            // remove all test and get all test
            dataRepository.EmptyFile(DATA_ROOT);
            var expectedResult = "\r\nScoreboard:\r\n1. Ivelin Stanchev --> 7 cells\r\n2. Ilian Yordanov --> 6 cells\r\n3. Plamen Stanev --> 5 cells\r\n4. Tancho Mihov --> 4 cells\r\n5. Petar Milchev --> 3 cells\r\n\r\n";
          
            var playersList = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(1, "Joan Sirakov"),
                new Tuple<int, string>(2, "Stoyan Stoyanov"),
                new Tuple<int, string>(3, "Petar Milchev"),
                new Tuple<int, string>(4, "Tancho Mihov"),
                new Tuple<int, string>(5, "Plamen Stanev"),
                new Tuple<int, string>(6, "Ilian Yordanov"),
                new Tuple<int, string>(7, "Ivelin Stanchev")
            };

            foreach (var tup in playersList)
            {
                dataRepository.AddPlayer(DATA_ROOT, tup.Item2, tup.Item1);
            }

            IList<KeyValuePair<int, IList<string>>> highScores = scoreBoard.GetHighScores(MediatorExtensions.NUMBER_OF_SHOWED_SCORES);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                concoleDrawer.PrintScoreBoard(highScores);

                // Assert
                var result = sw.ToString();
                Assert.AreEqual(result, expectedResult);
            }
        }


        [TestMethod]
        public void TestDraw()
        {
            var concoleDrawer = new ConsoleDrawer();

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

            var expectedResult = "\r\n     0 1 2 3 4 5 6 7 8 9\r\n   ---------------------\r\n0 |        * * *  |\r\n1 |    *       *  |\r\n2 |    *   * * *  |\r\n3 |    *          |\r\n4 |    *   * * *  |\r\n5 |    * * * * *  |\r\n6 |  *            |\r\n   ---------------------\r\n";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                concoleDrawer.Draw(matrix, true);

                // Assert
                var result = sw.ToString();
                Assert.AreEqual(result, expectedResult);
            }
        }
    }
}
