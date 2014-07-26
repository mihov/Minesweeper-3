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
    /// Used to start the Minesweeper #3 Game
    /// </summary>
    public class MainDemo
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        public static void Main()
        {
            GameFacade minesweeperGame = GameFacade.Instance;
            var started = minesweeperGame.Run();
        }
    }
}