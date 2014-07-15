// ********************************
// <copyright file="MinesInitializer.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame
{
    using System;
    using MinesweeperGame.MineGenerator;
    using MinesweeperGame.ScoresBoard;
    using MinesweeperGame.Interfaces;
    using MinesweeperGame.Demo.ConsoleDrawer;
    using MinesweeperGame.Demo.ConsoleInput;

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
        private ScoreBoard scoreBoard;

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

            // TODO: use the parameter.
            this.scoreBoard = new ScoreBoard();
            this.StartPlayCycle();
        }

        /// <summary>
        /// Start current game playing cycle
        /// </summary>
        private void StartPlayCycle()
        {
            //Random randomMines;
            string[,] mines;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;

            //InitializerExtensions.StartGame(out mines, out row, out col, out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);
            
            //InitializerExtensions.FillWithRandomMines(mines, randomMines);
            // TODO: use constants and move to factory.
            //randomMines = new Random();
            mines = minesGenerator.FillWithRandomMines(5, 10, 15, this.random);

            //PrintInitialMessage();
            // TODO: move to factory.
            drawer = new ConsoleDrawer();
            this.PrintInitialMessage();

            // TODO: move to factory.
            this.userInput = new ConsoleInput();

            isBoomed = false;
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            while (true)
            {
                //InitializerExtensions.Display(mines, isBoomed);
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
            //Console.Write("Enter row and column: ");
            //string line = Console.ReadLine();
            string line = this.userInput.GetCommand();
            line = line.Trim();

            if (InitializerExtensions.IsMoveEntered(line, ref row, ref col))
            {
                //string[] inputParams = line.Split();
                //row = int.Parse(inputParams[0]);
                //col = int.Parse(inputParams[1]);

                if ((row >= 0) && (row < mines.GetLength(0)) && (col >= 0) && (col < mines.GetLength(1)))
                {
                    bool hasBoomedMine = InitializerExtensions.HasExploded(mines, row, col);
                    if (hasBoomedMine)
                    {
                        isBoomed = true;
                        //InitializerExtensions.Display(mines, isBoomed);
                        this.drawer.Draw(mines, isBoomed);
                        //Console.Write("\nBoom! You are killed by a mine! ");
                        //Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);
                        this.drawer.Message("\nBoom! You are killed by a mine! ");
                        this.drawer.Message(string.Format("You revealed {0} cells without mines.", revealedCellsCounter));

                        //Console.Write("Please enter your name for the top scoreboard: ");
                        //string currentPlayerName = Console.ReadLine();
                        string currentPlayerName = this.userInput.GetUserName();
                        this.scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                        //Console.WriteLine();
                        this.StartPlayCycle();
                    }

                    bool winner = InitializerExtensions.IsWinner(mines, minesCounter);
                    if (winner)
                    {
                        //Console.WriteLine("Congratulations! You are the WINNER!\n");
                        this.drawer.Message("Congratulations! You are the WINNER!\n");

                        //Console.Write("Please enter your name for the top scoreboard: ");
                        //string currentPlayerName = Console.ReadLine();
                        string currentPlayerName = this.userInput.GetUserName();
                        this.scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                        //Console.WriteLine();
                        this.StartPlayCycle();
                    }

                    revealedCellsCounter++;
                }
                else
                {
                    //Console.WriteLine("Enter valid Row/Col!\n");
                    this.drawer.Message("Enter valid Row/Col!\n");
                }
            }
            else if (InitializerExtensions.CheckForGameEnd(line))
            {
                if (line == "top")
                {
                    this.scoreBoard.PrintScoreBoard();
                    this.EnterRowColInput(ref randomMines, ref mines, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                }
                else if (line == "exit")
                {
                    //Console.WriteLine("\nGood bye!\n");
                    this.drawer.ShowGameEnd("\nGood bye!\n");
                    Environment.Exit(0);
                }
                else if (line == "restart")
                {
                    //Console.WriteLine();
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
                //Console.WriteLine("Invalid Command!");
                this.drawer.Message("Invalid Command!");
            }
        }

        private void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use   'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            //Console.WriteLine(startMessage + "\n");
            this.drawer.ShowWelcome(startMessage);
        }
    }
}
