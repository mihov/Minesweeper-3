using System;
using MinesweeperGame.Interfaces;

namespace MinesweeperGame
{
    /// <summary>
    /// Generator of mines.
    /// </summary>
    public class MinesGenerator : IMinesGenerator
    {
        public MinesGenerator()
        {
        }

        /// <summary>
        /// Deploys mines at random positions.
        /// </summary>
        /// <param name="rows">Number of rows of the field.</param>
        /// <param name="columns">Number of columns of the field.</param>
        /// <param name="mineCount">Number of mines to deploy.</param>
        /// <param name="randomMines">Random number generator to use.</param>
        /// <returns>Field of randomly positioned mines.</returns>
        public string[,] FillWithRandomMines(int rows, int columns, int mineCount, Random random)
        {
            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException("rows cannot be less than one");
            }

            if (columns < 1)
            {
                throw new ArgumentOutOfRangeException("columns cannot be less than one");
            }

            if (mineCount < 1)
            {
                throw new ArgumentOutOfRangeException("mines cannot be less than one");
            }

            if (random == null)
            {
                throw new ArgumentNullException("random cannot be null");
            }

            string[,] minesField = new string[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    minesField[i, j] = string.Empty;
                }
            }

            int minesCounter = 0;
            int randomRow;
            int randomCol;
            do
            {
                randomRow = random.Next(0, rows);
                randomCol = random.Next(0, columns);
                if (minesField[randomRow, randomCol] == string.Empty)
                {
                    minesField[randomRow, randomCol] += MediatorExtensions.MINES_SYMBOL;
                    minesCounter++;
                }
            }
            while (minesCounter < mineCount);

            return minesField;
        }
    }
}