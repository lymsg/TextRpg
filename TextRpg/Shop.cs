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
    }

    public enum Type
    {
        Weapon = 1,
        Armor
    }
}
