using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinesweeperGame.UnitTests
{
    [TestClass]
    public class GameFacadeTest
    {
        [TestMethod]
        public void TestRun()
        {
            GameFacade minesweeperGame = GameFacade.Instance;
            var started = minesweeperGame.Run();
            Assert.AreEqual(true, started);
        }
    }
}
