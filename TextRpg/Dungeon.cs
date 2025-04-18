using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    class Dungeon
    {
        public string name;
        public int reqSheild;
        public int rewGold;
        public Level level;

        public override string ToString()
        {
            return $"{name},{reqSheild},{rewGold},{level}";
        }
        public static Dungeon FromString(string line)
        {
            string[] parts = line.Split(',');
            return new Dungeon
            {
                name = parts[0],
                reqSheild = int.Parse(parts[1]),
                rewGold = int.Parse(parts[2]),
                level = Enum.Parse<Level>(parts[3])
            };
        }
    }
    public enum Level
    {
        easy = 1,
        normal,
        hard
    }
}
