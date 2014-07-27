using MinesweeperGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Wintellect.PowerCollections;

namespace MinesweeperGame
{
    public class Repository : IRepository
    {
        private int currentPlayers = 0;

        /// <summary>
        /// Gets the players from the storage file
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path</param>
        /// <returns>OrderedMultiDictionary with the player data <int, string></returns>
        public OrderedMultiDictionary<int, string> GetPlayers(string playerStoreDocumentPath)
        {
            this.currentPlayers = 0;
            var savedPlayers = new OrderedMultiDictionary<int, string>(false);
            var playerDocumentRoot = XDocument.Load(playerStoreDocumentPath).Root;

            foreach (var player in playerDocumentRoot.Elements("player"))
            {
                var playerName = player.Element("name").Value;
                var playerScore = int.Parse(player.Element("points").Value);

                if (!savedPlayers.ContainsKey(playerScore))
                {
                    savedPlayers.Add(playerScore, playerName);
                }
                else
                {
                    savedPlayers[playerScore].Add(playerName);
                }
                this.currentPlayers++;
            }

            return savedPlayers;
        }


        /// <summary>
        /// Stores the scores to a file.
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path.</param>
        /// <param name="scores">High scores.</param>
        public void StorePlayers(string playerStoreDocumentPath, OrderedMultiDictionary<int, string> scores)
        {
            var root = XDocument.Load(playerStoreDocumentPath).Root;
            foreach (var score in scores.Keys)
            {
                foreach (var player in scores[score])
                {
                    root.Add(new XElement("player",
                        new XElement("name", player),
                        new XElement("points", score)
                    ));
                }
            }

            root.Document.Save(playerStoreDocumentPath);
        }

        /// <summary>
        /// Deletes all players in the database file
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path</param>
        public void EmptyFile(string playerStoreDocumentPath)
        {
            var root = XDocument.Load(playerStoreDocumentPath).Root;
            root.RemoveAll();
            root.Document.Save(playerStoreDocumentPath);
            this.currentPlayers = 0;
            //XDocument xdoc = XDocument.Load(playerStoreDocumentPath);
            //xdoc.Descendants("player")
            //    .Where(x => (string)x.Element("Name") == name)
            //    .Remove();
        }
    }
}
