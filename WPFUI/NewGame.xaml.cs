using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Engine.Factories;
using Engine.Models;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace WPFUI
{
    public partial class NewGame : Window
    {

        private static CharacterClass selectedClass;
        private static CharacterRace selectedRace;

        public List<CharacterClass> CharacterClassList = CharacterClassFactory.GetCharacterClassList();
        public List<CharacterRace> CharacterRaceList = CharacterRaceFactory.GetCharacterRaceList();


        public int selectedStrength;
        public int selectedEndurance;
        public int selectedDexterity;
        public int selectedIntelligence;
        public int selectedCharisma;
        public int selectedLuck;

        int minStatValue = 3, maxStatValue = 18, startStatValue = 10, commonStatsPool = 25;


        public NewGame()
        {
            InitializeComponent();

            txtBoxStrength.Text = startStatValue.ToString();
            txtBoxEndurance.Text = startStatValue.ToString();
            txtBoxDexterity.Text = startStatValue.ToString();
            txtBoxIntelligence.Text = startStatValue.ToString();
            txtBoxCharisma.Text = startStatValue.ToString();
            txtBoxLuck.Text = startStatValue.ToString();
        }

        private IEnumerable<ToggleButton> ListCharacterRacesButtons()
        {
            IEnumerable<ToggleButton> list = GridRaces.Children.OfType<ToggleButton>();
            return list;
        }

        private IEnumerable<ToggleButton> ListCharacterClassesButtons()
        {
            IEnumerable<ToggleButton> list = GridClasses.Children.OfType<ToggleButton>();
            return list;
        }


        private void OnClick_BackToMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Close();
            mainMenu.ShowDialog();
        }

        private void OnClick_StartGame(object sender, RoutedEventArgs e)
        {
            if (IsCharacterCreationFinished())
            {
                MainWindow mainWindow = new MainWindow(txtPlayerName.Text, selectedRace, selectedClass, 
                    selectedStrength, selectedEndurance, selectedDexterity, selectedIntelligence, selectedCharisma, selectedLuck);

                Close();
                mainWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("You have to finish your character creation");
            }
        }

        public void txtPlayerName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Enter Name")
            {
                tb.Text = string.Empty;
                tb.GotFocus -= txtPlayerName_GotFocus;
            }
        }

        public bool IsCharacterCreationFinished()
        {
            if (txtPlayerName.Text.Trim() != ""
                &&
                selectedRace != null
                &&
                selectedClass != null
                &&
                ListCharacterRacesButtons().Any(i => i.IsChecked == true)
                &&
                ListCharacterClassesButtons().Any(i => i.IsChecked == true)
                &&
                commonStatsPool == 0)
            {   
               return true;  
            }

            return false;
        }


        #region Character Races buttons

        private void btnCharacterRaceHuman_Checked(object sender, RoutedEventArgs e)
        {
            selectedRace = CharacterRaceList.FirstOrDefault(n => n.CharacterRaceName == "Human");

            foreach (var button in ListCharacterRacesButtons().Where(b => !b.Content.ToString().Contains(selectedRace.CharacterRaceName)))
            {
                button.IsChecked = false;
            }            

            foreach (var item in ListCharacterClassesButtons())
            {
                if (item.Content.ToString() == selectedRace.AvailableClasses.Find(o => o == item.Content.ToString()))
                {
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsEnabled = false;
                }
            }

            txtCharacterRaceDescription.Text = selectedRace.CharacterRaceDescription;
        }


        private void btnCharacterRaceElf_Checked(object sender, RoutedEventArgs e)
        {
            selectedRace = CharacterRaceList.FirstOrDefault(n => n.CharacterRaceName == "Elf");

            foreach (var button in ListCharacterRacesButtons().Where(b => !b.Content.ToString().Contains(selectedRace.CharacterRaceName)))
            {
                button.IsChecked = false;
            }

            foreach (var item in ListCharacterClassesButtons())
            {
                if (item.Content.ToString() == selectedRace.AvailableClasses.Find(o => o == item.Content.ToString()))
                {
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsEnabled = false;
                }
            }

            txtCharacterRaceDescription.Text = selectedRace.CharacterRaceDescription;
        }


        private void btnCharacterRaceDwarf_Checked(object sender, RoutedEventArgs e)
        {
            selectedRace = CharacterRaceList.FirstOrDefault(n => n.CharacterRaceName == "Dwarf");

            foreach (var button in ListCharacterRacesButtons().Where(b => !b.Content.ToString().Contains(selectedRace.CharacterRaceName)))
            {
                button.IsChecked = false;
            }

            foreach (var item in ListCharacterClassesButtons())
            {
                if (item.Content.ToString() == selectedRace.AvailableClasses.Find(o => o == item.Content.ToString()))
                {
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsEnabled = false;
                }
            }

            txtCharacterRaceDescription.Text = selectedRace.CharacterRaceDescription;
        }

        private void btnCharacterRaceHalfling_Checked(object sender, RoutedEventArgs e)
        {
            selectedRace = CharacterRaceList.FirstOrDefault(n => n.CharacterRaceName == "Halfling");

            foreach (var button in ListCharacterRacesButtons().Where(b => !b.Content.ToString().Contains(selectedRace.CharacterRaceName)))
            {
                button.IsChecked = false;
            }

            foreach (var item in ListCharacterClassesButtons())
            {
                if (item.Content.ToString() == selectedRace.AvailableClasses.Find(o => o == item.Content.ToString()))
                {
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsEnabled = false;
                }
            }

            txtCharacterRaceDescription.Text = selectedRace.CharacterRaceDescription;
        }

        private void btnCharacterRaceGnome_Checked(object sender, RoutedEventArgs e)
        {
            selectedRace = CharacterRaceList.FirstOrDefault(n => n.CharacterRaceName == "Gnome");

            foreach (var button in ListCharacterRacesButtons().Where(b => !b.Content.ToString().Contains(selectedRace.CharacterRaceName)))
            {
                button.IsChecked = false;
            }

            foreach (var item in ListCharacterClassesButtons())
            {
                if (item.Content.ToString() == selectedRace.AvailableClasses.Find(o => o == item.Content.ToString()))
                {
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsEnabled = false;
                }
            }

            txtCharacterRaceDescription.Text = selectedRace.CharacterRaceDescription;
        }

        private void btnCharacterRaceOrc_Checked(object sender, RoutedEventArgs e)
        {
            selectedRace = CharacterRaceList.FirstOrDefault(n => n.CharacterRaceName == "Orc");

            foreach (var button in ListCharacterRacesButtons().Where(b => !b.Content.ToString().Contains(selectedRace.CharacterRaceName)))
            {
                button.IsChecked = false;
            }

            foreach (var item in ListCharacterClassesButtons())
            {
                if (item.Content.ToString() == selectedRace.AvailableClasses.Find(o => o == item.Content.ToString()))
                {
                    item.IsEnabled = true;
                }
                else
                {
                    item.IsEnabled = false;
                }
            }

            txtCharacterRaceDescription.Text = selectedRace.CharacterRaceDescription;
        }

        private void btnCharacterRace_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var button in ListCharacterClassesButtons())
            {
                button.IsEnabled = true;
            }

            txtCharacterRaceDescription.Text = "";
        }
        #endregion


        #region Character Classes buttons

        private void btnCharacterClassWarrior_Checked(object sender, RoutedEventArgs e)
        {
            selectedClass = CharacterClassList.FirstOrDefault(n => n.CharacterClassName == "Warrior");

            foreach (var button in ListCharacterClassesButtons().Where(b => !b.Content.ToString().Contains(selectedClass.CharacterClassName)))
            {
                button.IsChecked = false;
            }

            foreach (var race in CharacterRaceList)
            {
                if (race.AvailableClasses.Contains(selectedClass.CharacterClassName))
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = true;
                }
                else
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = false;
                }
            }

            txtCharacterClassDescription.Text = selectedClass.CharacterClassDescription;
        }

        private void btnCharacterClassMage_Checked(object sender, RoutedEventArgs e)
        {
            selectedClass = CharacterClassList.FirstOrDefault(n => n.CharacterClassName == "Mage");

            foreach (var button in ListCharacterClassesButtons().Where(b => !b.Content.ToString().Contains(selectedClass.CharacterClassName)))
            {
                button.IsChecked = false;
            }

            foreach (var race in CharacterRaceList)
            {
                if (race.AvailableClasses.Contains(selectedClass.CharacterClassName))
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = true;
                }
                else
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = false;
                }
            }

            txtCharacterClassDescription.Text = selectedClass.CharacterClassDescription;
        }

        private void btnCharacterClassRogue_Checked(object sender, RoutedEventArgs e)
        {
            selectedClass = CharacterClassList.FirstOrDefault(n => n.CharacterClassName == "Rogue");

            foreach (var button in ListCharacterClassesButtons().Where(b => !b.Content.ToString().Contains(selectedClass.CharacterClassName)))
            {
                button.IsChecked = false;
            }

            foreach (var race in CharacterRaceList)
            {
                if (race.AvailableClasses.Contains(selectedClass.CharacterClassName))
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = true;
                }
                else
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = false;
                }
            }

            txtCharacterClassDescription.Text = selectedClass.CharacterClassDescription;
        }

        private void btnCharacterClassPaladin_Checked(object sender, RoutedEventArgs e)
        {
            selectedClass = CharacterClassList.FirstOrDefault(n => n.CharacterClassName == "Paladin");

            foreach (var button in ListCharacterClassesButtons().Where(b => !b.Content.ToString().Contains(selectedClass.CharacterClassName)))
            {
                button.IsChecked = false;
            }

            foreach (var race in CharacterRaceList)
            {
                if (race.AvailableClasses.Contains(selectedClass.CharacterClassName))
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = true;
                }
                else
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = false;
                }
            }

            txtCharacterClassDescription.Text = selectedClass.CharacterClassDescription;
        }

        private void btnCharacterClassDruid_Checked(object sender, RoutedEventArgs e)
        {
            selectedClass = CharacterClassList.FirstOrDefault(n => n.CharacterClassName == "Druid");

            foreach (var button in ListCharacterClassesButtons().Where(b => !b.Content.ToString().Contains(selectedClass.CharacterClassName)))
            {
                button.IsChecked = false;
            }

            foreach (var race in CharacterRaceList)
            {
                if (race.AvailableClasses.Contains(selectedClass.CharacterClassName))
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = true;
                }
                else
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = false;
                }
            }

            txtCharacterClassDescription.Text = selectedClass.CharacterClassDescription;
        }

        private void btnCharacterClassHunter_Checked(object sender, RoutedEventArgs e)
        {
            selectedClass = CharacterClassList.FirstOrDefault(n => n.CharacterClassName == "Hunter");

            foreach (var button in ListCharacterClassesButtons().Where(b => !b.Content.ToString().Contains(selectedClass.CharacterClassName)))
            {
                button.IsChecked = false;
            }

            foreach (var race in CharacterRaceList)
            {
                if (race.AvailableClasses.Contains(selectedClass.CharacterClassName))
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = true;
                }
                else
                {
                    ListCharacterRacesButtons().First(o => o.Content.ToString() == race.CharacterRaceName).IsEnabled = false;
                }
            }

            txtCharacterClassDescription.Text = selectedClass.CharacterClassDescription;
        }

        private void btnCharacterClass_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var button in ListCharacterRacesButtons())
            {
                button.IsEnabled = true;
            }

            txtCharacterClassDescription.Text = "";
        }
        #endregion



        #region Player Stats - Strength 

        private void txtBoxStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxStrength.Text != "") number = Convert.ToInt32(txtBoxStrength.Text);
            else number = 0;
            if (number < maxStatValue) 
            {
                if (commonStatsPool > 0)
                {
                    txtBoxStrength.Text = Convert.ToString(number + 1);
                    commonStatsPool -= 1;
                    lblCommonStatsPool.Content = commonStatsPool.ToString();
                }

            }
        }

        private void txtBoxStrengthMinus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxStrength.Text != "") number = Convert.ToInt32(txtBoxStrength.Text);
            else number = 0;
            if (number > minStatValue)
            {
                txtBoxStrength.Text = Convert.ToString(number - 1);
                commonStatsPool += 1;
                lblCommonStatsPool.Content = commonStatsPool.ToString();
            }
        }

        private void txtBoxStrength_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtBoxStrengthPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxStrengthPlus, new object[] { true });
            }

            if (e.Key == Key.Down)
            {
                txtBoxStrengthMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxStrengthMinus, new object[] { true });
            }
        }

        private void txtBoxStrength_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxStrengthPlus, new object[] { false });

            if (e.Key == Key.Down) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxStrengthMinus, new object[] { false });
        }

        private void txtBoxStrength_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtBoxStrength.Text != "")
                if (!int.TryParse(txtBoxStrength.Text, out number)) txtBoxStrength.Text = startStatValue.ToString();
            if (number > maxStatValue) txtBoxStrength.Text = maxStatValue.ToString();
            if (number < minStatValue) txtBoxStrength.Text = minStatValue.ToString();
            txtBoxStrength.SelectionStart = txtBoxStrength.Text.Length;

            selectedStrength = Convert.ToInt32(txtBoxStrength.Text);
        }

        #endregion

        #region Player Stats - Endurance 

        private void txtBoxEndurancePlus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxEndurance.Text != "") number = Convert.ToInt32(txtBoxEndurance.Text);
            else number = 0;
            if (number < maxStatValue)
            {
                if (commonStatsPool > 0)
                {
                    txtBoxEndurance.Text = Convert.ToString(number + 1);
                    commonStatsPool -= 1;
                    lblCommonStatsPool.Content = commonStatsPool.ToString();
                }
            }
        }

        private void txtBoxEnduranceMinus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxEndurance.Text != "") number = Convert.ToInt32(txtBoxEndurance.Text);
            else number = 0;
            if (number > minStatValue)
            {
                txtBoxEndurance.Text = Convert.ToString(number - 1);
                commonStatsPool += 1;
                lblCommonStatsPool.Content = commonStatsPool.ToString();
            }
        }

        private void txtBoxEndurance_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtBoxEndurancePlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxEndurancePlus, new object[] { true });
            }

            if (e.Key == Key.Down)
            {
                txtBoxEnduranceMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxEnduranceMinus, new object[] { true });
            }
        }

        private void txtBoxEndurance_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxEndurancePlus, new object[] { false });

            if (e.Key == Key.Down) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxEnduranceMinus, new object[] { false });
        }

        private void txtBoxEndurance_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtBoxEndurance.Text != "")
                if (!int.TryParse(txtBoxEndurance.Text, out number)) txtBoxEndurance.Text = startStatValue.ToString();
            if (number > maxStatValue) txtBoxEndurance.Text = maxStatValue.ToString();
            if (number < minStatValue) txtBoxEndurance.Text = minStatValue.ToString();
            txtBoxEndurance.SelectionStart = txtBoxEndurance.Text.Length;

            selectedEndurance = Convert.ToInt32(txtBoxEndurance.Text);
        }

        #endregion

        #region Player Stats - Dexterity 

        private void txtBoxDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxDexterity.Text != "") number = Convert.ToInt32(txtBoxDexterity.Text);
            else number = 0;
            if (number < maxStatValue)
            {
                if (commonStatsPool > 0)
                {
                    txtBoxDexterity.Text = Convert.ToString(number + 1);
                    commonStatsPool -= 1;
                    lblCommonStatsPool.Content = commonStatsPool.ToString();
                }
            }
        }

        private void txtBoxDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxDexterity.Text != "") number = Convert.ToInt32(txtBoxDexterity.Text);
            else number = 0;
            if (number > minStatValue)
            {
                txtBoxDexterity.Text = Convert.ToString(number - 1);
                commonStatsPool += 1;
                lblCommonStatsPool.Content = commonStatsPool.ToString();
            }
        }

        private void txtBoxDexterity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtBoxDexterityPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxDexterityPlus, new object[] { true });
            }

            if (e.Key == Key.Down)
            {
                txtBoxDexterityMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxDexterityMinus, new object[] { true });
            }
        }

        private void txtBoxDexterity_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxDexterityPlus, new object[] { false });

            if (e.Key == Key.Down) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxDexterityMinus, new object[] { false });
        }

        private void txtBoxDexterity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtBoxDexterity.Text != "")
                if (!int.TryParse(txtBoxDexterity.Text, out number)) txtBoxDexterity.Text = startStatValue.ToString();
            if (number > maxStatValue) txtBoxDexterity.Text = maxStatValue.ToString();
            if (number < minStatValue) txtBoxDexterity.Text = minStatValue.ToString();
            txtBoxDexterity.SelectionStart = txtBoxDexterity.Text.Length;

            selectedDexterity = Convert.ToInt32(txtBoxDexterity.Text);
        }

        #endregion

        #region Player Stats - Intelligence 

        private void txtBoxIntelligencePlus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxIntelligence.Text != "") number = Convert.ToInt32(txtBoxIntelligence.Text);
            else number = 0;
            if (number < maxStatValue)
            {
                if (commonStatsPool > 0)
                {
                    txtBoxIntelligence.Text = Convert.ToString(number + 1);
                    commonStatsPool -= 1;
                    lblCommonStatsPool.Content = commonStatsPool.ToString();
                }
            }
        }

        private void txtBoxIntelligenceMinus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxIntelligence.Text != "") number = Convert.ToInt32(txtBoxIntelligence.Text);
            else number = 0;
            if (number > minStatValue)
            {
                txtBoxIntelligence.Text = Convert.ToString(number - 1);
                commonStatsPool += 1;
                lblCommonStatsPool.Content = commonStatsPool.ToString();
            }
        }

        private void txtBoxIntelligence_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtBoxIntelligencePlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxIntelligencePlus, new object[] { true });
            }

            if (e.Key == Key.Down)
            {
                txtBoxIntelligenceMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxIntelligenceMinus, new object[] { true });
            }
        }

        private void txtBoxIntelligence_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxIntelligencePlus, new object[] { false });

            if (e.Key == Key.Down) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxIntelligenceMinus, new object[] { false });
        }

        private void txtBoxIntelligence_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtBoxIntelligence.Text != "")
                if (!int.TryParse(txtBoxIntelligence.Text, out number)) txtBoxIntelligence.Text = startStatValue.ToString();
            if (number > maxStatValue) txtBoxIntelligence.Text = maxStatValue.ToString();
            if (number < minStatValue) txtBoxIntelligence.Text = minStatValue.ToString();
            txtBoxIntelligence.SelectionStart = txtBoxIntelligence.Text.Length;

            selectedIntelligence = Convert.ToInt32(txtBoxIntelligence.Text);
        }

        #endregion

        #region Player Stats - Charisma 

        private void txtBoxCharismaPlus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxCharisma.Text != "") number = Convert.ToInt32(txtBoxCharisma.Text);
            else number = 0;
            if (number < maxStatValue)
            {
                if (commonStatsPool > 0)
                {
                    txtBoxCharisma.Text = Convert.ToString(number + 1);
                    commonStatsPool -= 1;
                    lblCommonStatsPool.Content = commonStatsPool.ToString();
                }
            }
        }

        private void txtBoxCharismaMinus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxCharisma.Text != "") number = Convert.ToInt32(txtBoxCharisma.Text);
            else number = 0;
            if (number > minStatValue)
            {
                txtBoxCharisma.Text = Convert.ToString(number - 1);
                commonStatsPool += 1;
                lblCommonStatsPool.Content = commonStatsPool.ToString();
            }
        }

        private void txtBoxCharisma_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtBoxCharismaPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxCharismaPlus, new object[] { true });
            }

            if (e.Key == Key.Down)
            {
                txtBoxCharismaMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxCharismaMinus, new object[] { true });
            }
        }

        private void txtBoxCharisma_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxCharismaPlus, new object[] { false });

            if (e.Key == Key.Down) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxCharismaMinus, new object[] { false });
        }

        private void txtBoxCharisma_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtBoxCharisma.Text != "")
                if (!int.TryParse(txtBoxCharisma.Text, out number)) txtBoxCharisma.Text = startStatValue.ToString();
            if (number > maxStatValue) txtBoxCharisma.Text = maxStatValue.ToString();
            if (number < minStatValue) txtBoxCharisma.Text = minStatValue.ToString();
            txtBoxCharisma.SelectionStart = txtBoxCharisma.Text.Length;

            selectedCharisma = Convert.ToInt32(txtBoxCharisma.Text);
        }

        #endregion

        #region Player Stats - Luck 

        private void txtBoxLuckPlus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxLuck.Text != "") number = Convert.ToInt32(txtBoxLuck.Text);
            else number = 0;
            if (number < maxStatValue)
            {
                if (commonStatsPool > 0)
                {
                    txtBoxLuck.Text = Convert.ToString(number + 1);
                    commonStatsPool -= 1;
                    lblCommonStatsPool.Content = commonStatsPool.ToString();
                }
            }
        }

        private void txtBoxLuckMinus_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtBoxLuck.Text != "") number = Convert.ToInt32(txtBoxLuck.Text);
            else number = 0;
            if (number > minStatValue)
            {
                txtBoxLuck.Text = Convert.ToString(number - 1);
                commonStatsPool += 1;
                lblCommonStatsPool.Content = commonStatsPool.ToString();
            }
        }

        private void txtBoxLuck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                txtBoxLuckPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxLuckPlus, new object[] { true });
            }

            if (e.Key == Key.Down)
            {
                txtBoxLuckMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxLuckMinus, new object[] { true });
            }
        }

        private void txtBoxLuck_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxLuckPlus, new object[] { false });

            if (e.Key == Key.Down) typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(txtBoxLuckMinus, new object[] { false });
        }

        private void txtBoxLuck_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtBoxLuck.Text != "")
                if (!int.TryParse(txtBoxLuck.Text, out number)) txtBoxLuck.Text = startStatValue.ToString();
            if (number > maxStatValue) txtBoxLuck.Text = maxStatValue.ToString();
            if (number < minStatValue) txtBoxLuck.Text = minStatValue.ToString();
            txtBoxLuck.SelectionStart = txtBoxLuck.Text.Length;

            selectedLuck = Convert.ToInt32(txtBoxLuck.Text);
        }

        #endregion


    }

}
