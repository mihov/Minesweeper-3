using System;
using System.Collections.Generic;

namespace MinesweeperGame.Interfaces
{
    /// <summary>
    /// Generic interface for score board manager.
    /// </summary>
    public interface IScoreBoard
    {
        /// <summary>
        /// Adds a player to the score list.
        /// </summary>
        /// <param name="playerName">The player name</param>
        /// <param name="playerScore">The player current score</param>
        void AddPlayer(string playerName, int playerScore);

        /// <summary>
        /// Gets the high score players as KeyValuePair collection where the key is score and the value is collection of players' names.
        /// </summary>
        /// <param name="count">Number of high scores to return.</param>
        /// <returns>Collection of the high score players.</returns>
        IList<KeyValuePair<int, IList<string>>> GetHighScores(int count);
    }
}
