using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    class Item
    {
        public bool itemEquipBool;
        public string itemName;
        public int itemAttack;
        public int itemShield;
        public string itemEtc;
        public int itemPrice;
        public Type type;
        public override string ToString()
        {
            return $"{itemEquipBool},{itemName},{itemAttack},{itemShield},{itemEtc},{itemPrice},{type}";
        }
        public static Item FromString(string line)
        {
            string[] parts = line.Split(',');
            return new Item
            {
                itemEquipBool = Boolean.Parse(parts[0]),
                itemName = parts[1],
                itemAttack = int.Parse(parts[2]),
                itemShield = int.Parse(parts[3]),
                itemEtc = parts[4],
                itemPrice = int.Parse(parts[5]),
                type = Enum.Parse<Type>(parts[6])
            };
        }
    }
}
