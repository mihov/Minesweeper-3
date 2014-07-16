// ********************************
// <copyright file="MainDemo.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MinesweeperGame;

    /// <summary>
    /// Used to test the <see cref="ScoreBoard"/> class.
    /// </summary>
    [TestClass]
    public class ScoreBoardTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullPlayersNameAndValidScore()
        {
            var scoreBoard = new ScoreBoard();
            string name = null;
            int scores = 100;

            scoreBoard.AddPlayer(name, scores);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddValidPlayersNameAndNegativeScore()
        {
            var scoreBoard = new ScoreBoard();
            string name = "John";
            int scores = -1;

            scoreBoard.AddPlayer(name, scores);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetHighScoresWithZeroCount()
        {
            var scoreBoard = new ScoreBoard();
            int count = 0;
            scoreBoard.GetHighScores(count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetHighScoresWithNegativeCount()
        {
            var scoreBoard = new ScoreBoard();
            int count = -1;
            scoreBoard.GetHighScores(count);
        }
    }
}
