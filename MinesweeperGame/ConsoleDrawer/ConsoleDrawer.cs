﻿using System;
using System.Collections.Generic;
using MinesweeperGame.Interfaces;

namespace MinesweeperGame
{
    /// <summary>
    /// Implements IDrawer for text console.
    /// </summary>
    public class ConsoleDrawer : IDrawer
    {
        /// <summary>
        /// Creates ConsoleDrawer instance.
        /// </summary>
        public ConsoleDrawer()
        { }

        /// <summary>
        /// Displays welcome message.
        /// </summary>
        /// <param name="message">Welcome contents.</param>
        public void ShowWelcome(string message)
        {
            Console.WriteLine(message + "\n");
        }

        /// <summary>
        /// Displays game end message.
        /// </summary>
        /// <param name="message">Game end contents.</param>
        public void ShowGameEnd(string message)
        {
            Console.WriteLine("\n" + message + "\n");
        }

        /// <summary>
        /// Draws the mines field to the UI.
        /// </summary>
        /// <param name="minesField">Mines field.</param>
        /// <param name="revealMines">If true, display the position of the mines, otherwise diplay
        /// the contents of the already visited fields only.</param>
        public void Draw(string[,] minesField, bool revealMines = false)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < minesField.GetLength(0); i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < minesField.GetLength(1); j++)
                {
                    if (!revealMines && ((minesField[i, j] == string.Empty) || (minesField[i, j] == "*")))
                    {
                        Console.Write(" ?");
                    }

                    if (!revealMines && (minesField[i, j] != string.Empty) && (minesField[i, j] != "*"))
                    {
                        Console.Write(" {0}", minesField[i, j]);
                    }

                    if (revealMines && (minesField[i, j] == string.Empty))
                    {
                        Console.Write(" -");
                    }

                    if (revealMines && (minesField[i, j] != string.Empty))
                    {
                        Console.Write(" {0}", minesField[i, j]);
                    }
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("   ---------------------");
        }

        /// <summary>
        /// Display general purpose message to the user.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays the score board.
        /// </summary>
        /// <param name="highScores">Scores to display.</param>
        public void PrintScoreBoard(IList<KeyValuePair<int, IList<string>>> highScores)
        {
            Console.WriteLine();
            if (highScores.Count == 0)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                Console.WriteLine("Scoreboard:");

                int index = 1;
                foreach (var score in highScores)
                {
                    foreach (string person in score.Value)
                    {
                        Console.WriteLine("{0}. {1} --> {2} cells", index, person, score.Key);
                    }

                    index++;
                }
            }

            Console.WriteLine();
        }
    }
}
