using System;
using System.Collections.Generic;

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

        //public void NewGame()
        //{
        //    //Console.Write("Enter your name: ");
        //    //string name = Console.ReadLine();
        //    // TODO: Validera namnet
        //    //players.Add(new Player(name, 100, 3, 3));
        //    bool play = true;
        //    players[players.Count - 1].CreateObjects();
        //    while (play)
        //    {
        //        players[players.Count - 1].Play();
        //        if (players[players.Count - 1].MovesLeft == 0)
        //        {
        //            play = false;
        //        }
        //        // om objektet är Exit --> play = false
        //    }
        //
        //    Console.WriteLine("Does anyone else want to play? (yes/no)");
        //    string answer = Console.ReadLine();
        //    // TODO: Check if answer is yes eller no
        //    if (answer == "yes")
        //    {
        //        NewGame();
        //    }
        //}

        public void ChangePosition(Box newBox, List<Box> boxList, int index)
        {
            //mapWithObjects[PositionRow, PositionCol].Symbol = Symbols.Room;
            boxList[index] = new Room(Symbols.Room, PositionRow, PositionCol);
            PositionRow = newBox.PositionX;
            PositionCol = newBox.PositionY;
            if (newBox.Item != null)
            {
                PickUpItem(newBox.Item, newBox);
            }
            if (newBox.Monster != null)
            {
                FightMonster();
            }
            MovesLeft--;
        }

        //public void ChangePosition(int newRowPosition, int newColPosition, Box[,] mapWithObjects)
        //{
        //    //mapWithObjects[PositionRow, PositionCol].Symbol = Symbols.Room;
        //    mapWithObjects[PositionRow, PositionCol] = new Room(Symbols.Room, PositionRow, PositionCol);
        //    PositionRow = newRowPosition;
        //    PositionCol = newColPosition;
        //    Box currentBox = mapWithObjects[PositionRow, PositionCol];
        //    Items item;
        //    if (currentBox.Item != null)
        //    {
        //        item = currentBox.Item;
        //        PickUpItem(item, currentBox);
        //    }

        //    MovesLeft--;
        //}

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
                    //item.NumberUsageItem -= 1;
                    //if (item.NumberUsageItem == 0)
                    //{
                    //    itemsList.Remove(item);
                    //}
                    return true;
                }
            }
            return false;
        }

        public bool IsWeaponInTheList(string input, out int weaponsIndex)
        {
            //foreach (var item in itemsList)
            //{
            //    Console.WriteLine("item get type" + item.GetType().Name.ToString().ToLower());
            //    Console.ReadKey();
            //    if (input == item.GetType().Name.ToString().ToLower())
            //    {
            //        weaponsIndex = item;
            //        return true;
            //    }
            //}

            for (int i = 0; i < itemsList.Count; i++)
            {
                Console.WriteLine("item get type" + itemsList[i].GetType().Name.ToString().ToLower());
                Console.ReadKey();
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
            //box.Item = null;
        }


        public void FightMonster()
        {
            bool inputValid = false;
            string input = null;
            Console.WriteLine("\nYou are aproaching a room with a monster\nChoose a weapon to fight the beast!");
            while (!inputValid)
            {
                input = Console.ReadLine().ToLower().Trim();
                int index;
                inputValid = IsWeaponInTheList(input, out index);
                //Console.WriteLine("inputvalid is " + inputValid);
                //Console.ReadKey();
                if (inputValid)
                {
                    switch (input)
                    {
                        case "bomb":
                            {
                                Console.WriteLine("using bomb");
                                //minska moves left + cw
                                itemsList[index].NumberUsageItem -= 1;
                                if (itemsList[index].NumberUsageItem == 0)
                                {
                                    itemsList.Remove(itemsList[index]);
                                }
                                inputValid = true;

                            }
                            break;
                        case "sword":
                            {
                                Console.WriteLine("using sword");
                                itemsList[index].NumberUsageItem -= 1;
                                if (itemsList[index].NumberUsageItem == 0)
                                {
                                    itemsList.Remove(itemsList[index]);
                                }
                                inputValid = true;

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
    }
}
