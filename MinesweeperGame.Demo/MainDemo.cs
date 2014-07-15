// ********************************
// <copyright file="MainDemo.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace MinesweeperGame.Demo
{
    using System;
    using MinesweeperGame;
    using MinesweeperGame.Interfaces;

    /// <summary>
    /// Used to start the Minesweeper 3 Game
    /// </summary>
    public class MainDemo
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        public static void Main()
        {

            IMinesweeperFactory factory = new MinesweeperFactory.MinesweeperFactory();
            IMinesGenerator minesGenerator = factory.GetMinesGenerator();
            IDrawer drawer = factory.GetDrawer();
            IUserInput userInput = factory.GetCommandProvider();
            IScoreBoard scoreBoard = factory.GetScoreBoard();
            Random random = new Random();

            MinesInitializer minesGame = MinesInitializer.Instance();
            minesGame.PlayMines(minesGenerator, drawer, userInput, scoreBoard, random);
        }
    }
}
