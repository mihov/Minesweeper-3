using System;

namespace MinesweeperGame.Interfaces
{
    /// <summary>
    /// Generic interface for UI display.
    /// </summary>
    public interface IDrawer
    {
        /// <summary>
        /// Displays welcome message.
        /// </summary>
        /// <param name="message">Welcome contents.</param>
        void ShowWelcome(string message);

        /// <summary>
        /// Displays game end message.
        /// </summary>
        /// <param name="message">Game end contents.</param>
        void ShowGameEnd(string message);

        /// <summary>
        /// Draws the mines field to the UI.
        /// </summary>
        /// <param name="minesField">Mines field.</param>
        /// <param name="revealMines">If true, display the position of the mines, otherwise diplay
        /// the contents of the already visited fields only.</param>
        void Draw(string[,] minesField, bool revealMines = false);

        /// <summary>
        /// Display general purpose message to the user.
        /// </summary>
        /// <param name="message">Message to display.</param>
        void Message(string message);
    }
}
