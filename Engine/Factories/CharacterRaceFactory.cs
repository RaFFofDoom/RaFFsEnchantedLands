using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    public static class CharacterRaceFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\CharacterRaces.xml";

        private static readonly List<CharacterRace> _characterRaces = new List<CharacterRace>();

        static CharacterRaceFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadCharacterRacesFromNodes(data.SelectNodes("/CharacterRaces/CharacterRace"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadCharacterRacesFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                List<string> availableClasses = new List<string>();

                foreach (XmlNode childNode in node.SelectNodes("./AvailableClasses/AvailableClass"))
                {
                    availableClasses.Add(childNode?.InnerText ?? "");
                }

                CharacterRace characterRace =
                    new CharacterRace(node.AttributeAsInt("ID"),
                               node.SelectSingleNode("./Name")?.InnerText ?? "",
                               node.AttributeAsInt("HitPointModifier"),
                               node.SelectSingleNode("./Description")?.InnerText ?? "",
                               availableClasses
                               );

                _characterRaces.Add(characterRace);
            }
        }

        public static CharacterRace GetCharacterRaceByID(int id)
        {
            return _characterRaces.FirstOrDefault(t => t.ID == id);
        }

        public static List<CharacterRace> GetCharacterRaceList()
        {
            return _characterRaces;
        }
    }
}