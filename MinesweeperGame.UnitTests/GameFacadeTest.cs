using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MinesweeperGame.UnitTests
{
    [TestClass]
    public class GameFacadeTest
    {
        [TestMethod]
        public void TestInstance()
        {
            GameFacade minesweeperGame1 = GameFacade.Instance;
            GameFacade minesweeperGame2 = GameFacade.Instance;

            Assert.AreSame(minesweeperGame1, minesweeperGame1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
        "Line input cannot be null (empty continue command)")]
        public void TestRun()
        {
            string input = null;

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(input))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    GameFacade minesweeperGame = GameFacade.Instance;
                    var started = minesweeperGame.Run();
                }
            }
        }
    }
}
