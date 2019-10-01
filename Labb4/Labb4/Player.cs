using System;
using System.Collections.Generic;
using System.Threading;

namespace Labb4

{
    internal class Player
    {
        public List<Key> items = new List<Key>();
        public string Name { get; set; }
        public int Score { get; set; }
        int positionRow = 3;
        int positionCol = 3;

        char[,] map = new char[,]
        {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', 'D','x','-', '-', '-', '-', '#'},
                {'#', '?', '-', 'x','-','-', '-', '?', '-', '#'},
                {'#', '-', 'k', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','M', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '?','-','-', '-', '?', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
        };

        Box[,] mapWithObjects = new Box[10, 10];

        public Player(string name, int score = 100)
        {
            this.Name = name;
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
                    else if (map[row, col] == '-' || map[row, col] == 'M' || map[row, col] == 'k')
                    {
                        if (map[row, col] == 'M')
                        {
                            Monster monster = new Monster();
                            box = new Room(Symbols.Monster, monster);
                        }
                        else if (map[row, col] == 'k')
                        {
                            Key key = new Key();
                            box = new Room(Symbols.Key, key);
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
                        int roomType = random.Next(1, 7);
                        Console.WriteLine("room type is " + roomType);
                        Console.ReadKey(true);
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
                                Key key = new Key();    // TODO: change constructor, argument: int antalGånger
                                box = new Room(Symbols.Surprise, key);
                                break;
                            case 4:
                                items = new Potion();
                                box = new Room(Symbols.Surprise, items);
                                break;
                            case 5:
                                items = new Trap();
                                box = new Room(Symbols.Surprise, items);
                                break;
                            case 6:
                                items = new Sword();
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

        public void StartMap(Box[,] map, int startPositionRow, int startPositionCol)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    map[startPositionRow, startPositionCol].Symbol = Symbols.Player;
                    Console.Write((char)map[row, col].Symbol + " ");
                }
                Console.WriteLine();
            }
        }

        public void Play()
        {
            Console.Clear();
            StartMap(mapWithObjects, positionRow, positionCol);
            Console.WriteLine("Instructions");
            Console.Write("Control: ");
            ConsoleKeyInfo control = Console.ReadKey();

            switch (Char.ToLower(control.KeyChar))
            {
                case 'w':
                    ChangePosition(positionRow - 1, positionCol, positionRow, positionCol);
                    break;
                case 'a':
                    ChangePosition(positionRow, positionCol - 1, positionRow, positionCol);
                    break;
                case 's':
                    ChangePosition(positionRow + 1, positionCol, positionRow, positionCol);
                    break;
                case 'd':
                    ChangePosition(positionRow, positionCol + 1, positionRow, positionCol);
                    break;
                case 'q':
                    Console.WriteLine("\n\nGame over!");
                    return;
                default:
                    Console.Write("\nInvalid input, try again: ");
                    break;
            }
        }

        public void ChangePosition(int rowPosition, int colPosition, int oldRow, int oldCol)
        {
            if (mapWithObjects[rowPosition, colPosition].IsBoxAvailable())
            {
                DoChange(rowPosition, colPosition, oldRow, oldCol);
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
                    foreach (var item in items)
                    {
                        if (item.GetType() == typeof(Key))
                        {
                            DoChange(rowPosition, colPosition, oldRow, oldCol);
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

        public void DoChange(int rowPosition, int colPosition, int oldRow, int oldCol)
        {
            mapWithObjects[oldRow, oldCol].Symbol = Symbols.Room;

            positionRow = rowPosition;
            positionCol = colPosition;
        }
    }
}
