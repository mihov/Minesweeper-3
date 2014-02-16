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
            MinesInitializer minesGame = new MinesInitializer();
            minesGame.PlayMines();
        }
    }
}
