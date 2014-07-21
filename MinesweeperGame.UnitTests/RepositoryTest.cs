﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MinesweeperGame.UnitTests
{
    /// <summary>
    /// Unit test for the class MediatorExtensions.cs
    /// Todo Add more tests
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void AllRepositoryMethodsTest()
        {
            Repository dataRepository = new Repository();
            var DATA_ROOT = "..\\..\\..\\MinesweeperGame.Demo\\players.xml";

            var playersList = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(1, "Joan Sirakov"),
                new Tuple<int, string>(2, "Stoyan Stoyanov"),
                new Tuple<int, string>(3, "Petar Milchev"),
                new Tuple<int, string>(4, "Tancho Mihov"),
                new Tuple<int, string>(5, "Plamen Stanev"),
                new Tuple<int, string>(6, "Ilian Yordanov"),
                new Tuple<int, string>(7, "Ivelin Stanchev")
            };

            // remove all test and get all test
            dataRepository.EmptyFile(DATA_ROOT);
            var scoreBoard1 = dataRepository.GetPlayers(DATA_ROOT);
            Assert.AreEqual(0, scoreBoard1.Count);

            // add player test and get all test
            foreach (var tup in playersList)
            {
                dataRepository.AddPlayer(DATA_ROOT, tup.Item2, tup.Item1);
            }
            var scoreBoard2 = dataRepository.GetPlayers(DATA_ROOT);
            Assert.AreEqual(playersList.Count, scoreBoard2.Count);
        }
    }
}