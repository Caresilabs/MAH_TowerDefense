using MAH_TowerDefense.LevelEditor;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MAH_TowerDefense
{
    public static class LevelIO
    {
        public static void SaveLevel(LevelModel.SingleLevel level, bool insert) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LevelModel));
            StringWriter textWriter = new StringWriter();

            LevelModel allLevels = ReadFile();

            if (insert)
            {
                allLevels.levels.Where(x => x.LevelIndex >= level.LevelIndex).ToList().ForEach(x => x.LevelIndex++);
                allLevels.levels.Insert(level.LevelIndex - 1, level);
            }
            else
            {
                if (level.LevelIndex > allLevels.levels.Count)
                    allLevels.levels.Add(level);
                else
                    allLevels.levels[level.LevelIndex -1] = level;
            }

            xmlSerializer.Serialize(textWriter, allLevels);
            System.IO.File.WriteAllText("Content/levels.txt", textWriter.ToString());
        }


        public static LevelModel ReadFile()
        {
            string xml = System.IO.File.ReadAllText("Content/levels.txt");

            if (xml == "")
            {
                return new LevelModel() { levels = new List<LevelModel.SingleLevel>() };
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LevelModel));
            StringReader textReader = new StringReader(xml);
            return (LevelModel)xmlSerializer.Deserialize(textReader);
        }

        public static int LevelCount()
        {
            return ReadFile().levels.Count;
        }

        public static LevelModel.SingleLevel LoadLevel(int index)
        {
            return ReadFile().levels.First(x => x.LevelIndex == index);
        }


        public static void DeleteLevel(int level)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LevelModel));
            StringWriter textWriter = new StringWriter();

            LevelModel allLevels = ReadFile();

            allLevels.levels.RemoveAt(level - 1);

            xmlSerializer.Serialize(textWriter, allLevels);
            System.IO.File.WriteAllText("Content/levels.txt", textWriter.ToString());
        }
    }
}
