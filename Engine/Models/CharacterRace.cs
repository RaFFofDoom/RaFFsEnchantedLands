using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class CharacterRace
    {
        public int ID { get; set; }
        public string CharacterRaceName { get; set; }
        public int CharacterRaceHitPointModifier { get; set; }
        public string CharacterRaceDescription { get; set; }
        public List<string> AvailableClasses { get; set; }


        public CharacterRace(int id, string characterRaceName, int characterRaceHitPointModifier, string characterRaceDescription, List<string> availableClasses)
        {
            ID = id;
            CharacterRaceName = characterRaceName;
            CharacterRaceHitPointModifier = characterRaceHitPointModifier;
            CharacterRaceDescription = characterRaceDescription;
            AvailableClasses = availableClasses;
        }
    }
}
