using System;

namespace Labb4

{
    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        int startPositionRow = 3;
        int startPositionCol = 3;

        char[,] mapLayout = new char[,]

        {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', '-', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
        };

        public Player(string name, int score = 100)
        {
            this.Name = name;
        }

        public static void StartMap(char[,] map, int startPositionRow, int startPositionCol)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {

                    if ((row == startPositionRow) && (col == startPositionCol))
                        map[row, col] = '@';
                }
            }
            PrintMap(map);
        }
        public static void PrintMap(char[,] map)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    Console.Write(map[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Play()
        {
            StartMap(mapLayout, startPositionRow, startPositionCol);
            Console.WriteLine("play");
        }
    }
}
