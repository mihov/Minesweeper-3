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
        private IMinesweeperFactory factory;
        private IMinesGenerator minesGenerator;
        private Random random;

        /// <summary>
        /// Initializing test parameters before every test
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.factory = new MinesweeperFactory();
            this.minesGenerator = factory.GetMinesGenerator();
            this.random = Mock.Create<Random>();
        }

        [Timeout(1000)]
        [TestMethod]
        public void CheckIfSingleElementMinesFieldMatrixIsFiledWithMine()
        {
            string[,] minesField = minesGenerator.FillWithRandomMines(1, 1, 1, random);
            // TODO: Change "*" with MediatorExtensions.MINES_SYMBOL 
            // use this [assembly: InternalsVisibleTo("MyExample.ServiceLayer")] to access internal class
            Assert.AreEqual(minesField[0, 0], "*"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithRowsSmallerThanOne()
        {
            minesGenerator.FillWithRandomMines(-1, 1, 1, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithColumnsSmallerThanOne()
        {
            minesGenerator.FillWithRandomMines(1, -1, 1, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithMineCountSmallerThanOne()
        {
            minesGenerator.FillWithRandomMines(1, 1, -1, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithNullRandom()
        {
            minesGenerator.FillWithRandomMines(1, 1, 1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithMinesMoreThanFieldPositions()
        {
            minesGenerator.FillWithRandomMines(1, 1, 2, random);
        }
    }
}