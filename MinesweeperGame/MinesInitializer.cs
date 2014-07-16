// ********************************
// <copyright file="MinesInitializer.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame
{
    using System;
    using System.Collections.Generic;
    using MinesweeperGame.Interfaces;

    /// <summary>
    /// Represents the main initializing class of the game
    /// </summary>
    public class MinesInitializer
    {
        /// <summary>
        /// The only instance of the class.
        /// </summary>
        private static volatile MinesInitializer onlyInstance;

        /// <summary>
        /// Object to ensure only one instance is created and only when the instance is needed.
        /// </summary>
        private static object syncLock = new object();

        private IMinesGenerator minesGenerator;

        /// <summary>
        /// Represents a Results list instance
        /// </summary>
        private IScoreBoard scoreBoard;

        /// <summary>
        /// IDrawer to use.
        /// </summary>
        private IDrawer drawer;

        /// <summary>
        /// IUserInput to use.
        /// </summary>
        private IUserInput userInput;

        private Random random;

        /// <summary>
        /// Disable external creation of the class.
        /// </summary>
        private MinesInitializer()
        { }

        /// <summary>
        /// Creates and returns the only instance of the class.
        /// </summary>
        /// <returns>Class single instance.</returns>
        /// <remarks>Uses lazy loading.</remarks>
        public static MinesInitializer Instance()
        {
            if (onlyInstance == null)
            {
                lock (syncLock)
                {
                    if (onlyInstance == null)
                    {
                        onlyInstance = new MinesInitializer();
                    }
                }
            }

            return onlyInstance;
        }

        /// <summary>
        /// Starting a game play public method
        /// Initializes new instance of score list
        /// </summary>
        public void PlayMines(IMinesGenerator minesGenerator, IDrawer drawer, IUserInput userInput,
            IScoreBoard scoreBoard, Random random)
        {
            if (minesGenerator == null)
            {
                throw new ArgumentNullException("minesGenerator");
            }

            if (drawer == null)
            {
                throw new ArgumentNullException("drawer");
            }

            if (userInput == null)
            {
                throw new ArgumentNullException("userInput");
            }

            if (scoreBoard == null)
            {
                throw new ArgumentNullException("scoreBoard");
            }

            if (random == null)
            {
                throw new ArgumentNullException("random");
            }

            this.minesGenerator = minesGenerator;
            this.drawer = drawer;
            this.userInput = userInput;
            this.random = random;
            this.scoreBoard = scoreBoard;

            this.StartPlayCycle();
        }

        /// <summary>
        /// Start current game playing cycle
        /// </summary>
        private void StartPlayCycle()
        {
            string[,] mines;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;

            mines = this.minesGenerator.FillWithRandomMines(MediatorExtensions.MINES_FIELD_ROWS,
                MediatorExtensions.MINES_FIELD_COLS, MediatorExtensions.NUMBER_OF_MINES, this.random);
            this.PrintInitialMessage();

            isBoomed = false;
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            while (true)
            {
                this.drawer.Draw(mines, isBoomed);
                this.EnterRowColInput(ref this.random, ref mines, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
            }
        }

        /// <summary>
        /// Called by <see cref="StartPlayCycle"/> method
        /// Checks the current field cell, valid or invalid, bomb or empty field
        /// </summary>
        private void EnterRowColInput(ref Random randomMines, ref string[,] mines, ref int row, ref int col, ref int minesCounter, ref int revealedCellsCounter, ref bool isBoomed)
        {
            string line = this.userInput.GetCommand();
            line = line.Trim();

            if (MediatorExtensions.IsMoveEntered(line, ref row, ref col))
            {
                if ((row >= 0) && (row < mines.GetLength(0)) && (col >= 0) && (col < mines.GetLength(1)))
                {
                    bool hasBoomedMine = MediatorExtensions.HasExploded(mines, row, col);
                    if (hasBoomedMine)
                    {
                        isBoomed = true;
                        this.drawer.Draw(mines, isBoomed);
                        this.drawer.Message("\nBoom! You are killed by a mine! ");
                        this.drawer.Message(string.Format("You revealed {0} cells without mines.", revealedCellsCounter));

                        string currentPlayerName = this.userInput.GetUserName();
                        if (!string.IsNullOrWhiteSpace(currentPlayerName))
                        {
                            this.scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);
                        }

                        this.StartPlayCycle();
                    }

                    bool winner = MediatorExtensions.IsWinner(mines, minesCounter);
                    if (winner)
                    {
                        this.drawer.Message("Congratulations! You are the WINNER!\n");
                        string currentPlayerName = this.userInput.GetUserName();
                        if (!string.IsNullOrWhiteSpace(currentPlayerName))
                        {
                            this.scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);
                        }

                        this.StartPlayCycle();
                    }

                    revealedCellsCounter++;
                }
                else
                {
                    this.drawer.Message("Enter valid Row/Col!\n");
                }
            }
            else if (MediatorExtensions.CheckForGameEnd(line))
            {
                if (line == "top")
                {
                    // TODO: use constant
                    IList<KeyValuePair<int, IList<string>>> highScores = this.scoreBoard.GetHighScores(5);
                    this.drawer.PrintScoreBoard(highScores);
                    this.EnterRowColInput(ref randomMines, ref mines, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                }
                else if (line == "exit")
                {
                    this.drawer.ShowGameEnd("\nGood bye!\n");
                    Environment.Exit(0);
                }
                else if (line == "restart")
                {
                    this.StartPlayCycle();
                }
                else
                {
                    // TODO exception can be catched here or in the above check
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                this.drawer.Message("Invalid Command!");
            }
        }

        private void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use   'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            this.drawer.ShowWelcome(startMessage);
        }
    }
}
