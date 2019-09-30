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
        int startPositionRow = 3;
        int startPositionCol = 3;

        char[,] map = new char[,]
        {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', 'D','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', 'k', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','M', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
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
                        box = new Wall('#');
                    }
                    else if (map[row, col] == '-' || map[row, col] == 'M' || map[row, col] == 'k')
                    {
                        if (map[row, col] == 'M')
                        {
                            Monster monster = new Monster();
                            box = new Room('M', monster);
                        }
                        else if (map[row, col] == 'k')
                        {
                            Key key = new Key();
                            box = new Room('k', key);
                        }
                        else
                        {
                            box = new Room('-');
                        }

                    }
                    else if (map[row, col] == 'D')
                    {
                        box = new Door('D');
                    }
                    else
                    {
                        box = new Exit('E');
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
                    map[startPositionRow, startPositionCol].Symbol = '@';
                    Console.Write(map[row, col].Symbol + " ");
                }
                Console.WriteLine();
            }
        }
        public void Play()
        {
            Console.Clear();
            StartMap(mapWithObjects, startPositionRow, startPositionCol);
            Console.WriteLine("Instructions");
            Console.Write("Control: ");
            ConsoleKeyInfo control = Console.ReadKey();


            switch (Char.ToLower(control.KeyChar))
            {
                case 'w':
                    ChangePosition(startPositionRow - 1, startPositionCol);
                    //if (mapWithObjects[startPositionRow - 1, startPositionCol].IsBoxAvailable())
                    //{
                    //    mapWithObjects[startPositionRow, startPositionCol].Symbol = '-';
                    //    startPositionRow--;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("\nYou reached the wall of the dungeon. Try another command!");
                    //    Thread.Sleep(2000);
                    //    return;
                    //}
                    break;
                case 'a':
                    mapWithObjects[startPositionRow, startPositionCol].Symbol = '-';
                    startPositionCol--;
                    break;
                case 's':
                    mapWithObjects[startPositionRow, startPositionCol].Symbol = '-';
                    startPositionRow++;
                    break;
                case 'd':
                    mapWithObjects[startPositionRow, startPositionCol].Symbol = '-';
                    startPositionCol++;
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
                mapWithObjects[rowPosition, colPosition].Symbol = '-';
                startPositionRow--;
            }
            else

            {
                if (mapWithObjects[rowPosition, colPosition].GetType() == typeof(Wall))
                {
                    Console.WriteLine($"\nYou reached a wall. Try another command!");
                    Thread.Sleep(2000);
                }

                else if (mapWithObjects[rowPosition, colPosition].GetType() == typeof(Door))
                {
                    bool IsKeyAvailable = false;
                    foreach (var item in items)
                    {

                        if (item.GetType() == typeof(Key))
                        {
                            mapWithObjects[rowPosition, colPosition].Symbol = '-';
                            startPositionRow--;
                            IsKeyAvailable = true;
                            break;
                        }
                    }
                    if (!IsKeyAvailable)
                    {
                        Console.WriteLine("There is no key. \nYou have to go around and pick up a key.");
                        Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}
