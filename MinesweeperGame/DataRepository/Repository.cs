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
    public class Repository
    {
        public static OrderedMultiDictionary<int, string> GetPlayers(string playerStoreDocumentPath)
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
    }
}
