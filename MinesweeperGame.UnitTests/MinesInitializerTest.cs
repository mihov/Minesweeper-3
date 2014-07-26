// ********************************
// <copyright file="MainDemo.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MinesweeperGame;
    using MinesweeperGame.Interfaces;
    using System.IO;

    /// <summary>
    /// Used to test the <see cref="MinesInitializer"/> class.
    /// </summary>
    [TestClass]
    public class MinesInitializerTest
    {
        private MinesInitializer minesInit;
        private IMinesGenerator minesGenerator;
        private IMinesweeperFactory factory;
        private IDrawer drawer;
        private IUserInput userInput;
        private IScoreBoard scoreBoard;
        private Random random;

        /// <summary>
        /// Initializing test parameters before every test
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.minesInit = MinesInitializer.Instance;
            this.factory = new MinesweeperFactory();
            this.minesGenerator = factory.GetMinesGenerator();
            this.drawer = factory.GetDrawer();
            this.userInput = factory.GetCommandProvider();
            this.scoreBoard = factory.GetScoreBoard();
            this.random = new Random();
        }

        // TODO: Add more Unit tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithAllNullArguments()
        {
            this.minesGenerator = null;
            this.drawer = null;
            this.userInput = null;
            this.scoreBoard = null;
            this.random = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithMinesGeneratorNull()
        {
            IMinesGenerator minesGenerator = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithDrawerNull()
        {
            IDrawer drawer = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithUserInputNull()
        {
            IUserInput userInput = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithScoreBoardNull()
        {
            IScoreBoard scoreBoard = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithRandomNull()
        {
            Random random = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
        "Line input cannot be null (empty continue command)")]
        public void TestPlayMinesMethodWithRegularValues()
        {
            var input = "";
            // Arrange
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(input))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
                }
            }
        }
    }
}
