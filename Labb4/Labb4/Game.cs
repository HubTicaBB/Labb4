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
                {'#', '-', '#', '?','-','-', '#', 'D', 'E', '#'},
                {'#', 'b', '#', 'D','k','s', '#', 'D', 'M', '#'},
                {'#', '-', 'k', '-','-','k', '?', '-', '-', '#'},
                {'#', 'M', 'D', '-','-','-', '-', 'b', 's', '#'},
                {'#', '-', 'k', '-','-','-', 'K', '-', 'k', '#'},
                {'#', 'D', 'D', '-','b','-', '-', '#', '#', '#'},
                {'#', 'b', 'p', '?','-','D', 'M', '?', '-', '#'},
                {'#', 'k', '-', '-','-','?', '-', '-', '-', '#'},
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

        
        internal void NewGame()
        {
            CreateObjects();
            AddPlayer();            

            Console.WriteLine("\nPress any key to start the game...");
            Console.ReadKey();
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
            }

            Console.Clear();
            Console.WriteLine("Does anyone else want to play? (yes/no)");
            bool invalidAnswer = true;
            while (invalidAnswer)
            {
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "yes" || answer == "y")
                {
                    NewGame();
                    invalidAnswer = false;
                }
                else if (answer == "no" || answer == "n")
                {
                    Console.WriteLine("\nGAME OVER!");
                    PrintHighscores();
                    invalidAnswer = false;
                }
                else
                {
                    Console.Write("Invalid! Write yes or no: ");
                }
            }
        }

        Player player;
        private void AddPlayer()
        {
            Console.Write($"PLAYER {players.Count + 1}\nEnter your name: ");
            string name = Console.ReadLine();
            //TODO: Validera namnet
            player = new Player(name, 100, 3, 3);
            players.Add(player);
        }

        private void PrintHighscores()
        {
            var highscores = from player in players
                             orderby player.MovesLeft descending
                             select player;

            Console.WriteLine($"\n---------------------------------\n| {"RANK",-4} |    {"PLAYER",-10}|  {"SCORE"}  |\n---------------------------------");
            int rank = 1;
            foreach (var player in highscores)
            {
                Console.WriteLine($"|   {rank,-2} | {player.Name,-12} | {player.MovesLeft,4}    |");
                rank++;
            }
            Console.WriteLine($"---------------------------------");
        }       

        private void CreateObjects()
        {

            Box box;
            Items item;
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == '#')
                    {
                        box = new Wall(Symbols.Wall, row, col);
                    }
                    else if (map[row, col] == 'D')
                    {
                        box = new Door(Symbols.Door, row, col);
                    }
                    else if (map[row, col] == 'M')
                    {
                        Monster monster = new Monster();
                        box = new Room(Symbols.Monster, monster, row, col);
                    }
                    else if (map[row, col] == 'k')
                    {
                        item = new Key(1);
                        box = new Room(Symbols.Key, item, row, col);
                    }
                    else if (map[row, col] == 'K')
                    {
                        item = new SuperKey(3);
                        box = new Room(Symbols.SuperKey, item, row, col);
                    }
                    else if (map[row, col] == 's')
                    {
                        item = new Sword(10);
                        box = new Room(Symbols.Sword, item, row, col);
                    }
                    else if (map[row, col] == 'b')
                    {
                        item = new Bomb(1);
                        box = new Room(Symbols.Bomb, item, row, col);
                    }
                    else if (map[row, col] == 'p')
                    {
                        item = new Potion(1);
                        box = new Room(Symbols.Potion, item, row, col);
                    }
                    else if (map[row, col] == 't')
                    {
                        item = new Trap(1);
                        box = new Room(Symbols.Trap, item, row, col);
                    }
                    else if (map[row, col] == '-')
                    {
                        box = new Room(Symbols.Room, row, col);
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
                                box = new Room(Symbols.Surprise, row, col);
                                break;
                            case 2:
                                Monster monster = new Monster();
                                box = new Room(Symbols.Surprise, monster, row, col);
                                break;
                            case 3:
                                item = new Key(1);
                                box = new Room(Symbols.Surprise, item, row, col);
                                break;
                            case 4:
                                item = new Potion(1);
                                box = new Room(Symbols.Surprise, item, row, col);
                                break;
                            case 5:
                                item = new Trap(1);
                                box = new Room(Symbols.Surprise, item, row, col);
                                break;
                            case 6:
                                item = new Sword(10);
                                box = new Room(Symbols.Surprise, item, row, col);
                                break;
                            case 7:
                                item = new Bomb(1);
                                box = new Room(Symbols.Surprise, item, row, col);
                                break;
                            default:
                                box = new Room(Symbols.Room, row, col);
                                break;
                        }
                    }
                    else
                    {
                        box = new Exit(Symbols.Exit, row, col);
                    }
                    boxList.Add(box);
                }
            }
        }

        private void PrintMap()
        {
            foreach (var box in boxList)
            {
                if (box.PositionX == player.PositionRow && box.PositionY == player.PositionCol)
                {
                    box.Symbol = Symbols.Player;
                }
                if (box.PositionY == 0)
                {
                    Console.WriteLine();
                }
                if ((box.Symbol == Symbols.Player) ||
                    (box is Wall) ||
                    (box.PositionX == player.PositionRow && (box.PositionY == player.PositionCol - 1 || box.PositionY == player.PositionCol + 1)) ||
                    (box.PositionY == player.PositionCol && (box.PositionX == player.PositionRow - 1 || box.PositionX == player.PositionRow + 1)))
                {
                    Console.Write($"{(char)box.Symbol} ");
                }
                else
                {
                    Console.Write("  ");
                }
            }
            Legend();
        }
        //public void PrintMap()
        //{
        //    for (int row = 0; row < map.GetLength(0); row++)
        //    {
        //        for (int col = 0; col < map.GetLength(1); col++)
        //        {
        //            for (int i = 0; i < boxList.Count; i++)
        //            {
        //                if (boxList[i].PositionX == row && boxList[i].PositionY == col)
        //                {
        //                    if (boxList[i].PositionX == players[players.Count - 1].PositionRow && boxList[i].PositionY == players[players.Count - 1].PositionCol)
        //                    {
        //                        boxList[i].Symbol = Symbols.Player;
        //                    }
        //                    Console.Write((char)boxList[i].Symbol + " ");
        //                }
        //            }
        //            //mapWithObjects[players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol].Symbol = Symbols.Player;
        //            //Console.Write((char)mapWithObjects[row, col].Symbol + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //    Legend();
        //}

        public void Legend()
        {
            Console.WriteLine($"\n\nLegend:\n\n{"Player name:",-12} {players[players.Count - 1].Name} \n{"Life points:",-12} {players[players.Count - 1].MovesLeft}\n\nItems:");

            var doubles = from item in players[players.Count - 1].itemsList
                          group item by item.GetType() into nGroup
                          select new { Name = nGroup.First(), Count = nGroup.Count() };

            foreach (var item in doubles)
            {
                Console.WriteLine($"{item.Name,-12} {item.Count}");
            }
        }
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
                case 'a':
                    return (Move(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol - 1));
                case 's':
                    return (Move(players[players.Count - 1].PositionRow + 1, players[players.Count - 1].PositionCol));
                case 'd':
                    return (Move(players[players.Count - 1].PositionRow, players[players.Count - 1].PositionCol + 1));
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
        public void InstructionsForUser(string name)
        {
            Console.Clear();
            Console.WriteLine($"Hello {name}! Welcome to the dungeon crawler!" +
                $"\n\nThis is a maze game where you (@) can move around using the following keys on the keyboard: " +
                $"\na (move left), d (move right), w (move up) and s (move down). " +
                $"\nEvery move costs you a life point and your goal is to have as many as possible when you find the Exit(E)." +
                $"\n\nDuring your adventure you can pick up a one - use keys(k) that can help you open Doors(D) to rooms" +
                $"\nwhere you can see the objects, empty rooms (-) or surprises(?)." +
                $"\n\nThere is also a special key(K) that you can use 3 times!" +
                $"\nThe maze contains weapons: swords(s) and bombs(b) that you can use in order to kill evil Monsters(M)." +
                $"\nFinding potions(p) will increase your life points, but watch out for the traps(t)!" +
                $"\n\nGood luck!!");
        }
    }
}
