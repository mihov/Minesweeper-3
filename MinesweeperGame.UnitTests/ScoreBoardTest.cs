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
            var scoreBoardClass = new ScoreBoard();
            string name = null;
            int scores = 100;

            scoreBoardClass.AddPlayer(name, scores);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddValidPlayersNameAndNegativeScore()
        {
            var scoreBoardClass = new ScoreBoard();
            string name = "John";
            int scores = -1;

            scoreBoardClass.AddPlayer(name, scores);
        }

        [TestMethod]
        public void Add1PersonWithValidNameAndScore()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);

            var boardWithScores = scoreBoardClass.board;
            Assert.IsTrue(boardWithScores.Contains(10, "John"));
        }

        [TestMethod]
        public void Add2PeopleWithDifferentNamesAndSameScores()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("Pesho", 10);

            var boardWithScores = scoreBoardClass.board;
            Assert.IsTrue(boardWithScores.Keys.Count == 1);
            Assert.IsTrue(boardWithScores.Values.Count == 2);
        }

        [TestMethod]
        public void Add2PeopleWithSameNamesAndSameScores()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("John", 10);

            var boardWithScores = scoreBoardClass.board;
            Assert.IsTrue(boardWithScores.Keys.Count == 1);
            Assert.IsTrue(boardWithScores.Values.Count == 2);
        }

        [TestMethod]
        public void Add2PeopleWithSameNamesAndDifferentScores()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("John", 20);

            var boardWithScores = scoreBoardClass.board;
            Assert.IsTrue(boardWithScores.Keys.Count == 2);
            Assert.IsTrue(boardWithScores.Values.Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetHighScoresWithZeroCount()
        {
            var scoreBoardClass = new ScoreBoard();
            int count = 0;
            scoreBoardClass.GetHighScores(count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetHighScoresWithNegativeCount()
        {
            var scoreBoardClass = new ScoreBoard();
            int count = -1;
            scoreBoardClass.GetHighScores(count);
        }

        [TestMethod]
        public void GetHighScoresWith1Person()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 20);

            var hightScores = scoreBoardClass.GetHighScores(scoreBoardClass.board.Count);

            Assert.IsTrue(hightScores[0].Key == 20);
            Assert.IsTrue(hightScores[0].Value[0] == "John");
        }

        [TestMethod]
        public void GetHighScoresWith3PeopleWithDifferentNames()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("Pesho", 20);
            scoreBoardClass.AddPlayer("Gosho", 15);

            var hightScores = scoreBoardClass.GetHighScores(scoreBoardClass.board.Count);

            Assert.IsTrue(hightScores[0].Key == 20);
            Assert.IsTrue(hightScores[0].Value[0] == "Pesho");
            Assert.IsTrue(hightScores[1].Key == 15);
            Assert.IsTrue(hightScores[1].Value[0] == "Gosho");
            Assert.IsTrue(hightScores[2].Key == 10);
            Assert.IsTrue(hightScores[2].Value[0] == "John");
        }

        [TestMethod]
        public void GetHighScoresWith3PeopleWithSameNames()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("John", 20);
            scoreBoardClass.AddPlayer("John", 15);

            var hightScores = scoreBoardClass.GetHighScores(scoreBoardClass.board.Count);

            Assert.IsTrue(hightScores[0].Key == 20);
            Assert.IsTrue(hightScores[0].Value[0] == "John");
            Assert.IsTrue(hightScores[1].Key == 15);
            Assert.IsTrue(hightScores[1].Value[0] == "John");
            Assert.IsTrue(hightScores[2].Key == 10);
            Assert.IsTrue(hightScores[2].Value[0] == "John");
        }

        [TestMethod]
        public void GetHighScoresWith3PeopleWithSameScores()
        {
            var scoreBoardClass = new ScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("Gosho", 10);
            scoreBoardClass.AddPlayer("Pesho", 10);

            var hightScores = scoreBoardClass.GetHighScores(scoreBoardClass.board.Count);

            Assert.IsTrue(hightScores[0].Key == 10);
            Assert.IsTrue(hightScores[0].Value[0] == "Gosho");
            Assert.IsTrue(hightScores[0].Value[1] == "John");
            Assert.IsTrue(hightScores[0].Value[2] == "Pesho");
        }
    }
}
