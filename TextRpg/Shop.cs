using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    class Shop
    {
        public bool itemBuyBool;
        public string itemName;
        public int itemAttack;
        public int itemShield;
        public string itemEtc;
        public int price;
        public Type type;

        public override string ToString()
        {
            return $"{itemBuyBool},{itemName},{itemAttack},{itemShield},{itemEtc},{price},{type}";
        }
        public static Shop FromString(string line)
        {
            string[] parts = line.Split(',');
            return new Shop
            {
                itemBuyBool = bool.Parse(parts[0]),
                itemName = parts[1],
                itemAttack = int.Parse(parts[2]),
                itemShield = int.Parse(parts[3]),
                itemEtc = parts[4],
                price = int.Parse(parts[5]),
                type = Enum.Parse<Type>(parts[6])
            };
        }
    }
    
    public enum Type
    {
        Weapon = 1,
        Armor
    }
}
