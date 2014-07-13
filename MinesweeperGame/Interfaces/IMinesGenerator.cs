using System;

namespace MinesweeperGame.Interfaces
{
    /// <summary>
    /// Generic interface for mines generators.
    /// </summary>
    public interface IMinesGenerator
    {
        /// <summary>
        /// Deploys mines at random positions.
        /// </summary>
        /// <param name="rows">Number of rows of the field.</param>
        /// <param name="columns">Number of columns of the field.</param>
        /// <param name="mineCount">Number of mines to deploy.</param>
        /// <param name="randomMines">Random number generator to use.</param>
        /// <returns>Field of randomly positioned mines.</returns>
        string[,] FillWithRandomMines(int rows, int columns, int mineCount, Random randomMines);
    }
}
