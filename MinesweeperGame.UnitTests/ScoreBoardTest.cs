// ********************************
// <copyright file="MainDemo.cs" company="Telerik Academy">
// Copyright (©) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace MinesweeperGame.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MinesweeperGame;

    /// <summary>
    /// Used to test the <see cref="ScoreBoard"/> class.
    /// </summary>
    [TestClass]
    public class ScoreBoardTest
    {
        private const string FILE_PATH = @"..\..\players.xml";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullPlayersNameAndValidScore()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            string name = null;
            int scores = 100;

            scoreBoardClass.AddPlayer(name, scores);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddValidPlayersNameAndNegativeScore()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            string name = "John";
            int scores = -1;

            scoreBoardClass.AddPlayer(name, scores);
        }

        [TestMethod]
        public void Add1PersonWithValidNameAndScore()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            scoreBoardClass.AddPlayer("John", 10);

            var scores = scoreBoardClass.GetHighScores(1);
            bool result = (scores.Count == 1 ) && (scores[0].Key == 10) && (scores[0].Value[0] == "John");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add2PeopleWithDifferentNamesAndSameScores()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("Pesho", 10);

            var boardWithScores = scoreBoardClass.GetHighScores(1);
            Assert.IsTrue(boardWithScores.Count == 1);
            Assert.IsTrue(boardWithScores[0].Value.Count == 2);
        }

        [TestMethod]
        public void Add2PeopleWithSameNamesAndSameScores()
        {
            var scoreBoardClass = GetScoreBoard();
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("John", 10);

            var boardWithScores = scoreBoardClass.GetHighScores(2);
            Assert.IsTrue(boardWithScores.Count == 1);
            Assert.IsTrue(boardWithScores[0].Value.Count == 1);
        }

        [TestMethod]
        public void Add2PeopleWithSameNamesAndDifferentScores()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
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
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            int count = 0;
            scoreBoardClass.GetHighScores(count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetHighScoresWithNegativeCount()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            int count = -1;
            scoreBoardClass.GetHighScores(count);
        }

        [TestMethod]
        public void GetHighScoresWith1Person()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            scoreBoardClass.AddPlayer("John", 20);

            var hightScores = scoreBoardClass.GetHighScores(scoreBoardClass.board.Count);

            Assert.IsTrue(hightScores[0].Key == 20);
            Assert.IsTrue(hightScores[0].Value[0] == "John");
        }

        [TestMethod]
        public void GetHighScoresWith3PeopleWithDifferentNames()
        {
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
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
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
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
            var scoreBoardClass = new ScoreBoard(FILE_PATH);
            scoreBoardClass.AddPlayer("John", 10);
            scoreBoardClass.AddPlayer("Gosho", 10);
            scoreBoardClass.AddPlayer("Pesho", 10);

            var hightScores = scoreBoardClass.GetHighScores(scoreBoardClass.board.Count);

            Assert.IsTrue(hightScores[0].Key == 10);
            Assert.IsTrue(hightScores[0].Value[0] == "Gosho");
            Assert.IsTrue(hightScores[0].Value[1] == "John");
            Assert.IsTrue(hightScores[0].Value[2] == "Pesho");
        }

        private ScoreBoard GetScoreBoard()
        {
            ScoreBoard scoreBoard = new ScoreBoard(FILE_PATH);
            scoreBoard.FullDeleteList();
            return scoreBoard;
        }
    }
}
