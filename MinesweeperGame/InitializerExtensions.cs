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
    
    // TODO change variables names
    // TODO provide comments
    public static class InitializerExtensions
    {
        internal static int NUMBER_OF_MINES = 15;
        internal static int MINES_FIELD_ROWS = 5;
        internal static int MINES_FIELD_COLS = 10;

        internal static bool CheckForGameEnd(string line)
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

        internal static bool IsMoveEntered(string line)
        {
            bool validMove = false;
            try
            {
                string[] inputParams = line.Split();
                int row = int.Parse(inputParams[0]);
                int col = int.Parse(inputParams[1]);
                validMove = true;
            }
            catch
            {
                validMove = false;
            }

            return validMove;
        }

        internal static void FillWithRandomMines(string[,] mines, Random randomMines)
        {
            int minesCounter = 0;
            while (minesCounter < NUMBER_OF_MINES)
            {
                int randomRow = randomMines.Next(0, 5);
                int randomCol = randomMines.Next(0, 10);
                if (mines[randomRow, randomCol] == "")
                {
                    mines[randomRow, randomCol] += "*";
                    minesCounter++;
                }
            }
        }

        internal static void Display(string[,] minesMatrix, bool boomed)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < minesMatrix.GetLength(0); i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < minesMatrix.GetLength(1); j++)
                {
                    if (!(boomed) && ((minesMatrix[i, j] == "") || (minesMatrix[i, j] == "*")))
                    {
                        Console.Write(" ?");
                    }
                    if (!(boomed) && (minesMatrix[i, j] != "") && (minesMatrix[i, j] != "*"))
                    {
                        Console.Write(" {0}", minesMatrix[i, j]);
                    }
                    if ((boomed) && (minesMatrix[i, j] == ""))
                    {
                        Console.Write(" -");
                    }
                    if ((boomed) && (minesMatrix[i, j] != ""))
                    {
                        Console.Write(" {0}", minesMatrix[i, j]);
                    }

                }
                Console.WriteLine("|");
            }

            Console.WriteLine("   ---------------------");
        }

        internal static bool HasExploded(string[,] matrix, int minesRow, int minesCol)
        {
            bool isKilled = false;
            int[] dRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int minesCounter = 0;
            if (matrix[minesRow, minesCol] == "*")
            {
                isKilled = true;
            }
            if ((matrix[minesRow, minesCol] != "") && (matrix[minesRow, minesCol] != "*"))
            {
                Console.WriteLine("Illegal Move!");
            }
            if (matrix[minesRow, minesCol] == "")
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    int newRow = dRow[direction] + minesRow;
                    int newCol = dCol[direction] + minesCol;
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

        internal static bool IsWinner(string[,] matrix, int minesCount)
        {
            bool isWinner = false;
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((matrix[i, j] != "") && (matrix[i, j] != "*"))
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

        internal static void StartGame(out string[,] mines, out int row,
            out int col, out bool isBoomed, out int minesCounter, out Random randomMines, out int revealedCellsCounter)
        {
            randomMines = new Random();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
            mines = new string[InitializerExtensions.MINES_FIELD_ROWS, InitializerExtensions.MINES_FIELD_COLS];

            for (int i = 0; i < mines.GetLength(0); i++)
            {
                for (int j = 0; j < mines.GetLength(1); j++)
                {
                    mines[i, j] = "";
                }
            }
        }

        internal static void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use   'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            Console.WriteLine(startMessage + "\n");
        }
    }
}
