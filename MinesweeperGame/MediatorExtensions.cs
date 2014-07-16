// ********************************
// <copyright file="MediatorExtensions.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace MinesweeperGame
{
    using System;

    /// <summary>
    /// Represents the static methods, variables and constants
    /// Working behavior via mediator pattetn
    /// Support the work of <see cref="MinesInitializer"/> class
    /// </summary>
    internal static class MediatorExtensions
    {
        #region Constants

        /// <summary>
        /// Represents the mine symbol stored in matrix cells
        /// </summary>
        public const string MINES_SYMBOL = "*";

        /// <summary>
        /// Represents the number of mines in the game.
        /// </summary>
        public const int NUMBER_OF_MINES = 15;

        /// <summary>
        /// Represents the number of rows in the field.
        /// </summary>
        public const int MINES_FIELD_ROWS = 5;

        /// <summary>
        /// Represents the number of columns in the field.
        /// </summary>
        public const int MINES_FIELD_COLS = 10;

        #endregion Constants

        #region Public Methods

        /// <summary>
        /// Checks if command is valid
        /// <param name="command">Command to check.</param>
        /// <returns>True, if command is valid; False otherwise</returns>
        /// </summary>
        public static bool IsValidCommand(string command)
        {
            if (command.Equals("top") || command.Equals("restart") || command.Equals("exit"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to parse <paramref name="line"/> to <paramref name="row"/> and <paramref name="column"/>.
        /// </summary>
        /// <param name="line">String line to parse.</param>
        /// <param name="row">New row.</param>
        /// <param name="column">New column</param>
        /// <returns>True, if parsing was successful and <paramref name="row"/> and 
        /// <paramref name="column"/> are updated; False otherwise and ref parameters are unchanged.</returns>
        public static bool IsMoveEntered(string line, ref int row, ref int column)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            string[] inputParams = line.Split();
            if (inputParams.Length != 2)
            {
                return false;
            }

            int newRow;
            bool isRowParsed = int.TryParse(inputParams[0], out newRow);
            int newColumn;
            bool isColumnParsed = int.TryParse(inputParams[1], out newColumn);

            if (isRowParsed && isColumnParsed)
            {
                row = newRow;
                column = newColumn;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the current cell of field matrix is a mine bomb
        /// <param name="matrix">Game field matrix[row,col]</param>
        /// <param name="minesRow">Current row position</param>
        /// <param name="minesCol">Current column position</param>
        /// <returns>True, if the current cell is mine bomb, false if not</returns>
        /// </summary>
        public static bool HasExploded(string[,] matrix, int minesRow, int minesCol)
        {
            bool isKilled = false;
            int[] directionByRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] directionByCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int minesCounter = 0;
            if (matrix[minesRow, minesCol] == "*")
            {
                isKilled = true;
            }

            if (matrix[minesRow, minesCol] == string.Empty)
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    int newRow = directionByRow[direction] + minesRow;
                    int newCol = directionByCol[direction] + minesCol;
                    if ((newRow >= 0) && (newRow < matrix.GetLength(0)) &&
                        (newCol >= 0) && (newCol < matrix.GetLength(1)) &&
                        (matrix[newRow, newCol] == MINES_SYMBOL))
                    {
                        minesCounter++;
                    }
                }

                matrix[minesRow, minesCol] += Convert.ToString(minesCounter);
            }

            return isKilled;
        }

        /// <summary>
        /// Checks if the the mines in the game had been finished
        /// <param name="matrix">Game field matrix[row,col]</param>
        /// <param name="minesCount">Mines in the game</param>
        /// <returns>True, if all of the mines are opened, false if not</returns>
        /// </summary>
        public static bool IsWinner(string[,] matrix, int minesCount)
        {
            bool isWinner = false;
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((matrix[i, j] != string.Empty) && (matrix[i, j] != MINES_SYMBOL))
                    {
                        counter++;
                    }
                }
            }

            if (counter == matrix.Length - minesCount)
            {
                isWinner = true;
            }

            return isWinner;
        }

        #endregion Public Methods
    }
}