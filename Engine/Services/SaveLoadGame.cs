using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using System.Xml;
using Engine.ViewModels;
using Engine.Factories;
using System.Data;

namespace Engine.Services
{
    public class SaveLoadGame
    {
        public const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";

        public static readonly SaveLoadGame saveLoadGame = new SaveLoadGame();


        public string XMLSaveData(GameSession gs)
        {
            XmlDocument playerData = new XmlDocument();

            XmlNode saveGame = playerData.CreateElement("SaveGame");
            playerData.AppendChild(saveGame);

            XmlNode player = playerData.CreateElement("Player");
            saveGame.AppendChild(player);

            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);

            XmlNode playerName = playerData.CreateElement("PlayerName");
            playerName.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Name));
            stats.AppendChild(playerName);

            XmlNode playerClass = playerData.CreateElement("PlayerClass");
            playerClass.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.CharacterClass.ID.ToString()));
            stats.AppendChild(playerClass);

            XmlNode playerRace = playerData.CreateElement("PlayerRace");
            playerRace.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.CharacterRace.ID.ToString()));
            stats.AppendChild(playerRace);

            XmlNode playerStrength = playerData.CreateElement("Strength");
            playerStrength.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Strength.ToString()));
            stats.AppendChild(playerStrength);

            XmlNode playerEndurance = playerData.CreateElement("Endurance");
            playerEndurance.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Endurance.ToString()));
            stats.AppendChild(playerEndurance);

            XmlNode playerDexterity = playerData.CreateElement("Dexterity");
            playerDexterity.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Dexterity.ToString()));
            stats.AppendChild(playerDexterity);

            XmlNode playerIntelligence = playerData.CreateElement("Intelligence");
            playerIntelligence.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Intelligence.ToString()));
            stats.AppendChild(playerIntelligence);

            XmlNode playerCharisma = playerData.CreateElement("Charisma");
            playerCharisma.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Charisma.ToString()));
            stats.AppendChild(playerCharisma);

            XmlNode playerLuck = playerData.CreateElement("Luck");
            playerLuck.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Luck.ToString()));
            stats.AppendChild(playerLuck);

            XmlNode currentHitPoints = playerData.CreateElement("CurrentHitPoints");
            currentHitPoints.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.CurrentHitPoints.ToString()));
            stats.AppendChild(currentHitPoints);

            XmlNode maximumHitPoints = playerData.CreateElement("MaximumHitPoints");
            maximumHitPoints.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.MaximumHitPoints.ToString()));
            stats.AppendChild(maximumHitPoints);

            XmlNode level = playerData.CreateElement("Level");
            level.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Level.ToString()));
            stats.AppendChild(level);

            XmlNode experiencePoints = playerData.CreateElement("ExperiencePoints");
            experiencePoints.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.ExperiencePoints.ToString()));
            stats.AppendChild(experiencePoints);

            XmlNode gold = playerData.CreateElement("Gold");
            gold.AppendChild(playerData.CreateTextNode(gs.CurrentPlayer.Gold.ToString()));
            stats.AppendChild(gold);


            XmlNode currentLocation = playerData.CreateElement("CurrentLocation");
            player.AppendChild(currentLocation);

            XmlAttribute yAttribute = playerData.CreateAttribute("Y");
            yAttribute.Value = gs.CurrentLocation.YCoordinate.ToString();
            currentLocation.Attributes.Append(yAttribute);

            XmlAttribute xAttribute = playerData.CreateAttribute("X");
            xAttribute.Value = gs.CurrentLocation.XCoordinate.ToString();
            currentLocation.Attributes.Append(xAttribute);


            XmlNode inventoryItems = playerData.CreateElement("InventoryItems");
            player.AppendChild(inventoryItems);

            foreach (GameItem item in gs.CurrentPlayer.Inventory.Items)
            {
                XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = item.ItemTypeID.ToString();
                inventoryItem.Attributes.Append(idAttribute);

                inventoryItems.AppendChild(inventoryItem);
            }


            XmlNode playerQuests = playerData.CreateElement("PlayerQuests");
            player.AppendChild(playerQuests);

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


            XmlNode recipes = playerData.CreateElement("Recipes");
            player.AppendChild(recipes);

            foreach (Recipe r in gs.CurrentPlayer.Recipes)
            {
                XmlNode recipe = playerData.CreateElement("Recipe");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = r.ID.ToString();
                recipe.Attributes.Append(idAttribute);

                recipes.AppendChild(recipe);
            }


            XmlNode traders = playerData.CreateElement("Traders");
            saveGame.AppendChild(traders);

            foreach (Trader trader in TraderFactory.GetTraderList())
            {
                XmlNode traderNode = playerData.CreateElement("Trader");

                XmlAttribute traderID = playerData.CreateAttribute("TraderID");
                traderID.Value = trader.ID.ToString();
                traderNode.Attributes.Append(traderID);

                foreach (GameItem item in trader.Inventory.Items)
                {
                    XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                    XmlAttribute idAttribute = playerData.CreateAttribute("ItemID");
                    idAttribute.Value = item.ItemTypeID.ToString();
                    inventoryItem.Attributes.Append(idAttribute);

                    traderNode.AppendChild(inventoryItem);
                }

                traders.AppendChild(traderNode);

            }

            return playerData.InnerXml; // The XML document, as a string, so we can save the data to disk
        }

        public static GameSession XMLLoadData(string xmlPlayerData)
        {
            XmlDocument playerData = new XmlDocument();

            playerData.LoadXml(xmlPlayerData);

            string playerName = playerData.SelectSingleNode("/SaveGame/Player/Stats/PlayerName").InnerText;
            CharacterRace playerCharacterRace = CharacterRaceFactory.GetCharacterRaceByID(Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/PlayerRace").InnerText));
            CharacterClass playerCharacterClass = CharacterClassFactory.GetCharacterClassByID(Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/PlayerClass").InnerText));
            int playerStrength = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Strength").InnerText);
            int playerEndurance = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Endurance").InnerText);
            int playerDexterity = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Dexterity").InnerText);
            int playerIntelligence = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Intelligence").InnerText);
            int playerCharisma = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Charisma").InnerText);
            int playerLuck = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Luck").InnerText);
            int playerExperiencePoints = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/ExperiencePoints").InnerText);
            int playerMaximumHitPoitns = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/MaximumHitPoints").InnerText);
            int playerCurrentHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/CurrentHitPoints").InnerText);
            int playerGold = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Gold").InnerText);
            int playerLevel = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/Stats/Level").InnerText);
            int playerLocationX = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/CurrentLocation").Attributes["X"].Value);
            int playerLocationY = Convert.ToInt32(playerData.SelectSingleNode("/SaveGame/Player/CurrentLocation").Attributes["Y"].Value);


            List<GameItem> playerInventoryItems = new List<GameItem>();

            foreach (XmlNode node in playerData.SelectNodes("/SaveGame/Player/InventoryItems/InventoryItem"))
            {
                int id = Convert.ToInt32(node.Attributes["ID"].Value);

                playerInventoryItems.Add(ItemFactory.CreateGameItem(id));

            }

            List<Quest> playerQuests = new List<Quest>();
            Dictionary<Quest, bool> playerQuestsWithStatus = new Dictionary<Quest, bool>();

            foreach (XmlNode node in playerData.SelectNodes("/SaveGame/Player/PlayerQuests/PlayerQuest"))
            {
                int id = Convert.ToInt32(node.Attributes["ID"].Value);
                bool isCompleted = Convert.ToBoolean(node.Attributes["IsCompleted"].Value);

                playerQuests.Add(QuestFactory.GetQuestByID(id));

                playerQuestsWithStatus.Add(QuestFactory.GetQuestByID(id), isCompleted);
            }


            List<Recipe> playerRecipes = new List<Recipe>();

            foreach (XmlNode node in playerData.SelectNodes("/SaveGame/Player/Recipes/Recipe"))
            {
                int id = Convert.ToInt32(node.Attributes["ID"].Value);

                playerRecipes.Add(RecipeFactory.RecipeByID(id));

            }

            Dictionary<Trader, List<GameItem>> tradersWithItems = new Dictionary<Trader, List<GameItem>>();

            foreach (XmlNode node in playerData.SelectNodes("/SaveGame/Traders/Trader"))
            {
                int traderID = Convert.ToInt32(node.Attributes["TraderID"].Value);
                List<GameItem> traderItems = new List<GameItem>();

                foreach (XmlNode itemnode in node.SelectNodes("InventoryItem"))
                {
                    int itemID = Convert.ToInt32(itemnode.Attributes["ItemID"].Value);

                    traderItems.Add(ItemFactory.CreateGameItem(itemID));
                }

                tradersWithItems.Add(TraderFactory.GetTraderByID(traderID), traderItems);

            }

            GameSession gameSession = new GameSession(playerName, playerCharacterRace, playerCharacterClass, playerStrength, playerEndurance, 
                playerDexterity, playerIntelligence, playerCharisma, playerLuck, playerLevel, playerExperiencePoints, playerMaximumHitPoitns, 
                playerCurrentHitPoints, playerGold, playerLocationX, playerLocationY, playerInventoryItems, playerQuestsWithStatus, 
                playerRecipes, tradersWithItems);

            return gameSession;
        }

    }

}
