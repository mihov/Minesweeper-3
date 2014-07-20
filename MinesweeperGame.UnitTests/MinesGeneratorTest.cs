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
            // TODO: Change "*" with MediatorExtensions.MINES_SYMBOL 
            // use this [assembly: InternalsVisibleTo("MyExample.ServiceLayer")] to access internal class
            Assert.AreEqual(minesField[0, 0], "*"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithRowsSmallerThanOne()
        {
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            Random random = Mock.Create<Random>();
            minesGenerator.FillWithRandomMines(-1, 1, 1, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithColumnsSmallerThanOne()
        {
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            Random random = Mock.Create<Random>();
            minesGenerator.FillWithRandomMines(1, -1, 1, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithMineCountSmallerThanOne()
        {
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            Random random = Mock.Create<Random>();
            minesGenerator.FillWithRandomMines(1, 1, -1, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithNullRandom()
        {
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            minesGenerator.FillWithRandomMines(1, 1, 1, null);
        }
    }
}