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
        
    }
    public enum Level
    {
        easy = 1,
        normal,
        hard
    }
}
