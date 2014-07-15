using System;

namespace MinesweeperGame.Interfaces
{
    /// <summary>
    /// Generic interface for retrieval of user input.
    /// </summary>
    public interface IUserInput
    {
        /// <summary>
        /// Queries for command.
        /// </summary>
        /// <returns>User's command.</returns>
        string GetCommand();

        /// <summary>
        /// Queries for user's name.
        /// </summary>
        /// <returns>User's name.</returns>
        string GetUserName();
    }
}
