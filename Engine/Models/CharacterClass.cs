using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class CharacterClass
    {
        public int ID { get; set; }
        public string CharacterClassName { get; set; }
        public int CharacterClassHitDice { get; set; }
        public string CharacterClassDescription { get; set; }


        public CharacterClass(int id, string characterClassName, int characterClassHitDice, string characterClassDescription)
        {
            ID = id;
            CharacterClassName = characterClassName;
            CharacterClassHitDice = characterClassHitDice;
            CharacterClassDescription = characterClassDescription;
        }
    }
}
