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
                {'#', '-', '-', 'x','-','-', '-', '-', '-', '#'},
                {'#', '-', 'k', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','M', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '?','-','-', '-', '-', '-', '#'},
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
                        int roomType = random.Next(1, 6);
                        switch (roomType)
                        {
                            case 1:
                                box = new Room(Symbols.Room);
                                break;
                            case 2:
                                Monster monster = new Monster();
                                box = new Room(Symbols.Monster, monster);
                                break;
                            case 3:
                                Key key = new Key();    // TODO: change constructor, argument: int antalGånger
                                box = new Room(Symbols.Key, key);
                                break;
                            case 4:
                                box = new Room(Symbols.Room);   // change this
                                // potion room: lägga till klassen, lägga till konstruktor till room
                                break;
                            case 5:
                                box = new Room(Symbols.Room); // and this
                                // trap room: samma som ovan
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

        public static void StartMap(Box[,] map, int startPositionRow, int startPositionCol)
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
                    ChangePosition(positionRow - 1, positionCol);                   
                    break;
                case 'a':
                    ChangePosition(positionRow, positionCol - 1);
                    break;
                case 's':
                    ChangePosition(positionRow + 1, positionCol);
                    break;
                case 'd':
                    ChangePosition(positionRow, positionCol + 1);
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
                    foreach (var item in items)
                    {
                        if (item.GetType() == typeof(Key))
                        {
                            DoChange(rowPosition, colPosition);
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

        public void DoChange(int rowPosition, int colPosition)
        {
            mapWithObjects[rowPosition, colPosition].Symbol = Symbols.Room;
            positionRow = rowPosition;
            positionCol = colPosition;
        }
    }
}
