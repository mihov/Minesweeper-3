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
        /// Stores the scores to a file.
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path.</param>
        /// <param name="scores">High scores.</param>
        void StorePlayers(string playerStoreDocumentPath, OrderedMultiDictionary<int, string> scores);

        /// <summary>
        /// Deletes the players in the DB file
        /// </summary>
        void EmptyFile(string playerStoreDocumentPath);
    }
}
