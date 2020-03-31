using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using System.Xml;
using Engine.ViewModels;
using System.IO;

namespace Engine.Services
{
    public class SaveLoadGame
    {
        public const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";

        public static readonly SaveLoadGame saveLoadGame = new SaveLoadGame();


        public string XMLSaveData(GameSession gs)
        {
            XmlDocument playerData = new XmlDocument();

            // Create the top-level XML node
            XmlNode player = playerData.CreateElement("Player");
            playerData.AppendChild(player);

            // Create the "Stats" child node to hold the other player statistics nodes
            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);

            // Create the child nodes for the "Stats" node

            XmlNode playerName = playerData.CreateElement("PlayerName");
            playerName.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Name));
            stats.AppendChild(playerName);

            XmlNode playerClass = playerData.CreateElement("PlayerClass");
            playerClass.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.CharacterClass));
            stats.AppendChild(playerClass);

            XmlNode currentHitPoints = playerData.CreateElement("CurrentHitPoints");
            currentHitPoints.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.CurrentHitPoints.ToString()));
            stats.AppendChild(currentHitPoints);

            XmlNode maximumHitPoints = playerData.CreateElement("MaximumHitPoints");
            maximumHitPoints.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.MaximumHitPoints.ToString()));
            stats.AppendChild(maximumHitPoints);

            XmlNode gold = playerData.CreateElement("Gold");
            gold.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Gold.ToString()));
            stats.AppendChild(gold);

            XmlNode level = playerData.CreateElement("Level");
            level.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Level.ToString()));
            stats.AppendChild(level);

            XmlNode experiencePoints = playerData.CreateElement("ExperiencePoints");
            experiencePoints.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.ExperiencePoints.ToString()));
            stats.AppendChild(experiencePoints);

            XmlNode currentLocation = playerData.CreateElement("CurrentLocation");

            XmlAttribute yAttribute = playerData.CreateAttribute("Y");
            yAttribute.Value = gs.CurrentLocation.YCoordinate.ToString();
            currentLocation.Attributes.Append(yAttribute);

            XmlAttribute xAttribute = playerData.CreateAttribute("X");
            xAttribute.Value = gs.CurrentLocation.XCoordinate.ToString();
            currentLocation.Attributes.Append(xAttribute);

            stats.AppendChild(currentLocation);

            // Create the "InventoryItems" child node to hold each InventoryItem node
            XmlNode inventoryItems = playerData.CreateElement("InventoryItems");
            player.AppendChild(inventoryItems);

            // Create an "InventoryItem" node for each item in the player's inventory
            foreach (GameItem item in gs.CurrentPlayer.Inventory.Items)
            {
                XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = item.ItemTypeID.ToString();
                inventoryItem.Attributes.Append(idAttribute);

                XmlAttribute quantityAttribute = playerData.CreateAttribute("Quantity");
                quantityAttribute.Value = item.Name;
                inventoryItem.Attributes.Append(quantityAttribute);

                inventoryItems.AppendChild(inventoryItem);
            }

            // Create the "PlayerQuests" child node to hold each PlayerQuest node
            XmlNode playerQuests = playerData.CreateElement("PlayerQuests");
            player.AppendChild(playerQuests);

            // Create a "PlayerQuest" node for each quest the player has acquired
            foreach (QuestStatus quest in gs.CurrentPlayer.Quests)
            {
                XmlNode playerQuest = playerData.CreateElement("PlayerQuest");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = quest.PlayerQuest.ID.ToString();
                playerQuest.Attributes.Append(idAttribute);

                XmlAttribute isCompletedAttribute = playerData.CreateAttribute("IsCompleted");
                isCompletedAttribute.Value = quest.IsCompleted.ToString();
                playerQuest.Attributes.Append(isCompletedAttribute);

                playerQuests.AppendChild(playerQuest);
            }

            return playerData.InnerXml; // The XML document, as a string, so we can save the data to disk
        }



    }


}
