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
        /// <summary>
        /// Gets the players from the storage file
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path</param>
        /// <returns>OrderedMultiDictionary with the player data <int, string></returns>
        public OrderedMultiDictionary<int, string> GetPlayers(string playerStoreDocumentPath)
        {
            var savedPlayers = new OrderedMultiDictionary<int, string>(true);
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
            }

            return savedPlayers;
        }

        /// <summary>
        /// Adds a player in the DB file
        /// </summary>
        /// <param name="documenPath">File path</param>
        /// <param name="name">Player name</param>
        /// <param name="points">Player points</param>
        public void AddPlayer(string documenPath, string name, int points)
        {
            var root = XDocument.Load(documenPath).Root;
            root.Add(new XElement("player",
                new XElement("name", name),
                new XElement("points", points)
                ));
            root.Document.Save(documenPath);
        }

        /// <summary>
        /// Deletes all players in the database file
        /// </summary>
        /// <param name="playerStoreDocumentPath">File path</param>
        public void EmptyFile(string playerStoreDocumentPath)
        {
            var root = XDocument.Load(playerStoreDocumentPath).Root;

            // todo fix the counter
            for (int i = 0; i < 100; i++)
            {
                foreach (var storeChild in root.Elements("player"))
                {
                    storeChild.Remove();
                    root.Document.Save(playerStoreDocumentPath);
                }
            }
            root.Document.Save(playerStoreDocumentPath);

            //XDocument xdoc = XDocument.Load(playerStoreDocumentPath);
            //xdoc.Descendants("player")
            //    .Where(x => (string)x.Element("Name") == name)
            //    .Remove();
        }
    }
}