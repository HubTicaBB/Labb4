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

        public bool ChangePosition(Box newBox, List<Box> boxList, int index)
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
                FightMonster(); // skicka newBox som argument, för att kunna ändra boxens monsters styrka i metoden
                return true;
            }
            if (newBox is Exit)
            {
                Console.WriteLine("YOU WON! CONGRATULATIONS!");
                Console.WriteLine($"Your points: {MovesLeft}");
                Console.WriteLine("Press any key ... ");
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

        public void FightMonster() // Här ska vi ha newBox som parameter
        {
            bool inputValid = false;
            string input = null;
            Console.WriteLine("\nYou are aproaching a room with a monster\nChoose a weapon to fight the beast!");
            while (!inputValid)
            {
                input = Console.ReadLine().ToLower().Trim();
                int index;
                inputValid = IsWeaponInTheList(input, out index);
                if (inputValid)
                {
                    switch (input)  // Bryta ner sakerna i båda cases till en metod (uprepande)
                    {
                        case "bomb":
                            {
                                Console.WriteLine("using bomb");  // lägga till text om fight
                                //minska moves left + cw
                                itemsList[index].NumberUsageItem -= 1;
                                if (itemsList[index].NumberUsageItem == 0)
                                {
                                    itemsList.Remove(itemsList[index]);
                                }
                                inputValid = true;
                                // newBox.Monster.MonsterPower -= 10;
                                // om det är 0 eller mindre, ta bort monster                                
                            }
                            break;
                        case "sword":
                            {
                                Console.WriteLine("using sword"); // lägga till text om fight
                                itemsList[index].NumberUsageItem -= 1;
                                if (itemsList[index].NumberUsageItem == 0)
                                {
                                    itemsList.Remove(itemsList[index]);
                                }
                                inputValid = true;
                                // newBox.Monster.MonsterPower -= 2;
                                // om det är 0 eller mindre, ta bort monster

                                // ELLER:

                                // Tvinga spelaren att kämpa igen, tills monster är död, annars kan han inte lämna rummet
                                // dvs. köra case i en loop

                                // Lägga till info att man är hämtat i en "trap" ät det finns ingen utgång innan man besegrar monster
                                // 
                            }
                            break;
                            //default:
                            //    Console.WriteLine("You have to choose a weapon you have in your legend.");
                            //    break;
                    }
                }
                else
                {
                    Console.WriteLine("You have to choose a weapon you have in your legend.");
                }
            }
        }


        public bool HasPotion()
        {
            foreach (var item in itemsList)
            {
                if (item is Potion)
                {
                    MovesLeft += 5;
                    Console.WriteLine("\nCongratulations! You found a magic potion! That gives you 5 extra moves to complete your quest!");
                    Thread.Sleep(1000);
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
                        "\nThat costs you 10 moves.");
                    Thread.Sleep(1500);
                    return true;
                }
            }
            return false;
        }
    }
}
