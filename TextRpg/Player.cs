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
        public int hp = 0;
        public int gold = 1500;

        public Player() { }
        public Player(string name)
        {
            this.name = name;
            level = 1;
            hp = 100;
            gold = 1500;
        }

        public static Player FromString(string line)
        {
            string[] parts = line.Split(',');
            return new Player
            {
                level = int.Parse(parts[0]),
                name = parts[1],
                job = Enum.Parse<Job>(parts[2]),
                power= int.Parse(parts[3]),
                shield= int.Parse(parts[4]),
                hp= int.Parse(parts[5]),
                gold= int.Parse(parts[6])
            };
        }
    }
    public enum Job
    {
        warrior = 1,
        assasin
    }
}
