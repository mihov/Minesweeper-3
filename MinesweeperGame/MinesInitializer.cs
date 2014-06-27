// ********************************
// <copyright file="MinesInitializer.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents the main initializing class of the game
    /// </summary>
    public class MinesInitializer
    {
        #region Private Fields
        /// <summary>
        /// Represents a Results list instance
        /// </summary>
        private ScoreBoard scoreBoard;

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        /// Start current game playing cycle
        /// </summary>
        private void StartPlayCycle()
        {
            Random randomMines;
            string[,] mines;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;


            InitializerExtensions.StartGame(out mines, out row, out col, out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);

            InitializerExtensions.FillWithRandomMines(mines, randomMines);

            InitializerExtensions.PrintInitialMessage();

            while (true)
            {
                InitializerExtensions.Display(mines, isBoomed);
                enterRowColInput(ref randomMines, ref mines, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);              
            }
        }

        /// <summary>
        /// Called by <see cref="StartPlayCycle"/> method
        /// Checks the current field cell, valud invalid, bomb or empty field
        /// </summary>
        private void enterRowColInput(ref Random randomMines, ref string[,] mines, ref int row, ref int col, ref int minesCounter, ref int revealedCellsCounter, ref bool isBoomed)
        {
            Console.Write("Enter row and column: ");
            string line = Console.ReadLine();
            line = line.Trim();

            if (InitializerExtensions.IsMoveEntered(line))
            {
                string[] inputParams = line.Split();
                row = int.Parse(inputParams[0]);
                col = int.Parse(inputParams[1]);

                if ((row >= 0) && (row < mines.GetLength(0)) && (col >= 0) && (col < mines.GetLength(1)))
                {
                    bool hasBoomedMine = InitializerExtensions.HasExploded(mines, row, col);
                    if (hasBoomedMine)
                    {
                        isBoomed = true;
                        InitializerExtensions.Display(mines, isBoomed);
                        Console.Write("\nBooom! You are killed by a mine! ");
                        Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);

                        Console.Write("Please enter your name for the top scoreboard: ");
                        string currentPlayerName = Console.ReadLine();
                        scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                        Console.WriteLine();
                        StartPlayCycle();
                    }
                    bool winner = InitializerExtensions.IsWinner(mines, minesCounter);
                    if (winner)
                    {
                        Console.WriteLine("Congratulations! You are the WINNER!\n");

                        Console.Write("Please enter your name for the top scoreboard: ");
                        string currentPlayerName = Console.ReadLine();
                        scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                        Console.WriteLine();
                        StartPlayCycle();
                    }
                    revealedCellsCounter++;
                }
                else
                {
                    Console.WriteLine("Enter valid Row/Col!\n");
                }
            }
            else if (InitializerExtensions.CheckForGameEnd(line))
            {
                if (line == "top")
                {
                    scoreBoard.PrintScoreBoard();
                    enterRowColInput(ref randomMines, ref mines, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                }
                else if (line == "exit")
                {
                    Console.WriteLine("\nGood bye!\n");
                    Environment.Exit(0);
                }
                else if (line == "restart")
                {
                    Console.WriteLine();
                    StartPlayCycle();
                }
                else
                {
                    // TODO exception can be catched here or in the above check
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                Console.WriteLine("Invalid Command!");
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Starting a game play public method
        /// Initializes new instance of score list
        /// </summary>
        public void PlayMines()
        {
            scoreBoard = new ScoreBoard();
            StartPlayCycle();
        }

        #endregion Public Methods

    }
}
