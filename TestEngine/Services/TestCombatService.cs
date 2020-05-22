using Engine.Models;
using Engine.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Factories;

namespace TestEngine.Services
{
    [TestClass]
    public class TestCombatService
    {
        [TestMethod]
        public void Test_FirstAttacker()
        {
            // Player and monster with dexterity 10
            Player player = new Player("", CharacterRaceFactory.GetCharacterRaceByID(1), CharacterClassFactory.GetCharacterClassByID(1), 10, 10, 10 , 10, 10, 10, 0, 10, 10, 0);
            Monster monster = new Monster(0, "", "", 0, 10, 10, 10, 10, 10, 10, null, 0, 0);

            CombatService.Combatant result = CombatService.FirstAttacker(player, monster);
        }
    }
}