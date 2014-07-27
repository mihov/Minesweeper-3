using System;
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
        private const string DATA_ROOT = @"..\..\players.xml";

        [Timeout(1000)]
        [TestMethod]
        public void AllRepositoryMethodsTest()
        {
            Repository dataRepository = new Repository();

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
                scoreBoard1.Add(tup.Item1, tup.Item2);
            }

            dataRepository.StorePlayers(DATA_ROOT, scoreBoard1);
            var scoreBoard2 = dataRepository.GetPlayers(DATA_ROOT);
            Assert.AreEqual(playersList.Count, scoreBoard2.Count);
        }

        //[Timeout(1000)]
        [TestMethod]
        public void BigDataTest()
        {
            Repository dataRepository = new Repository();

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
            var cycleLength = 10;

            // remove all test and get all test
            dataRepository.EmptyFile(DATA_ROOT);
            var scoreBoard1 = dataRepository.GetPlayers(DATA_ROOT);
            Assert.AreEqual(0, scoreBoard1.Count);


            for (int i = 0; i < cycleLength; i++)
            {
                // add player test and get all test
                foreach (var tup in playersList)
                {
                    scoreBoard1.Add(tup.Item1, tup.Item2);
                }
            }

            dataRepository.StorePlayers(DATA_ROOT, scoreBoard1);
            var scoreBoard2 = dataRepository.GetPlayers(DATA_ROOT);
            Assert.AreEqual(playersList.Count, scoreBoard2.KeyValuePairs.Count);
        }
    }
}
