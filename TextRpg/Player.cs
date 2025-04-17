using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    class Player
    {
        public int level = 1;
        public string name;
        public Job job;
        public int power = 10;
        public int shield = 5;
        public int hp = 100;
        public int gold = 15000;

        public Player(string name)
        {
            this.name = name;
        }   
    }
    public enum Job
    {
        warrior = 1,
        assasin
    }
}
