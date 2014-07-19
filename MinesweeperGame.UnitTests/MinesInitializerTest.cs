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

    /// <summary>
    /// Used to test the <see cref="MinesInitializer"/> class.
    /// </summary>
    [TestClass]
    public class MinesInitializerTest
    {
        // TODO: Add more Unit tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithAllNullArguments()
        {
            var minesInit = MinesInitializer.Instance();
            IMinesGenerator minesGenerator = null;
            IDrawer drawer = null;
            IUserInput userInput = null;
            IScoreBoard scoreBoard = null;
            Random random = null;
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithMinesGeneratorNull()
        {
            var minesInit = MinesInitializer.Instance();
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = null;
            IDrawer drawer = factory.GetDrawer();
            IUserInput userInput = factory.GetCommandProvider();
            IScoreBoard scoreBoard = factory.GetScoreBoard();
            Random random = new Random();
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithDrawerNull()
        {
            var minesInit = MinesInitializer.Instance();
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            IDrawer drawer = null;
            IUserInput userInput = factory.GetCommandProvider();
            IScoreBoard scoreBoard = factory.GetScoreBoard();
            Random random = new Random();
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithUserInputNull()
        {
            var minesInit = MinesInitializer.Instance();
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            IDrawer drawer = factory.GetDrawer();
            IUserInput userInput = null;
            IScoreBoard scoreBoard = factory.GetScoreBoard();
            Random random = new Random();
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Null argument is not allowed")]
        public void TestPlayMinesMethodWithScoreBoardNull()
        {
            var minesInit = MinesInitializer.Instance();
            IMinesweeperFactory factory = new MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            IDrawer drawer = factory.GetDrawer();
            IUserInput userInput = factory.GetCommandProvider();
            IScoreBoard scoreBoard = null;
            Random random = new Random();
            minesInit.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }
    }
}
