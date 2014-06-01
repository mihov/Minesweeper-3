// ********************************
// <copyright file="Extensions.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperGame
{
    public static class Extensions
    {
        internal static int NUMBER_OF_MINES = 15;
        internal static int MINES_FIELD_ROWS = 5;
        internal static int MINES_FIELD_COLS = 10;

        internal static bool proverka(string line)
        {
            if (line.Equals("top") || line.Equals("restart") || line.Equals("exit"))
            {
                return true;
            }
            else
                return false;
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
    }
}
