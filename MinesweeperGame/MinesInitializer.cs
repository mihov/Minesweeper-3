// ********************************
// <copyright file="MinesInitializer.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame
{
    using System;
    using System.Text;

    public class MinesInitializer
    {
        private ScoreBoard scoreBoard;

        private static void Display(string[,] matricaNaMinite, bool boomed)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < matricaNaMinite.GetLength(0); i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < matricaNaMinite.GetLength(1); j++)
                {
                    if (!(boomed) && ((matricaNaMinite[i, j] == "") || (matricaNaMinite[i, j] == "*")))
                    {
                        Console.Write(" ?");
                    }
                    if (!(boomed) && (matricaNaMinite[i, j] != "") && (matricaNaMinite[i, j] != "*"))
                    {
                        Console.Write(" {0}", matricaNaMinite[i, j]);
                    }
                    if ((boomed) && (matricaNaMinite[i, j] == ""))
                    {
                        Console.Write(" -");
                    }
                    if ((boomed) && (matricaNaMinite[i, j] != ""))
                    {
                        Console.Write(" {0}", matricaNaMinite[i, j]);
                    }

                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   ---------------------");
        }
        private static bool Boom(string[,] matrica, int minesRow, int minesCol)
        {
            bool isKilled = false;
            int[] dRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int minesCounter = 0;
            if (matrica[minesRow, minesCol] == "*")
            {
                isKilled = true;
            }
            if ((matrica[minesRow, minesCol] != "") && (matrica[minesRow, minesCol] != "*"))
            {
                Console.WriteLine("Illegal Move!");
            }
            if (matrica[minesRow, minesCol] == "")
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    int newRow = dRow[direction] + minesRow;
                    int newCol = dCol[direction] + minesCol;
                    if ((newRow >= 0) && (newRow < matrica.GetLength(0)) &&
                        (newCol >= 0) && (newCol < matrica.GetLength(1)) &&
                        (matrica[newRow, newCol] == "*"))
                    {
                        minesCounter++;
                    }
                }
                matrica[minesRow, minesCol] += Convert.ToString(minesCounter);
            }
            return isKilled;
        }
        private static bool PichLiSi(string[,] matrix, int cntMines)
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
            if (counter == matrix.Length - cntMines)
            {
                isWinner = true;
            }
            return isWinner;
        }
        private static void Zapochni(out string[,] mines, out int row,
            out int col, out bool isBoomed, out int minesCounter, out Random randomMines, out int revealedCellsCounter)
        {
            randomMines = new Random();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
            mines = new string[Extensions.MINES_FIELD_ROWS, Extensions.MINES_FIELD_COLS];

            for (int i = 0; i < mines.GetLength(0); i++)
            {
                for (int j = 0; j < mines.GetLength(1); j++)
                {
                    mines[i, j] = "";
                }
            }
        }

        private static void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use   'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            Console.WriteLine(startMessage + "\n");
        }

        private void StartPlayCycle()
        {
            Random randomMines;
            string[,] minichki;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;


            Zapochni(out minichki, out row, out col, out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);

            Extensions.FillWithRandomMines(minichki, randomMines);

            PrintInitialMessage();

            while (true)
            {
                Display(minichki, isBoomed);
                enterRowColInput(ref randomMines, ref minichki, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                 
            }
        }

        private void enterRowColInput(ref Random randomMines, ref string[,] minichki, ref int row, ref int col, ref int minesCounter, ref int revealedCellsCounter, ref bool isBoomed)
        {
            Console.Write("Enter row and column: ");
            string line = Console.ReadLine();
            line = line.Trim();

            if (Extensions.IsMoveEntered(line))
            {
                string[] inputParams = line.Split();
                row = int.Parse(inputParams[0]);
                col = int.Parse(inputParams[1]);

                if ((row >= 0) && (row < minichki.GetLength(0)) && (col >= 0) && (col < minichki.GetLength(1)))
                {
                    bool hasBoomedMine = Boom(minichki, row, col);
                    if (hasBoomedMine)
                    {
                        isBoomed = true;
                        Display(minichki, isBoomed);
                        Console.Write("\nBooom! You are killed by a mine! ");
                        Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);

                        Console.Write("Please enter your name for the top scoreboard: ");
                        string currentPlayerName = Console.ReadLine();
                        scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                        Console.WriteLine();
                        StartPlayCycle();
                    }
                    bool winner = PichLiSi(minichki, minesCounter);
                    if (winner)
                    {
                        Console.WriteLine("Congratulations! You are the WINNER!\n");

                        Console.Write("Please enter your name for the top scoreboard: ");
                        string currentPlayerName = Console.ReadLine();
                        scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                        Console.WriteLine();
                        StartPlayCycle();
                    }
                    revealedCellsCounter++;
                }
                else
                {
                    Console.WriteLine("Enter valid Row/Col!\n");
                }
            }
            else if (Extensions.proverka(line))
            {
                if (line == "top")
                {
                    scoreBoard.PrintScoreBoard();
                    enterRowColInput(ref randomMines, ref minichki, ref row, ref col, ref minesCounter, ref revealedCellsCounter, ref isBoomed);
                }
                else if (line == "exit")
                {
                    Console.WriteLine("\nGood bye!\n");
                    Environment.Exit(0);
                }
                else if (line == "restart")
                {
                    Console.WriteLine();
                    StartPlayCycle();
                }
                else
                {
                    // TODO exception can be catched here or in the aboce check
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                Console.WriteLine("Invalid Command!");
            }
        }

        public void PlayMines()
        {
            scoreBoard = new ScoreBoard();

            StartPlayCycle();            
        }
      
    }
}
