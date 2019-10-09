using System;
using System.Collections.Generic;
using System.Threading;

namespace Labb4
{
    internal class Player
    {
        public string Name { get; set; }
        public int MovesLeft { get; set; }
        public int PositionRow { get; set; }
        public int PositionCol { get; set; }

        public Player(string name, int movesLeft, int positionRow, int positionCol)
        {
            Name = name;
            MovesLeft = movesLeft;
            PositionRow = positionRow;
            PositionCol = positionCol;
        }

        public bool CreatePlayerEvent(Box newBox, List<Box> boxList, int index)
        {
            boxList[index] = new Room(Symbols.Room, PositionRow, PositionCol);
            PositionRow = newBox.PositionX;
            PositionCol = newBox.PositionY;
            if (newBox.Item != null)
            {
                PickUpItem(newBox.Item, newBox);
                if (newBox.Item is Potion)
                {
                    HasPotion();
                }
                if (newBox.Item is Trap)
                {
                    FoundTrap();
                }
                return true;
            }
            if (newBox.Monster != null)
            {
                FightMonster(newBox);
                return true;
            }
            if (newBox is Exit)
            {
                Console.WriteLine($"\nYou finished the quest!! \nCongratulations {Name} !! You got {MovesLeft} points.\n\nPress any key to continue... ");
                Console.ReadKey(true);
                return false;
            }
            MovesLeft--;
            return true;
        }

        public bool HasKey()
        {
            foreach (var item in itemsList)
            {
                if (item is Key || item is SuperKey)
                {
                    item.NumberUsageItem -= 1;
                    if (item.NumberUsageItem == 0)
                    {
                        itemsList.Remove(item);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool HasWeapon()
        {
            foreach (var item in itemsList)
            {
                if (item is Bomb || item is Sword)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsWeaponInTheList(string input, out int weaponsIndex)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (input == itemsList[i].GetType().Name.ToString().ToLower())
                {
                    weaponsIndex = i;
                    return true;
                }
            }
            weaponsIndex = -1;
            return false;
        }

        public List<Items> itemsList = new List<Items>();
        internal virtual void PickUpItem(Items item, Box box)
        {
            itemsList.Add(item);
        }

        public void FightMonster(Box newBox)
        {
            bool inputValid = false;
            bool monsterIsDead = false;
            string input = null;
            Console.WriteLine($"\nYou are aproaching a room with a monster. It has {newBox.Monster.Power} health points. " +
                $"\nChoose a weapon to fight the beast!");
            while (!inputValid || !monsterIsDead)
            {
                input = Console.ReadLine().ToLower().Trim();
                int index;
                inputValid = IsWeaponInTheList(input, out index);
                if (inputValid)
                {
                    switch (input)
                    {
                        case "bomb":
                            {
                                newBox.Monster.Power -= 10;
                                MovesLeft -= 5;
                                Console.WriteLine("\nWow!That was a clever choice! \nThe bomb that you used killed the beast!" +
                                    "\nUnfortunately you got injured so you lost 5 health points.");
                                Thread.Sleep(5000);
                                if (newBox.Monster.Power <= 0)
                                {
                                    monsterIsDead = true;
                                }
                                ReduceNumberOfItemUsages(index);
                                inputValid = true;
                            }
                            break;
                        case "sword":
                            {
                                newBox.Monster.Power -= 5;
                                Console.WriteLine($"\nYou managed to damage the monster, so now it has {newBox.Monster.Power} health points." +
                                $"\nBut you are trapped with the beast so you have to choose a weapon and continue until you destroy it!!");
                                if (newBox.Monster.Power <= 0)
                                {
                                    Console.WriteLine("\nThat was a difficult fight but you killed the evil beast! " +
                                        "\nYou got injured so you lost 5 life points, but you can continue your quest!");
                                    Thread.Sleep(5000);
                                    MovesLeft -= 5;
                                    monsterIsDead = true;
                                }
                                ReduceNumberOfItemUsages(index);
                                inputValid = true;
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You have to choose a weapon you have in your legend and kill the monster!");
                }
            }
        }

        private void ReduceNumberOfItemUsages(int index)
        {
            itemsList[index].NumberUsageItem -= 1;
            if (itemsList[index].NumberUsageItem == 0)
            {
                itemsList.Remove(itemsList[index]);
            }
        }

        public bool HasPotion()
        {
            foreach (var item in itemsList)
            {
                if (item is Potion)
                {
                    MovesLeft += 5;
                    Console.WriteLine("\nCongratulations! You found a magic potion! That gives you 5 extra life points to complete your quest!");
                    Thread.Sleep(5000);
                    item.NumberUsageItem -= 1;
                    if (item.NumberUsageItem == 0)
                    {
                        itemsList.Remove(item);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool FoundTrap()
        {
            foreach (var item in itemsList)
            {
                if (item is Trap)
                {
                    MovesLeft -= 10;
                    itemsList.Remove(item);
                    Console.WriteLine("\nOh noooo! You fell in a huge hole!\nThere is a wooden stair on the wall so you can climb back." +
                        "\nThat costs you 10 life points.");
                    Thread.Sleep(5000);
                    return true;
                }
            }
            return false;
        }
    }
}
