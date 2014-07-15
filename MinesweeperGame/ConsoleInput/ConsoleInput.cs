using System;
using MinesweeperGame.Interfaces;

namespace MinesweeperGame
{
    /// <summary>
    /// Implements IUserInput for text console.
    /// </summary>
    public class ConsoleInput : IUserInput
    {
        /// <summary>
        /// Queries for command.
        /// </summary>
        /// <returns>User's command.</returns>
        public string GetCommand()
        {
            Console.Write("Enter row and column: ");
            string line = Console.ReadLine();
            return line;
        }

        /// <summary>
        /// Queries for user's name.
        /// </summary>
        /// <returns>User's name.</returns>
        public string GetUserName()
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string playerName = Console.ReadLine();
            return playerName;
        }
    }
}
