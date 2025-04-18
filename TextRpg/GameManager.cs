using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRpg
{
    class GameManager
    {
        //Program.Player player = new Program.Player();
        //Player player = new Player();
        Player _player;
        Shop shop = new Shop();
        List<Item> items = new List<Item>();
        List<Shop> shopItems = new List<Shop>();
        List<Dungeon> dungeons = new List<Dungeon>();
        bool isEquipOpen = false;
        bool isShopOpen = false;
        bool isDungeonOpen = false;
        int successCount = 0;

        public void Start()
        {
            if (!File.Exists("save.txt"))                                           //저장한적이 없을시 = 게임처음시작시
            {
                
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "수련자 갑옷", itemAttack = 0, itemShield = 5, itemEtc = "수련에 도움을 주는 갑옷입니다.", price = 1000, type = Type.Armor });
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "무쇠갑옷", itemAttack = 0, itemShield = 9, itemEtc = "무쇠로 만들어져 튼튼한 갑옷입니다.", price = 2000, type = Type.Armor });
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "스파르타의 갑옷", itemAttack = 0, itemShield = 15, itemEtc = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", price = 3500, type = Type.Armor });
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "낡은 검", itemAttack = 2, itemShield = 0, itemEtc = "쉽게 볼 수 있는 낡은 검입니다", price = 600, type = Type.Weapon });
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "청동 도끼", itemAttack = 5, itemShield = 0, itemEtc = "어디선가 사용됐던거 같은 도끼입니다", price = 1500, type = Type.Weapon });
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "스파르타의 창", itemAttack = 7, itemShield = 0, itemEtc = "스파르타의 전사들이 사용했다는 전설의 창입니다", price = 3000, type = Type.Weapon });
                shopItems.Add(new Shop { itemBuyBool = false, itemName = "장미칼", itemAttack = 100, itemShield = 0, itemEtc = "전설의 아이템", price = 10000, type = Type.Weapon });

                dungeons.Add(new Dungeon { name = "쉬운 던전", reqSheild = 5, rewGold = 1000, level = Level.easy });
                dungeons.Add(new Dungeon { name = "일반 던전", reqSheild = 11, rewGold = 1700, level = Level.normal });
                dungeons.Add(new Dungeon { name = "어려운 던전", reqSheild = 17, rewGold = 2500, level = Level.hard });


                Init();

            }
            else
            {
                Load();
                VillageWindow();
            }

        }
        void Init()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.\n");
                string? name = Console.ReadLine();
                Console.WriteLine($"입력하신 이름은 {name}입니다.\n");
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    switch (select)
                    {
                        case 1:
                            isRunning = false;
                            //Player _player = new Player(name);
                            _player = new Player(name);
                            JobSelectWindow();
                            break;
                        case 2:
                            Console.WriteLine("취소했습니다");
                            Thread.Sleep(1000);
                            break;
                        default:
                            Console.WriteLine("입력값이 올바르지않습니다");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
            VillageWindow();
        }

        void JobSelectWindow()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 직업을 설정해주세요.\n");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 도적");
                Console.WriteLine("\n원하시는 직업을 선택해주세요.\n");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int job))
                {
                    _player.job = (Job)job;
                    switch (_player.job)
                    {
                        case Job.warrior:
                            isRunning = false;
                            Console.WriteLine($"{_player.job}을 선택하셨습니다");
                            break;
                        case Job.assasin:
                            isRunning = false;
                            Console.WriteLine($"{_player.job}을 선택하셨습니다");
                            break;
                        default:
                            Console.WriteLine("입력값이 올바르지않습니다");
                            Thread.Sleep(1000);
                            break;

                    }
                }
            }

        }
        void VillageWindow()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("6. 저장하기");
                Console.WriteLine("7. 게임종료");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    switch (select)
                    {
                        case 1:
                            isRunning = false;
                            StatusWindow();
                            break;
                        case 2:
                            isRunning = false;
                            InventoryWindow();
                            break;
                        case 3:
                            isRunning = false;
                            ShopWindow();
                            break;
                        case 4:
                            isRunning = false;
                            DungeonWindow();
                            break;
                        case 5:
                            isRunning = false;
                            RestWindow();
                            break;
                        case 6:
                            isRunning = false;
                            SaveWindow();
                            break;
                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
        }


        void StatusWindow()                                                                                                   //상태보기 창
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine("Lv.{0}", _player.level);
                Console.WriteLine("{0} ( {1} )", _player.name, _player.job);
                Console.WriteLine("공격력 : {0}", _player.power);
                Console.WriteLine("방어력 : {0}", _player.shield);
                Console.WriteLine("체력 : {0}", _player.hp);
                Console.WriteLine("Gold : {0}", _player.gold);
                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    switch (select)
                    {
                        case 0:
                            isRunning = false;
                            VillageWindow();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
        }

        void InventoryWindow()                                                                                            //인벤토리 창
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]\n");
                if (items.Count == 0)
                {
                    Console.WriteLine("장비아이템이 없습니다.");
                }
                else
                {
                    foreach (Item item in items)
                    {
                        string equipBool = item.itemEquipBool ? "[E]" : "";                //삼항 연산자
                        string attackShield;                                                //공,방중 보여줄거 선택
                        
                        if (item.type == Type.Armor)
                        {
                            attackShield = "방어력 +" + item.itemShield;
                        }
                        else
                        {
                            attackShield = "공격력 +" + item.itemAttack;
                        }
                        Console.WriteLine("{0}{1} | {2} | {3}", equipBool, item.itemName, attackShield, item.itemEtc);
                    }
                }
                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    switch (select)
                    {
                        case 0:
                            isRunning = false;
                            VillageWindow();
                            break;
                        case 1:
                            isRunning = false;
                            EquipWindow();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
        }

        void EquipWindow()                                                                                    //장비 착용페이지
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");
                int itemNum = 0;
                if (items.Count == 0)
                {
                    Console.WriteLine("장비아이템이 없습니다.");
                }
                else
                {
                    foreach (Item item in items)
                    {
                        itemNum++;
                        string equipBool = item.itemEquipBool ? "[E]" : "";
                        string attackShield;
                        if (item.type == Type.Armor)
                        {
                            attackShield = "방어력 +" + item.itemShield;
                        }
                        else
                        {
                            attackShield = "공격력 +" + item.itemAttack;
                        }
                        Console.WriteLine("-{0} {1}{2} | {3} | {4}", itemNum, equipBool, item.itemName, attackShield, item.itemEtc);


                    }
                }
                Console.WriteLine("\n0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    if (select == 0)
                    {
                        isRunning = false;
                        InventoryWindow();
                    }
                    else if (select <= items.Count)
                    {
                        if (items[select - 1].itemEquipBool == false)                                                               //선택한 아이템이 장비해제된 상태일때
                        {
                            bool equiped = false;
                            for (int i = 0; i < items.Count; i++)                                                                   //이미 장비한 아이템 탐색
                            {
                                if (items[i].itemEquipBool == true && items[i].type == items[select - 1].type)                      //장비한 아이템과 선택한 장비가 같은 타입일때
                                {
                                    if (items[i].itemName != items[select - 1].itemName)                                            //선택한 아이템과 이름이 다르면 전장비 장비해제후 선택한 장비 장비착용
                                    {
                                        items[i].itemEquipBool = false;
                                        items[select - 1].itemEquipBool = true;
                                        _player.power -= items[i].itemAttack;
                                        _player.shield -= items[i].itemShield;
                                        _player.power += items[select - 1].itemAttack;
                                        _player.shield += items[select - 1].itemShield;
                                        Console.WriteLine($"{items[i].itemName}을 장비해제했습니다.\n");
                                        Thread.Sleep(500);
                                        Console.WriteLine($"{items[select - 1].itemName}을 장비했습니다.");
                                        Thread.Sleep(1000);
                                        equiped = true;                                                                             //중복착용 로직 실행되면 추가실행 안되도록 equiped 변수 추가
                                        break;
                                    }
                                }
                            }
                            if (!equiped)                                                                                           //중복착용 로직이 발동안되면
                            {
                                _player.power += items[select - 1].itemAttack;
                                _player.shield += items[select - 1].itemShield;
                                items[select - 1].itemEquipBool = true;
                                Console.WriteLine($"{items[select - 1].itemName}을 장비했습니다.");
                                Thread.Sleep(1000);
                            }

                        }
                        else
                        {
                            _player.power -= items[select - 1].itemAttack;
                            _player.shield -= items[select - 1].itemShield;
                            items[select - 1].itemEquipBool = false;
                            Console.WriteLine($"{items[select - 1].itemName}을 장비해제했습니다.");
                            Thread.Sleep(1000);
                        }

                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(1000);
                }
            }
        }
        void ShopWindow()                                                                                             //상점 창
        {
            //if (!isShopOpen)
            //{
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "수련자 갑옷", itemAttack = 0, itemShield = 5, itemEtc = "수련에 도움을 주는 갑옷입니다.", price = 1000 ,type = Type.Armor});
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "무쇠갑옷", itemAttack = 0, itemShield = 9, itemEtc = "무쇠로 만들어져 튼튼한 갑옷입니다.", price = 2000, type = Type.Armor });
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "스파르타의 갑옷", itemAttack = 0, itemShield = 15, itemEtc = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", price = 3500, type = Type.Armor });
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "낡은 검", itemAttack = 2, itemShield = 0, itemEtc = "쉽게 볼 수 있는 낡은 검입니다", price = 600, type = Type.Weapon });
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "청동 도끼", itemAttack = 5, itemShield = 0, itemEtc = "어디선가 사용됐던거 같은 도끼입니다", price = 1500, type = Type.Weapon });
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "스파르타의 창", itemAttack = 7, itemShield = 0, itemEtc = "스파르타의 전사들이 사용했다는 전설의 창입니다", price = 3000, type = Type.Weapon });
            //    shopItems.Add(new Shop { itemBuyBool = false, itemName = "장미칼", itemAttack = 100, itemShield = 0, itemEtc = "전설의 아이템", price = 10000 ,type = Type.Weapon });
            //    isShopOpen = true;
            //}
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]\n{0}\n", _player.gold);
                Console.WriteLine("[아이템 목록]");
                foreach (Shop shop in shopItems)
                {
                    string attackShield;                                                //공,방중 보여줄거 선택
                    if (shop.itemAttack == 0)
                    {
                        attackShield = "방어력 +" + shop.itemShield;
                    }
                    else
                    {
                        attackShield = "공격력 +" + shop.itemAttack;
                    }
                    string goldOrBuy = shop.itemBuyBool ? "구매완료" : $"{shop.price}G";

                    Console.WriteLine("-{0} | {1} | {2} | {3}", shop.itemName, attackShield, shop.itemEtc, goldOrBuy);

                }
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    switch (select)
                    {
                        case 0:
                            isRunning = false;
                            VillageWindow();
                            break;
                        case 1:
                            isRunning = false;
                            PurchaseWindow();
                            break;
                        case 2:
                            isRunning = false;
                            SellWindow();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }


        }
        void PurchaseWindow()                                                                 //구매 페이지
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]\n{0}\n", _player.gold);
                Console.WriteLine("[아이템 목록]");
                int shopItemNum = 0;
                foreach (Shop shop in shopItems)
                {
                    shopItemNum++;
                    string attackShield;                                                //공,방중 보여줄거 선택
                    if (shop.itemAttack == 0)
                    {
                        attackShield = "방어력 +" + shop.itemShield;
                    }
                    else
                    {
                        attackShield = "공격력 +" + shop.itemAttack;
                    }
                    string goldOrBuy = shop.itemBuyBool ? "구매완료" : $"{shop.price}G";

                    Console.WriteLine("-{0} {1} | {2} | {3} | {4}", shopItemNum, shop.itemName, attackShield, shop.itemEtc, goldOrBuy);
                }
                Console.WriteLine("\n0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if(int.TryParse(input,out int select))
                {
                    if (select == 0)
                    {
                        isRunning = false;
                        ShopWindow();
                    }
                    else if (select <= shopItemNum)
                    {
                        if (_player.gold >= shopItems[select - 1].price && !shopItems[select - 1].itemBuyBool)                                                                                      //보유골드와 아이템가격비교 and 구매완료된것인지 체크
                        {
                            _player.gold -= shopItems[select - 1].price;
                            shopItems[select - 1].itemBuyBool = true;
                            items.Add(new Item { itemEquipBool = false, itemName = shopItems[select - 1].itemName, itemAttack = shopItems[select - 1].itemAttack, itemShield = shopItems[select - 1].itemShield, itemEtc = shopItems[select - 1].itemEtc, itemPrice = shopItems[select - 1].price, type = shopItems[select - 1].type });
                            Console.WriteLine("구매를 완료했습니다");
                            Thread.Sleep(1000);
                        }
                        else if (_player.gold < shopItems[select - 1].price)
                        {
                            Console.WriteLine("Gold가 부족합니다");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine("이미 구매를 완료한 아이템입니다");
                            Thread.Sleep(1000);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(1000);

                }
            }
        }
    
        void SellWindow()                                                                     //판매창
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]\n{0}\n", _player.gold);
                Console.WriteLine("[아이템 목록]");
                int itemNum = 0;
                if (items.Count == 0)
                {
                    Console.WriteLine("가진 아이템이 없습니다.");
                }
                else
                {
                    foreach (Item item in items)
                    {
                        itemNum++;
                        string attackShield;                                                //공,방중 보여줄거 선택
                        if (item.itemAttack == 0)
                        {
                            attackShield = "방어력 +" + item.itemShield;
                        }
                        else
                        {
                            attackShield = "공격력 +" + item.itemAttack;
                        }
                        Console.WriteLine("{0} {1} | {2} | {3}",itemNum, item.itemName, attackShield, item.itemEtc);
                    }
                }
                Console.WriteLine("\n0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    if (select == 0)
                    {
                        isRunning = false;
                        ShopWindow();
                    }
                    else if (select <= itemNum)
                    {
                        if (items[select - 1].itemEquipBool == true)                                                //판매한 아이템이 장착상태라면 해제
                        {
                            items[select - 1].itemEquipBool = false;
                        }
                        _player.gold += (int)(items[select - 1].itemPrice * 0.85f);

                        for (int i = 0; i < shopItems.Count; i++)                                            //판ㅁㅐ물품을 상점아이템에서 탐색
                        {
                            if (shopItems[i].itemName == items[select - 1].itemName)
                            {
                                shopItems[i].itemBuyBool = false;
                            }
                        }

                        Console.WriteLine($"{items[select - 1].itemName} 을 판매했습니다");
                        items.RemoveAt(select - 1);
                        Thread.Sleep(1000);

                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(1000);
                }
            }
        }                                                               
        void DungeonWindow()                                                              //던전창
        {
            //if (!isDungeonOpen)
            //{
            //    dungeons.Add(new Dungeon{ name = "쉬운 던전", reqSheild = 5, rewGold = 1000 ,level=Level.easy });
            //    dungeons.Add(new Dungeon { name = "일반 던전", reqSheild = 11, rewGold = 1700, level = Level.normal });
            //    dungeons.Add(new Dungeon { name = "어려운 던전", reqSheild = 17, rewGold = 2500, level = Level.hard });
            //    isDungeonOpen = true;
            //}
            bool isRunning = true;
            while (isRunning)
            {
                Random rand = new Random();
                Console.Clear();
                Console.WriteLine("던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    if (select == 0)
                    {
                        isRunning = false;
                        VillageWindow();
                    }
                    else if (select <= dungeons.Count)
                    {
                        if(_player.hp == 0)
                        {
                            Console.WriteLine("HP가 없습니다!");
                            Thread.Sleep(1000);
                        }
                        else if (_player.shield < dungeons[select - 1].reqSheild)                               //방어력이 권장방어력보다 낮을때 성공실패
                        {
                            int success = rand.Next(1, 11);
                            if (success <= 4)                                                                  //40% 확률
                            {
                                Console.WriteLine("던전공략에 실패했습니다..");
                                Thread.Sleep(1000);
                                int lastHp = _player.hp;
                                _player.hp =(int)(_player.hp *0.5);
                                if(_player.hp < 0)
                                {
                                    _player.hp = 0;
                                }
                            }
                            else
                            {
                                DungeonSuccess(select);
                            }
                        }
                        else
                        {
                            DungeonSuccess(select);
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        Thread.Sleep(1000);
                    }

                }
            }   

        }

        void DungeonSuccess(int select)                                                 //던전성공 창
        {
            bool isRunning = true;
            bool isLevelUp = false;
           
            successCount++;

            if(_player.level == successCount)
            {
                isLevelUp = true;
                _player.level++;
                successCount = 0;
            }

           
            Random rand = new Random();
            int lastHp = _player.hp;
            int lastGold = _player.gold;
            int hpDown = rand.Next(20, 36) - (_player.shield - dungeons[select - 1].reqSheild);
            _player.hp -= hpDown;
            if (_player.hp < 0)
            {
                _player.hp = 0;
            }
            int bonusGold = rand.Next(_player.power, (_player.power * 2) + 1);
            _player.gold = (int)(_player.gold + dungeons[select - 1].rewGold + (dungeons[select - 1].rewGold * (bonusGold * 0.01)));          //플레이어골드 = 플레이어골드 + 클리어골드 + 보너스골드
            while (isRunning)
            {
                Console.Clear();
                if (isLevelUp) 
                {
                    Console.WriteLine($"Level {_player.level}로 레벨업했습니다!");
                    isLevelUp=false;
                    Thread.Sleep(1000);
                }
                
                Console.WriteLine($"던전 클리어\n축하합니다!!\n{dungeons[select - 1].name}을 클리어 하였습니다.\n");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {lastHp} -> {_player.hp}");
                Console.WriteLine($"Gold {lastGold} -> {_player.gold}");
                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int _select))
                {
                    if (_select == 0)
                    {
                        isRunning = false;
                        DungeonWindow();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        Thread.Sleep(1000);
                    }
                }
            }
        }
        void RestWindow()                                                             //휴식창
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine($"500G 를 내면 체력을 회복할 수 있습니다.(보유 골드 : {_player.gold} G\n");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                string? input = Console.ReadLine();
                if (int.TryParse(input, out int select))
                {
                    switch (select)
                    {
                        case 0:
                            isRunning = false;
                            VillageWindow();
                            break;
                        case 1:
                            if (_player.gold >= 500 && _player.hp < 100)
                            {
                                _player.gold -= 500;
                                _player.hp = 100;
                                Console.WriteLine("체력을 회복했습니다");
                                Thread.Sleep(1000);
                            }
                            else if (_player.gold < 500)
                            {
                                Console.WriteLine("골드가 부족합니다");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.WriteLine("hp가 이미 최대입니다");
                                Thread.Sleep(1000);
                            }
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            Thread.Sleep(1000);
                            break;

                    }
                }

            }
        }

        void SaveWindow()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("저장\n현재의 데이터를 저장하시겠습니까?\n");
                Console.WriteLine("1. 예");
                Console.WriteLine("0. 돌아가기");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int _select))
                {
                    switch (_select)
                    {
                        case 0:
                            isRunning = false;
                            VillageWindow();
                            break;
                        case 1:
                            Save(_player, items, shopItems, dungeons);
                            Console.WriteLine("저장되었습니다");
                            Thread.Sleep(1000);
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            Thread.Sleep(1000);
                            break;

                    }
                }
            }
        }

        public void Save(Player player,List<Item> items,List<Shop> shopItems,List<Dungeon> dungeons)
        {
            using (StreamWriter writer = new StreamWriter("save.txt"))
            {
                
                writer.WriteLine(items != null? items.Count : 0);
                writer.WriteLine(_player.level);
                writer.WriteLine(_player.name);
                writer.WriteLine(_player.job);
                writer.WriteLine(_player.power);
                writer.WriteLine(_player.shield);
                writer.WriteLine(_player.hp);
                writer.WriteLine(_player.gold);

                foreach (Item item in items)
                {
                    writer.WriteLine(item.ToString());
                }
                foreach (Shop shop in shopItems)
                {
                    writer.WriteLine(shop.ToString());
                }
                foreach (Dungeon dungeon in dungeons)
                {
                    writer.WriteLine(dungeon.ToString());
                }
            }
        }

        public void Load()
        {
            _player = null;
            items = new List<Item>();
            shopItems = new List<Shop>();
            dungeons = new List<Dungeon>();

            if (!File.Exists("save.txt"))
            {
                Console.WriteLine("저장된 파일이 없습니다.");
                return;
            }

            string[] lines = File.ReadAllLines("save.txt");
            int index = 0;                

            // Player 정보 7줄
            string name = lines[index + 2];
            _player = new Player(name)
            {
                level = int.Parse(lines[index + 1]),
                job = Enum.Parse<Job>(lines[index + 3]),
                power = int.Parse(lines[index + 4]),
                shield = int.Parse(lines[index + 5]),
                hp = int.Parse(lines[index + 6]),
                gold = int.Parse(lines[index + 7])
            };
            index += 8;

            int itemCount = int.Parse(lines[0]);
            int shopCount = 7;
            int dungeonCount = 3;

            if (itemCount != 0)
            {
                for (int i = 0; i < itemCount; i++)
                {
                    items.Add(Item.FromString(lines[index++]));
                }
            }
            for (int i = 0; i < shopCount; i++)
            {
                shopItems.Add(Shop.FromString(lines[index++]));
            }

            for (int i = 0; i < dungeonCount; i++)
            {
                dungeons.Add(Dungeon.FromString(lines[index++]));
            }
        }


    }
}
