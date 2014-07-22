// ********************************
// <copyright file="GameFacade.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace MinesweeperGame
{
    using System;
    using MinesweeperGame.Interfaces;

    /// <summary>
    /// Facade class hiding game initialization and starting.
    /// </summary>
    public class GameFacade
    {
        /// <summary>
        /// The only instance of the class.
        /// </summary>
        private static volatile GameFacade instance;

        /// <summary>
        /// Object to ensure only one instance is created and only when the instance is needed.
        /// </summary>
        private static object syncLock = new object();

        private readonly IMinesweeperFactory factory = new MinesweeperFactory();
        private readonly Random random = new Random();
        private readonly IMinesGenerator minesGenerator;
        private readonly IDrawer drawer;
        private readonly IUserInput userInput;
        private readonly IScoreBoard scoreBoard;

        /// <summary>
        /// Only internal instance creation of the class. 
        /// Call all initializers necessary for the game starting.
        /// </summary>
        private GameFacade()
        {
            this.minesGenerator = this.factory.GetMinesGenerator();
            this.drawer = this.factory.GetDrawer();
            this.userInput = this.factory.GetCommandProvider();
            this.scoreBoard = this.factory.GetScoreBoard();
        }

        /// <summary>
        /// Creates and returns the only one instance of the class.
        /// </summary>
        /// <returns>Class single instance.</returns>
        /// <remarks>Uses lazy loading.</remarks>
        public static GameFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new GameFacade();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Run the game.
        /// </summary>
        public void Run()
        {
            MinesInitializer minesGame = MinesInitializer.Instance;
            minesGame.PlayMines(this.minesGenerator, this.drawer, this.userInput, this.scoreBoard, this.random);
        }
    }
}