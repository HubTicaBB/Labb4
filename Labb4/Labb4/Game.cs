using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Labb4
{
    internal class Game
    {
        public List<Player> players = new List<Player>();
        public List<Box> boxList = new List<Box>();


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

        //public void CreatePlayer()
        //{
        //    Console.Write("Enter your name: ");
        //    string name = Console.ReadLine();
        //    Player newPlayer = new Player(name, 100, 3, 3);
        //    players.Add(newPlayer);
        //}

        public void NewGame()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            //TODO: Validera namnet
            players.Add(new Player(name, 100, 3, 3));
            bool play = true;
            CreateObjects();
            while (play)
            {
                Play();
                if (players[players.Count - 1].MovesLeft == 0)
                {
                    play = false;
                }
                // om objektet är Exit --> play = false
            }

            Console.WriteLine("Does anyone else want to play? (yes/no)");
            string answer = Console.ReadLine();
            // TODO: Check if answer is yes eller no
            if (answer == "yes")
            {
                NewGame();
            }
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
                        box = new Wall(Symbols.Wall, row, col);
                        boxList.Add(box);

                    }
                    else if (map[row, col] == 'D')
                    {
                        box = new Door(Symbols.Door, row, col);
                        boxList.Add(box);
                    }
                    //else if (map[row, col] == '-' || map[row, col] == 'M' || map[row, col] == 'k' || map[row, col] == 'K')
                    //{
                    else if (map[row, col] == 'M')
                    {
                        Monster monster = new Monster();
                        box = new Room(Symbols.Monster, monster, row, col);
                        boxList.Add(box);
                    }
                    else if (map[row, col] == 'k')
                    {
                        Key key = new Key(1);
                        box = new Room(Symbols.Key, key, row, col);
                        boxList.Add(box);
                    }
                    else if (map[row, col] == 'K')
                    {
                        SuperKey superKey = new SuperKey(3);
                        box = new Room(Symbols.SuperKey, superKey, row, col);
                        boxList.Add(box);
                    }
                    else if (map[row, col] == '-')
                    {
                        box = new Room(Symbols.Room, row, col);
                        boxList.Add(box);
                    }
                    //}                    
                    else if (map[row, col] == '?')
                    {
                        Random random = new Random();
                        int roomType = random.Next(1, 8);
                        Thread.Sleep(2000);
                        Items items;
                        switch (roomType)
                        {
                            case 1:
                                box = new Room(Symbols.Surprise, row, col);
                                boxList.Add(box);
                                break;
                            case 2:
                                Monster monster = new Monster();
                                box = new Room(Symbols.Surprise, monster, row, col);
                                boxList.Add(box);
                                break;
                            case 3:
                                items = new Key(1);
                                box = new Room(Symbols.Surprise, items, row, col);
                                boxList.Add(box);
                                break;
                            case 4:
                                items = new Potion(1);
                                box = new Room(Symbols.Surprise, items, row, col);
                                boxList.Add(box);
                                break;
                            case 5:
                                items = new Trap(1); // Change it so that it doesn't act as an item
                                box = new Room(Symbols.Surprise, items, row, col);
                                boxList.Add(box);
                                break;
                            case 6:
                                items = new Sword(1);
                                box = new Room(Symbols.Surprise, items, row, col);
                                boxList.Add(box);
                                break;
                            case 7:
                                items = new Bomb(1);
                                box = new Room(Symbols.Surprise, items, row, col);
                                boxList.Add(box);
                                break;
                            default:
                                box = new Room(Symbols.Room, row, col);
                                boxList.Add(box);
                                break;
                        }
                    }
                    else
                    {
                        box = new Exit(Symbols.Exit, row, col);
                        boxList.Add(box);
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
                    for (int i = 0; i < boxList.Count; i++)
                    {
                        if (boxList[i].PositionX == row && boxList[i].PositionY == col)
                        {
                            Console.Write((char)boxList[i].Symbol + " ");
                        }
                    }
                    //mapWithObjects[players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol].Symbol = Symbols.Player;
                    //Console.Write((char)mapWithObjects[row, col].Symbol + " ");
                }
                Console.WriteLine();
            }
            Legend();
        }

        public void Legend()
        {
            Console.WriteLine($"\n\nLegend:\n\n{"Player name:",-12} {players[players.Count - 1].Name} \n{"Moves left:",-12} {players[players.Count - 1].MovesLeft}\n\nItems:");

            var doubles = from item in players[players.Count - 1].itemsList
                          group item by item.GetType() into nGroup
                          select new { Name = nGroup.First(), Count = nGroup.Count() };

            foreach (var item in doubles)
            {
                Console.WriteLine($"{item.Name,-12} {item.Count}");
            }
        }

        public bool CheckIfKeyIsAvailable()
        {
            foreach (var item in players[players.Count - 1].itemsList)
            {
                if (item.GetType() == typeof(Key) || item.GetType() == typeof(SuperKey))
                {
                    return true;
                }
            }
            return false;
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
                    CheckIfPositionisAvailable(players[players.Count - 1].PositionRow - 1, players[players.Count - 1].PositionCol);
                    break;
                case 'a':
                    CheckIfPositionisAvailable(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol - 1);
                    break;
                case 's':
                    CheckIfPositionisAvailable(players[players.Count - 1].PositionRow + 1, players[players.Count - 1].PositionCol);
                    break;
                case 'd':
                    CheckIfPositionisAvailable(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol + 1);
                    break;
                case 'q':
                    Console.WriteLine("\n\nGame over!");
                    return;
                default:
                    Console.Write("\nInvalid input, try again: ");
                    break;
            }
        }

        public void CheckIfPositionisAvailable(int rowPosition, int colPosition)
        {
            if (mapWithObjects[rowPosition, colPosition].IsBoxAvailable(players[players.Count - 1]))
            {
                players[players.Count - 1].ChangePosition(rowPosition, colPosition, mapWithObjects);
            }
        }
    }
}
