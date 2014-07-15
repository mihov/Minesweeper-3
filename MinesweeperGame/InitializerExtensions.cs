// ********************************
// <copyright file="InitializerExtensions.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

namespace MinesweeperGame
{
    using System;

    /// <summary>
    /// Represents the static methods, variables and constants which support the work of <see cref="MinesInitializer"/> class
    /// </summary>
    public static class InitializerExtensions
    {
        // TODO change variables names
        // TODO provide comments
        private const int NumberOFMines = 15;
        private const int MinesFieldRows = 5;
        private const int MinesFieldCols = 10;

        public static bool CheckForGameEnd(string line)
        {
            if (line.Equals("top") || line.Equals("restart") || line.Equals("exit"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsMoveEntered(string line)
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

            int row;
            bool isRowParsed = int.TryParse(inputParams[0], out row);
            int col;
            bool isColParsed = int.TryParse(inputParams[1], out col);
            if (isRowParsed && row >= 0 &&
                isColParsed && col >= 0)
            {
                return true;
            }

            return false;
        }

        public static void FillWithRandomMines(string[,] mines, Random randomMines)
        {
            int minesCounter = 0;
            while (minesCounter < NumberOFMines)
            {
                int randomRow = randomMines.Next(0, 5);
                int randomCol = randomMines.Next(0, 10);
                if (mines[randomRow, randomCol] == string.Empty)
                {
                    mines[randomRow, randomCol] += "*";
                    minesCounter++;
                }
            }
        }

        public static void Display(string[,] minesMatrix, bool boomed)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < minesMatrix.GetLength(0); i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < minesMatrix.GetLength(1); j++)
                {
                    if (!boomed && ((minesMatrix[i, j] == string.Empty) || (minesMatrix[i, j] == "*")))
                    {
                        Console.Write(" ?");
                    }

                    if (!boomed && (minesMatrix[i, j] != string.Empty) && (minesMatrix[i, j] != "*"))
                    {
                        Console.Write(" {0}", minesMatrix[i, j]);
                    }

                    if (boomed && (minesMatrix[i, j] == string.Empty))
                    {
                        Console.Write(" -");
                    }

                    if (boomed && (minesMatrix[i, j] != string.Empty))
                    {
                        Console.Write(" {0}", minesMatrix[i, j]);
                    }
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("   ---------------------");
        }

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

            if ((matrix[minesRow, minesCol] != string.Empty) && (matrix[minesRow, minesCol] != "*"))
            {
                Console.WriteLine("Illegal Move!");
            }

            if (matrix[minesRow, minesCol] == string.Empty)
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    int newRow = directionByRow[direction] + minesRow;
                    int newCol = directionByCol[direction] + minesCol;
                    if ((newRow >= 0) && (newRow < matrix.GetLength(0)) &&
                        (newCol >= 0) && (newCol < matrix.GetLength(1)) &&
                        (matrix[newRow, newCol] == "*"))
                    {
                        minesCounter++;
                    }
                }

                matrix[minesRow, minesCol] += Convert.ToString(minesCounter);
            }

            return isKilled;
        }

        public static bool IsWinner(string[,] matrix, int minesCount)
        {
            bool isWinner = false;
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((matrix[i, j] != string.Empty) && (matrix[i, j] != "*"))
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

        public static void StartGame(out string[,] mines, out int row, out int col, out bool isBoomed, out int minesCounter, out Random randomMines, out int revealedCellsCounter)
        {
            randomMines = new Random();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
            mines = new string[InitializerExtensions.MinesFieldRows, InitializerExtensions.MinesFieldCols];

            for (int i = 0; i < mines.GetLength(0); i++)
            {
                for (int j = 0; j < mines.GetLength(1); j++)
                {
                    mines[i, j] = string.Empty;
                }
            }
        }

    }
}
