using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Labb4

{
    internal class Player
    {
        public List<Items> itemsList = new List<Items>();

        public string Name { get; set; }
        public int MovesLeft { get; set; }
        public int PositionRow { get; set; }
        public int PositionCol { get; set; }

        private char[,] map = new char[,]
        {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', 'D','D','D', 'D', '-', '-', '#'},
                {'#', '?', '-', '-','-','-', '-', '?', '-', '#'},
                {'#', '-', 'k', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','M', '-', 'K', '-', '#'},
                {'#', '-', 'k', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '?','-','-', '-', '?', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        public Box[,] mapWithObjects = new Box[10, 10];

        public Player(string name, int movesLeft, int positionRow, int positionCol)
        {
            Name = name;
            MovesLeft = movesLeft;
            PositionRow = positionRow;
            PositionCol = positionCol;
        }

        public void CreateObjects()
        {
            Box box;
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == '#')
                    {
                        box = new Wall(Symbols.Wall);
                    }
                    else if (map[row, col] == '-' || map[row, col] == 'M' || map[row, col] == 'k' || map[row, col] == 'K')
                    {
                        if (map[row, col] == 'M')
                        {
                            Monster monster = new Monster();
                            box = new Room(Symbols.Monster, monster);
                        }
                        else if (map[row, col] == 'k')
                        {
                            Key key = new Key(1);
                            box = new Room(Symbols.Key, key);
                        }
                        else if (map[row, col] == 'K')
                        {
                            SuperKey superKey = new SuperKey(3);
                            box = new Room(Symbols.SuperKey, superKey);
                        }
                        else
                        {
                            box = new Room(Symbols.Room);
                        }
                    }
                    else if (map[row, col] == 'D')
                    {
                        box = new Door(Symbols.Door);
                    }
                    else if (map[row, col] == '?')
                    {
                        Random random = new Random();
                        int roomType = random.Next(1, 8);
                        Thread.Sleep(2000);
                        Items items;
                        switch (roomType)
                        {
                            case 1:
                                box = new Room(Symbols.Surprise);
                                break;
                            case 2:
                                Monster monster = new Monster();
                                box = new Room(Symbols.Surprise, monster);
                                break;
                            case 3:
                                items = new Key(1);
                                box = new Room(Symbols.Surprise, items);
                                break;
                            case 4:
                                items = new Potion(1);
                                box = new Room(Symbols.Surprise, items);
                                break;
                            case 5:
                                items = new Trap(1); // Change it so that it doesn't act as an item
                                box = new Room(Symbols.Surprise, items);
                                break;
                            case 6:
                                items = new Sword(1);
                                box = new Room(Symbols.Surprise, items);
                                break;
                            case 7:
                                items = new Bomb(1);
                                box = new Room(Symbols.Surprise, items);
                                break;
                            default:
                                box = new Room(Symbols.Room);
                                break;
                        }
                    }
                    else
                    {
                        box = new Exit(Symbols.Exit);
                    }
                    mapWithObjects[row, col] = box;
                }
            }
        }

        public void PrintMap()
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    mapWithObjects[PositionRow, PositionCol].Symbol = Symbols.Player;
                    Console.Write((char)mapWithObjects[row, col].Symbol + " ");
                }
                Console.WriteLine();
            }
            Legend();
        }

        public void Legend()
        {
            Console.WriteLine($"\n\nLegend:\n\n{"Player name:",-12} {Name} \n{"Moves left:", -12} {MovesLeft}\n\nItems:");
           
            var doubles = from item in itemsList
                          group item by item.GetType() into nGroup
                          select new { Name = nGroup.First(), Count = nGroup.Count() };

            foreach (var item in doubles)
            {
                Console.WriteLine($"{item.Name,-12} {item.Count}");
            }
        }

        public void Play()
        {
            Console.Clear();
            PrintMap();
            Console.Write("\nCommand: ");
            ConsoleKeyInfo control = Console.ReadKey();

            switch (Char.ToLower(control.KeyChar))
            {
                case 'w':
                    ChangePosition(PositionRow - 1, PositionCol);
                    break;
                case 'a':
                    ChangePosition(PositionRow, PositionCol - 1);
                    break;
                case 's':
                    ChangePosition(PositionRow + 1, PositionCol);
                    break;
                case 'd':
                    ChangePosition(PositionRow, PositionCol + 1);
                    break;
                case 'q':
                    Console.WriteLine("\n\nGame over!");
                    return;
                default:
                    Console.Write("\nInvalid input, try again: ");
                    break;
            }
        }

        public void ChangePosition(int rowPosition, int colPosition)
        {
            if (mapWithObjects[rowPosition, colPosition].IsBoxAvailable())
            {
                DoChange(rowPosition, colPosition);
            }
            else
            {
                if (mapWithObjects[rowPosition, colPosition].GetType() == typeof(Wall))
                {
                    Console.WriteLine($"\nYou reached a wall. Try another command!");
                    Thread.Sleep(1200);
                }
                else if (mapWithObjects[rowPosition, colPosition].GetType() == typeof(Door))
                {
                    bool IsKeyAvailable = false;
                    foreach (var item in itemsList)
                    {
                        if (item.GetType() == typeof(Key) || item.GetType() == typeof(SuperKey))
                        {
                            DoChange(rowPosition, colPosition);
                            item.NumberUsageKey--;
                            if (item.NumberUsageKey == 0)
                            {
                                itemsList.Remove(item);
                            }
                            IsKeyAvailable = true;
                            break;
                        }
                    }
                    if (!IsKeyAvailable)
                    {
                        Console.WriteLine("There is no key. \nYou have to go around and pick up a key.");
                        Thread.Sleep(1200);
                    }
                }
            }
        }

        public void DoChange(int newRowPosition, int newColPosition)
        {
            mapWithObjects[PositionRow, PositionCol].Symbol = Symbols.Room;            
            PositionRow = newRowPosition;
            PositionCol = newColPosition;
            Box currentBox = mapWithObjects[PositionRow, PositionCol];
            Items item;
            if (currentBox.Item != null)
            {
                item = currentBox.Item;
                PickUpItem(item, currentBox);
            }
            MovesLeft--;
        }

        internal virtual void PickUpItem(Items item, Box box)
        {
            itemsList.Add(item);
            box.Item = null;
        }
    }
}
