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
        /// Returned by command handlers.
        /// </summary>
        private enum CommandResult {
            /// <summary>
            /// Continue game.
            /// </summary>
            ContinueGame,

            /// <summary>
            /// Start new game.
            /// </summary>
            RestartGame,

            /// <summary>
            /// End application.
            /// </summary>
            EndApplication
        }

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
        {
        }

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

            CommandResult commandResult;
            do
            {
                commandResult = this.StartPlayCycle();
            } while (commandResult != CommandResult.EndApplication);
        }

        /// <summary>
        /// Start current game playing cycle
        /// </summary>
        /// <returns>Result of the command processing.</returns>
        private CommandResult StartPlayCycle()
        {
            string[,] mines;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;

            mines = this.minesGenerator.FillWithRandomMines(MediatorExtensions.MINES_FIELD_ROWS,
                MediatorExtensions.MINES_FIELD_COLS, MediatorExtensions.NUMBER_OF_MINES, this.random);
            this.PrintInitialMessage();

            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            CommandResult commandResult;

            do
            {
                this.drawer.Draw(mines);
                commandResult = this.ProcessCommands(ref mines, ref row, ref col, ref minesCounter, ref revealedCellsCounter);
            } while (commandResult == CommandResult.ContinueGame);

            return commandResult;
        }

        /// <summary>
        /// Called by <see cref="StartPlayCycle"/> method
        /// Gets and processes the user input.
        /// </summary>
        /// <returns>Result of the command processing.</returns>
        private CommandResult ProcessCommands(ref string[,] mines, ref int row, ref int col, ref int minesCounter, ref int revealedCellsCounter)
        {
            string line = this.userInput.GetCommand();
            line = line.Trim();
            CommandResult commandResult;

            if (MediatorExtensions.IsMoveEntered(line, ref row, ref col))
            {
                commandResult = MoveTo(mines, row, col, minesCounter, ref revealedCellsCounter);
            }
            else if (MediatorExtensions.IsValidCommand(line))
            {
                if (line == "top")
                {
                    IList<KeyValuePair<int, IList<string>>> highScores = this.scoreBoard.GetHighScores(MediatorExtensions.NUMBER_OF_SHOWED_SCORES);
                    this.drawer.PrintScoreBoard(highScores);
                    commandResult = CommandResult.ContinueGame;
                }
                else if (line == "exit")
                {
                    this.drawer.ShowGameEnd("\nGood bye!\n");
                    commandResult = CommandResult.EndApplication;
                }
                else if (line == "restart")
                {
                    commandResult = CommandResult.RestartGame;
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
                commandResult = CommandResult.ContinueGame;
            }

            return commandResult;
        }

        /// <summary>
        /// Tries to move to new position.
        /// </summary>
        /// <param name="mines">Mines field</param>
        /// <param name="row">New row</param>
        /// <param name="col">New column</param>
        /// <param name="minesCounter">Mines total count</param>
        /// <param name="revealedCellsCounter">Revealed mines so far</param>
        /// <returns>Result of the command processing.</returns>
        private CommandResult MoveTo(string[,] mines, int row, int col, int minesCounter, ref int revealedCellsCounter)
        {
            if ((row >= 0) && (row < mines.GetLength(0)) && (col >= 0) && (col < mines.GetLength(1)))
            {
                if ((mines[row, col] != string.Empty) && (mines[row, col] != "*"))
                {
                    this.drawer.Message("Illegal Move!");
                    return CommandResult.ContinueGame;
                }

                bool hasBoomedMine = MediatorExtensions.HasExploded(mines, row, col);
                if (hasBoomedMine)
                {
                    this.drawer.Draw(mines, hasBoomedMine);
                    this.drawer.Message("\nBoom! You are killed by a mine! ");
                    this.drawer.Message(string.Format("You revealed {0} cells without mines.", revealedCellsCounter));

                    string currentPlayerName = this.userInput.GetUserName();

                    // not alowing the user to skip entering his/her name
                    while (string.IsNullOrWhiteSpace(currentPlayerName))
                    {
                        currentPlayerName = this.userInput.GetUserName();
                    }
                    this.scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                    return CommandResult.RestartGame;
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

                    return CommandResult.RestartGame;
                }

                revealedCellsCounter++;
            }
            else
            {
                this.drawer.Message("Enter valid Row/Col!\n");
            }

            return CommandResult.ContinueGame;
        }

        private void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use   'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            this.drawer.ShowWelcome(startMessage);
        }
    }
}
