using System.Linq;
using Engine.Factories;
using Engine.Models;
using Engine.Services;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();

        private Battle _currentBattle;

        #region Properties

        private Player _currentPlayer;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;

        public World CurrentWorld { get; }

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnPlayerKilled;
                }

                _currentPlayer = value;

                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnPlayerKilled;
                }
            }
        }

        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                CurrentMonster = CurrentLocation.GetMonster();

                CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentBattle != null)
                {
                    _currentBattle.OnCombatVictory -= OnCurrentMonsterKilled;
                    _currentBattle.Dispose();
                }

                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentBattle = new Battle(CurrentPlayer, CurrentMonster);

                    _currentBattle.OnCombatVictory += OnCurrentMonsterKilled;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        public Trader CurrentTrader
        {
            get => _currentTrader;
            set
            {
                _currentTrader = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;

        public bool HasLocationToWest =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        public bool HasMonster => CurrentMonster != null;

        public bool HasTrader => CurrentTrader != null;

        public string PlayerNextLevel => ShowNextLevel();

        #endregion

        /// <summary>
        /// Default GameSession and Player
        /// </summary>
        public GameSession()
        {
            CurrentPlayer = new Player("DefaultPlayer", CharacterRaceFactory.GetCharacterRaceByID(1), 
                CharacterClassFactory.GetCharacterClassByID(1), 10, 10, 10, 10, 10, 10, 0, 10, 10, 10);

            if (!CurrentPlayer.Inventory.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3003));

            CurrentPlayer.LearnRecipe(RecipeFactory.RecipeByID(1));


            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        /// <summary>
        /// New Game - GameSession and Player
        /// </summary>
        public GameSession(string selectedPlayerName, CharacterRace selectedPlayerRace, CharacterClass selectedPlayerClass, 
            int strength, int endurance, int dexterity, int intelligence, int charisma, int luck)
        {

            CurrentPlayer = new Player(selectedPlayerName, selectedPlayerRace, selectedPlayerClass, 
                strength, endurance, dexterity, intelligence, charisma, luck, 0, 
                selectedPlayerClass.CharacterClassHitDice + selectedPlayerRace.CharacterRaceHitPointModifier, 
                selectedPlayerClass.CharacterClassHitDice + selectedPlayerRace.CharacterRaceHitPointModifier, 10); 

            if (!CurrentPlayer.Inventory.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3003));

            CurrentPlayer.LearnRecipe(RecipeFactory.RecipeByID(1));

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        /// <summary>
        /// Load Game - GameSession and Player
        /// </summary>
        public GameSession(string loadedPlayerName, CharacterRace loadedPlayerRace, CharacterClass loadedPlayerClass, 
            int loadedStrength, int loadedEndurance, int loadedDexterity, int loadedIntelligence, int loadedCharisma, int loadedLuck,           
            int loadedLevel, int loadedExperiencePoints, int loadedMaxiumumHitPoints, int loadedCurrentHitPoints, int loadedGold,
            int loadedLocationX, int loadedLocationY, List<GameItem> loadedInventoryItems, Dictionary<Quest,bool> loadedPlayerQuests, 
            List<Recipe> loadedRecipes, Dictionary<Trader,List<GameItem>> loadedTradersWIthItems)
        {
            CurrentPlayer = new Player(loadedPlayerName, loadedPlayerRace, loadedPlayerClass, loadedStrength, loadedEndurance, loadedDexterity, 
                loadedIntelligence, loadedCharisma, loadedLuck, loadedExperiencePoints, loadedMaxiumumHitPoints, loadedCurrentHitPoints, loadedGold);

            CurrentPlayer.Level = loadedLevel;

            foreach (GameItem item in loadedInventoryItems)
            {
                CurrentPlayer.AddItemToInventory(item);
            }

            foreach (var item in loadedPlayerQuests)
            {
                CurrentPlayer.Quests.Add(new QuestStatus(item.Key));

                QuestStatus questCompleted =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == item.Key.ID);

                questCompleted.IsCompleted = item.Value;
            }

            foreach (Recipe recipe in loadedRecipes)
            {
                CurrentPlayer.LearnRecipe(recipe);
            }

            foreach (KeyValuePair<Trader,List<GameItem>> pair in loadedTradersWIthItems)
            {

                foreach (GameItem item in pair.Key.Inventory.Items)
                {
                    pair.Key.RemoveItemFromInventory(item);
                }

                foreach (GameItem item in pair.Value)
                {
                    pair.Key.AddItemToInventory(item);
                }
            }

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(loadedLocationX, loadedLocationY);

        }

        /// <summary>
        /// Returns next Player Level, in case of maximum level achieved display "Max", for use in UI
        /// </summary>
        public string ShowNextLevel()
        {
            if (_currentPlayer.Level + 1 < _currentPlayer.ExperienceLevels.Keys.Max())
            {
                return _currentPlayer.ExperienceLevels[_currentPlayer.Level + 1].ToString();
            }
            else
            return "Max";
        }

        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }

        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }

        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.Inventory.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        CurrentPlayer.RemoveItemsFromInventory(quest.ItemsToComplete);

                        _messageBroker.RaiseMessage("");
                        _messageBroker.RaiseMessage($"You completed the '{quest.Name}' quest");

                        // Give the player the quest rewards
                        _messageBroker.RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);

                        _messageBroker.RaiseMessage($"You receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            _messageBroker.RaiseMessage($"You receive a {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                        }

                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage($"You receive the '{quest.Name}' quest");
                    _messageBroker.RaiseMessage(quest.Description);

                    _messageBroker.RaiseMessage("Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        _messageBroker.RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    _messageBroker.RaiseMessage("And you will receive:");
                    _messageBroker.RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    _messageBroker.RaiseMessage($"   {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        _messageBroker.RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        public void AttackCurrentMonster()
        {
            _currentBattle.AttackOpponent();
        }

        public void UseCurrentConsumable()
        {
            if (CurrentPlayer.CurrentConsumable != null)
            {
                CurrentPlayer.UseCurrentConsumable();
            }
        }

        public void CraftItemUsing(Recipe recipe)
        {
            if (CurrentPlayer.Inventory.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveItemsFromInventory(recipe.Ingredients);

                foreach (ItemQuantity itemQuantity in recipe.OutputItems)
                {
                    for (int i = 0; i < itemQuantity.Quantity; i++)
                    {
                        GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(outputItem);
                        _messageBroker.RaiseMessage($"You craft 1 {outputItem.Name}");
                    }
                }
            }
            else
            {
                _messageBroker.RaiseMessage("You do not have the required ingredients:");
                foreach (ItemQuantity itemQuantity in recipe.Ingredients)
                {
                    _messageBroker.RaiseMessage($"  {itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
                }
            }
        }

        private void OnPlayerKilled(object sender, System.EventArgs e)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage("You have been killed.");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            // Get another monster to fight
            CurrentMonster = CurrentLocation.GetMonster();
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage($"You are now level {CurrentPlayer.Level}!");
            OnPropertyChanged(nameof(PlayerNextLevel));
        }

        public void SaveGame()
        {
            File.WriteAllText(SaveLoadGame.PLAYER_DATA_FILE_NAME, SaveLoadGame.saveLoadGame.XMLSaveData(this));
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage("Game has been saved");
        }

        public void LoadGame()
        {
            _messageBroker.RaiseMessage("Game has been loaded");
        }

    }
}