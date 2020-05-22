using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private CharacterClass _characterClass;
        private CharacterRace _characterRace;
        private int _experiencePoints;

        private Dictionary<int, int> _experienceLevels;

        public CharacterClass CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }

        public CharacterRace CharacterRace
        {
            get => _characterRace;
            set
            {
                _characterRace = value;
                OnPropertyChanged();
            }
        }

        public int ExperiencePoints
        {
            get => _experiencePoints;
            private set
            {
                _experiencePoints = value;

                OnPropertyChanged();
                LevelUp();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; }

        public ObservableCollection<Recipe> Recipes { get; }

        public Dictionary<int, int> ExperienceLevels
        {
            get => _experienceLevels;
            set
            {
                _experienceLevels = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event EventHandler OnLeveledUp;

        public Player(string name, CharacterRace characterRace, CharacterClass characterClass, int strength, int endurance, int dexterity, int intelligence, int charisma, int luck, 
            int experiencePoints, int maximumHitPoints, int currentHitPoints, int gold) :
            base(name, strength, endurance, dexterity, intelligence, charisma, luck, maximumHitPoints, currentHitPoints, gold)
        {
            ExperienceLevels = new Dictionary<int, int>();
            ExperienceLevels[0] = 0;
            ExperienceLevels[1] = 0;
            ExperienceLevels[2] = 50;
            ExperienceLevels[3] = 120;
            ExperienceLevels[4] = 200;
            ExperienceLevels[5] = 300;
            ExperienceLevels[6] = 450;
            ExperienceLevels[7] = 620;
            ExperienceLevels[8] = 810;
            ExperienceLevels[9] = 1020;
            ExperienceLevels[10] = 1250;
            ExperienceLevels[11] = 0;

            CharacterRace = characterRace;
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;

            Quests = new ObservableCollection<QuestStatus>();
            Recipes = new ObservableCollection<Recipe>();

        }

        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }

        private void LevelUp()
        {
            int originalLevel = Level;

            int playerClassHitDice = _characterClass.CharacterClassHitDice;
            int raceHitPointModifier = _characterRace.CharacterRaceHitPointModifier;

            switch (ExperiencePoints)
            {
                default:
                    Level = 1;
                    MaximumHitPoints = playerClassHitDice + raceHitPointModifier;
                    break;
                case int n when (n > 0 && n < ExperienceLevels[2]):
                    Level = 1; MaximumHitPoints = playerClassHitDice + raceHitPointModifier;
                    break;
                case int n when (n >= ExperienceLevels[2] && n < ExperienceLevels[3]):
                    Level = 2; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[3] && n < ExperienceLevels[4]):
                    Level = 3; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[4] && n < ExperienceLevels[5]):
                    Level = 4; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[5] && n < ExperienceLevels[6]):
                    Level = 5; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[6] && n < ExperienceLevels[7]):
                    Level = 6; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[7] && n < ExperienceLevels[8]):
                    Level = 7; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[8] && n < ExperienceLevels[9]):
                    Level = 8; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[9] && n < ExperienceLevels[10]):
                    Level = 9; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= ExperienceLevels[10]):
                    Level = 10; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
            }
        }
    }
}