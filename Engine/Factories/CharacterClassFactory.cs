using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    public static class CharacterClassFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\CharacterClasses.xml";

        private static readonly List<CharacterClass> _characterClasses = new List<CharacterClass>();

        static CharacterClassFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadCharacterClassesFromNodes(data.SelectNodes("/CharacterClasses/CharacterClass"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadCharacterClassesFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                CharacterClass characterClass =
                    new CharacterClass(node.AttributeAsInt("ID"),
                               node.SelectSingleNode("./Name")?.InnerText ?? "",
                               node.AttributeAsInt("HitDice"),
                               node.SelectSingleNode("./Description")?.InnerText ?? "");

                _characterClasses.Add(characterClass);
            }
        }

        public static CharacterClass GetCharacterClassByID(int id)
        {
            return _characterClasses.FirstOrDefault(t => t.ID == id);
        }

        public static List<CharacterClass> GetCharacterClassList()
        {
            return _characterClasses;
        }
    }
}