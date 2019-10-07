using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb4
{
    internal class Game
    {
        public List<Player> players = new List<Player>();
        public List<Box> boxList = new List<Box>();


        private char[,] map = new char[,]
        {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '-', '-', '-','-','-', '-', '-', 'E', '#'},
                {'#', '-', 's', 'M','D','D', 'D', '-', '-', '#'},
                {'#', '-', 'b', 'M','-','-', '-', '?', '-', '#'},
                {'#', '-', 'p', 's','-','-', '-', 'b', '-', '#'},
                {'#', '-', '-', '-','b','M', '-', 'K', '-', '#'},
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
            players.Add(new Player(name, 100, 2, 1));
            CreateObjects();
            bool play = true;
            while (play)
            {
                play = Play();
                if (players[players.Count - 1].MovesLeft == 0)
                {
                    play = false;
                }
                if (!play)
                {
                    boxList.Clear();
                }
                // om objektet är Exit --> play = false
            }

            Console.Clear();
            Console.WriteLine("Does anyone else want to play? (yes/no)");
            string answer = Console.ReadLine();
            // TODO: Check if answer is yes eller no
            if (answer == "yes")
            {
                NewGame();
            }
            else
            {
                Console.WriteLine("Good bye");
                // TODO: skriv ut highscores, sort objects by movesLeft descending with LINQ
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.Name} - {player.MovesLeft}");
                }
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
                    else if (map[row, col] == 's')
                    {
                        Items items = new Sword(10);
                        box = new Room(Symbols.Sword, items, row, col);
                        boxList.Add(box);

                    }
                    else if (map[row, col] == 'b')
                    {
                        Items items = new Bomb(1);
                        box = new Room(Symbols.Bomb, items, row, col);
                        boxList.Add(box);
                    }
                    else if (map[row, col] == 'p')
                    {
                        Items items = new Potion(1);
                        box = new Room(Symbols.Potion, items, row, col);
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
                        //Thread.Sleep(2000);
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
                                items = new Sword(10);
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
                            if (boxList[i].PositionX == players[players.Count - 1].PositionRow && boxList[i].PositionY == players[players.Count - 1].PositionCol)
                            {
                                boxList[i].Symbol = Symbols.Player;
                            }
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

        //public bool CheckIfKeyIsAvailable()
        //{
        //    foreach (var item in players[players.Count - 1].itemsList)
        //    {
        //        if (item.GetType() == typeof(Key) || item.GetType() == typeof(SuperKey))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public bool Play()
        {
            Console.Clear();
            PrintMap();
            Console.Write("\nCommand: ");
            ConsoleKeyInfo control = Console.ReadKey();

            switch (Char.ToLower(control.KeyChar))
            {
                case 'w':
                    return (Move(players[players.Count - 1].PositionRow - 1, players[players.Count - 1].PositionCol));
                //CheckIfPositionisAvailable(players[players.Count - 1].PositionRow - 1, players[players.Count - 1].PositionCol);
                //return true;
                case 'a':
                    return (Move(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol - 1));
                //CheckIfPositionisAvailable(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol - 1);
                case 's':
                    return (Move(players[players.Count - 1].PositionRow + 1, players[players.Count - 1].PositionCol));
                //CheckIfPositionisAvailable(players[players.Count - 1].PositionRow + 1, players[players.Count - 1].PositionCol);
                case 'd':
                    return (Move(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol + 1));
                //CheckIfPositionisAvailable(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol + 1);
                case 'q':
                    Console.WriteLine("\n\nGame over, you have lost all your points!");
                    players[players.Count - 1].MovesLeft = 0;
                    return false;
                default:
                    Console.Write("\nInvalid input, try again: ");
                    return true;
            }
        }

        public bool Move(int newPositionRow, int newPositionCol)
        {
            int index = 0;
            for (int i = 0; i < boxList.Count; i++)
            {
                if (boxList[i].PositionX == players[players.Count - 1].PositionRow && boxList[i].PositionY == players[players.Count - 1].PositionCol)
                {
                    index = i;
                    break;
                }
            }
            foreach (var box in boxList)
            {
                if (box.PositionX == newPositionRow && box.PositionY == newPositionCol)
                {
                    return (CheckIfPositionIsAvailable(box, boxList, index));
                }
            }
            return true;
        }

        public bool CheckIfPositionIsAvailable(Box nextBox, List<Box> boxList, int index)
        {
            if (nextBox.IsBoxAvailable(players[players.Count - 1]))
            {
                return (players[players.Count - 1].ChangePosition(nextBox, boxList, index));
            }
            return true;
        }

        //public void CheckIfPositionisAvailable(int rowPosition, int colPosition)
        //{

        //    if (mapWithObjects[rowPosition, colPosition].IsBoxAvailable(players[players.Count - 1]))
        //    {
        //        players[players.Count - 1].ChangePosition(rowPosition, colPosition, mapWithObjects);
        //    }
        //}
    }
}
