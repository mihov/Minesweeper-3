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
    using System.Collections.Generic;
    using MinesweeperGame.Interfaces;
    using Wintellect.PowerCollections;

    /// <summary>
    /// Represents the results history of the current set of games.
    /// </summary>
    public class ScoreBoard : IScoreBoard
    {
        /// <summary>
        /// The main container of the results rank list
        /// </summary>
        /// <param type="int">Score</param>
        /// <param type="string">Player name</param>
        private OrderedMultiDictionary<int, string> scoreBoard;

        /// <summary>
        /// The main data persister of the game
        /// </summary>
        private Repository dataRepository = new Repository();

        public OrderedMultiDictionary<int, string> board
        {
            get
            {
                return this.scoreBoard;
            }
            private set { }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// Initializes a new instance of the <see cref="scoreBoard"/> OrderedMultiDictionary.
        /// </summary>
        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        /// <summary>
        /// Adds a player to the score list.
        /// </summary>
        /// <param name="playerName">The player name</param>
        /// <param name="playerScore">The player current score</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="playerName"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="playerScore"/> less than zero.</exception>
        public void AddPlayer(string playerName, int playerScore)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentNullException("playerName", "The player name cannot be null or empty.");
            }

            if (playerScore < 0)
            {
                throw new ArgumentOutOfRangeException("playerScore", "The player score cannot be less than zero.");
            }

            dataRepository.AddPlayer(MediatorExtensions.MAIN_DATAFILE_PATH, playerName, playerScore);
        }

        /// <summary>
        /// Gets the high score players as KeyValuePair collection where the key is score and the value is collection of players' names.
        /// </summary>
        /// <param name="count">Number of high scores to return.</param>
        /// <returns>Collection of the high score players.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <paramref name="count"/> less than zero.</exception>
        public IList<KeyValuePair<int, IList<string>>> GetHighScores(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count cannot be negative or zero");
            }
            this.scoreBoard = dataRepository.GetPlayers(MediatorExtensions.MAIN_DATAFILE_PATH);

            var highScores = this.scoreBoard.Keys.OrderByDescending(a => a).Take(count);
            IList<KeyValuePair<int, IList<string>>> result = new List<KeyValuePair<int, IList<string>>>();
            foreach (var score in highScores)
            {
                result.Add(new KeyValuePair<int, IList<string>>(score, new List<string>()));
                foreach (var player in this.scoreBoard[score])
                {
                    result[result.Count - 1].Value.Add(player);
                }
            }

            return result;
        }

        public void FullDeleteList() {
            dataRepository.EmptyFile(MediatorExtensions.MAIN_DATAFILE_PATH);
        }
    }
}