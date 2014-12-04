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

            // clamp level
            level.LevelIndex = 1 + (int)MathHelper.Clamp(level.LevelIndex, 0, allLevels.levels.Count);

            if (insert)
            {
                allLevels.levels.Where(x => x.LevelIndex >= level.LevelIndex).ToList().ForEach(x => x.LevelIndex++);
                allLevels.levels.Insert(level.LevelIndex, level);
            }
            else
                allLevels.levels.Add(level);

            xmlSerializer.Serialize(textWriter, allLevels);
            System.IO.File.WriteAllText("Content/levels.txt", textWriter.ToString());
        }


        public static LevelModel ReadFile()
        {
            string xml = System.IO.File.ReadAllText("Content/levels.txt");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LevelModel));
            StringReader textReader = new StringReader(xml);
            return (LevelModel)xmlSerializer.Deserialize(textReader);
        }

        public static LevelModel.SingleLevel LoadLevel(int index)
        {
            return ReadFile().levels.First(x => x.LevelIndex == index);
        }

    }
}
