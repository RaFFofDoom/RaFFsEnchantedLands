using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private string _characterClass;
        private int _experiencePoints;

        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;

                OnPropertyChanged();

                exp_dict[0] = 0;
                exp_dict[1] = 0;
                exp_dict[2] = 50;
                exp_dict[3] = 120;
                exp_dict[4] = 200;
                exp_dict[5] = 300;
                exp_dict[6] = 450;
                exp_dict[7] = 620;
                exp_dict[8] = 810;
                exp_dict[9] = 1020;
                exp_dict[10] = 1250;
                exp_dict[11] = 0;

                LevelUp();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; }

        public ObservableCollection<Recipe> Recipes { get; }

        public Dictionary<int, int> exp_dict = new Dictionary<int, int>();



        #endregion

        public event EventHandler OnLeveledUp;

        public Player(string name, string characterClass, int experiencePoints,
                      int maximumHitPoints, int currentHitPoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
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

            int playerClassHitDice = 10;
            int raceHitPointModifier = 2;

            switch (ExperiencePoints)
            {
                default:
                    Level = 1;
                    MaximumHitPoints = Level * 10;
                    break;
                case int n when (n > 0 && n < exp_dict[2]):
                    Level = 1; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    break;
                case int n when (n >= exp_dict[2] && n < exp_dict[3]):
                    Level = 2; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[3] && n < exp_dict[4]):
                    Level = 3; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[4] && n < exp_dict[5]):
                    Level = 4; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[5] && n < exp_dict[6]):
                    Level = 5; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[6] && n < exp_dict[7]):
                    Level = 6; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[7] && n < exp_dict[8]):
                    Level = 7; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[8] && n < exp_dict[9]):
                    Level = 8; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[9] && n < exp_dict[10]):
                    Level = 9; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
                case int n when (n >= exp_dict[10]):
                    Level = 10; MaximumHitPoints = (Level * playerClassHitDice) + raceHitPointModifier;
                    if (Level != originalLevel) { OnLeveledUp?.Invoke(this, System.EventArgs.Empty); }
                    break;
            }
        }
    }
}