// ********************************
// <copyright file="ScoreBoard.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    /// <summary>
    /// Represents the results history of the current set of games
    /// </summary>
    public class ScoreBoard
    {
        #region Private Fields

        /// <summary>
        /// The main container of the results rank list
        /// </summary>
        /// <param type="int">Score</param>
        /// <param type="string">Player name</param>
        private OrderedMultiDictionary<int, string> scoreBoard;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// Initializes a new instance of the <see cref="scoreBoard"/> OrderedMultiDictionary.
        /// </summary>
        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Adds a player to the score list.
        /// </summary>
        /// <param name="playerName">The player name</param>
        /// <param name="playerScore">The player current score</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="playerName"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="playerScore"/> less than zerol.</exception>
        public void AddPlayer(string playerName, int playerScore)
        {
            if (playerName == null)
            {
                throw new ArgumentNullException("playerName", "The player name cannot be null.");
            }
            if (playerScore < 0)
            {
                throw new ArgumentException("playerScore", "The player score cannot ness than zero.");
            }

            if (!scoreBoard.ContainsKey(playerScore))
            {
                scoreBoard.Add(playerScore, playerName);
            }
            else
            {
                scoreBoard[playerScore].Add(playerName);
            }
        }

        /// <summary>
        /// Prints the score to the console
        /// </summary>
        public void PrintScoreBoard()
        {
            bool FirstFive = false;
            int currentCounter = 1;

            Console.WriteLine();
            if (this.scoreBoard.Values.Count == 0)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                Console.WriteLine("Scoreboard:");

                foreach (int key in this.scoreBoard.Keys.OrderByDescending(obj => obj))
                {
                    foreach (string person in this.scoreBoard[key])
                    {
                        if (currentCounter < 6)

                        {
                            Console.WriteLine("{0}. {1} --> {2} cells", currentCounter, person, key);
                            currentCounter++;
                        }
                        else
                        {
                            FirstFive = true;
                            break;
                        }
                    }
                    if (FirstFive)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
        }

        #endregion Public Methods
    }
}
