using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace MinesweeperGame.Interfaces
{
    /// <summary>
    /// Represents the Interface for the Game Data Saving Class
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Represents the interface method of getting the players from the storage file
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path</param>
        OrderedMultiDictionary<int, string> GetPlayers(string playerStoreDocumentPath);

        /// <summary>
        /// Represents the interface method of adding a player in the DB file
        /// </summary>
        /// <param name="documenPath">File path</param>
        /// <param name="name">Player name</param>
        /// <param name="points">Player points</param>
        void AddPlayer(string documenPath, string name, int points);
    }
}
