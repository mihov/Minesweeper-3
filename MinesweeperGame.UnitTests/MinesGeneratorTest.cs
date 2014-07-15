namespace MinesweeperGame.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MinesweeperGame;
    using MinesweeperGame.Interfaces;
    using System;
    using Telerik.JustMock;
    
    /// <summary>
    /// Unit test for the class MinesGenerator
    /// </summary>
    [TestClass]
    public class MinesGeneratorTest
    {
        [TestMethod]
        public void CheckIfSingleElementMinesFieldMatrixIsFiledWithMine()
        {
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            Random random = Mock.Create<Random>();
            string[,] minesField = minesGenerator.FillWithRandomMines(1, 1, 1, random);
            Assert.AreNotEqual(minesField[0, 0], "");
        }
    }
}