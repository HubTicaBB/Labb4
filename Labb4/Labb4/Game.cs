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
            {'#', 'b', '#', 'D','k','s', '#', '-', 't', '#'},
            {'#', '-', 'k', '-','-','k', '?', 'M', '-', '#'},
            {'#', 'M', 'D', '-','-','-', '-', 'b', 's', '#'},
            {'#', '-', 'k', '-','-','-', 'K', '-', 'k', '#'},
            {'#', 'D', 'D', '-','b','-', '-', '#', '#', '#'},
            {'#', 'b', 'p', '?','-','D', 'M', '?', '-', '#'},
            {'#', 'k', '-', '-','-','?', '-', '-', '-', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        internal void NewGame()
        {
            AddPlayer();
            InstructionsForUser(player.Name);
            CreateObjects();

            Console.WriteLine("\nPress any key to start the game...");
            Console.ReadKey();
            bool play = true;
            while (play)
            {
                play = Play();
                if (player.MovesLeft == 0)
                {
                    play = false;
                    Console.WriteLine("\nYou don't have any life points left, so your quest is over. Maybe you can try again later!" +
                        "\nPress any key to continue...");
                    Console.ReadKey();
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
                    Console.Write("\nInvalid! Write yes or no: ");
                }
            }
        }

        Player player;
        private void AddPlayer()
        {
            Console.Write($"PLAYER {players.Count + 1}\nEnter your name: ");
            string name = Console.ReadLine();
            player = new Player(name, 100, 4, 4);
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
            UserInformation();
        }

        public void Legend()
        {
            Console.Clear();
            Console.WriteLine($"\n----------------------------\nLegend:" +
                              $"\n----------------------------" +
                              $"\n {(char)Symbols.Player,-2} Your position" +
                              $"\n {(char)Symbols.Wall,-2} Wall" +
                              $"\n {(char)Symbols.Door,-2} Door" +
                              $"\n {(char)Symbols.Monster,-2} Monster" +
                              $"\n {(char)Symbols.Key,-2} Key" +
                              $"\n {(char)Symbols.SuperKey,-2} Super Key" +
                              $"\n {(char)Symbols.Bomb,-2} Bomb" +
                              $"\n {(char)Symbols.Sword,-2} Sword" +
                              $"\n {(char)Symbols.Potion,-2} Potion" +
                              $"\n {(char)Symbols.Trap,-2} Trap" +
                              $"\n {(char)Symbols.Surprise,-2} Surprise" +
                              $"\n----------------------------" +
                              $"\n\nPress any key to continue...");
            Console.ReadKey(true);
        }
        public void UserInformation()
        {
            Console.Write($"\n-------------------" +
                          $"\n{"Player name:",-12} {player.Name} " +
                          $"\n{"Life points:",-12} {player.MovesLeft}" +
                          $"\n-------------------" +
                          $"\nItems: ");
            if (player.itemsList.Count == 0)
            {
                Console.Write(0);
            }
            var doubles = from item in player.itemsList
                          group item by item.GetType() into nGroup
                          select new { Name = nGroup.First(), Count = nGroup.Count() };
            foreach (var item in doubles)
            {
                Console.Write($"\n{item.Name,-11}{item.Count}");
            }
            Console.WriteLine("\n-------------------");
        }

        public bool Play()
        {
            Console.Clear();
            PrintMap();
            Console.Write("\n\nCommand: ");
            ConsoleKeyInfo control = Console.ReadKey();
            switch (Char.ToLower(control.KeyChar))
            {
                case 'w':
                    return (Move(player.PositionRow - 1, player.PositionCol));
                case 'a':
                    return (Move(player.PositionRow, player.PositionCol - 1));
                case 's':
                    return (Move(player.PositionRow + 1, player.PositionCol));
                case 'd':
                    return (Move(player.PositionRow, player.PositionCol + 1));
                case 'l':
                    Legend();
                    return true;
                case 'q':
                    player.MovesLeft = 0;
                    return false;
                default:
                    Console.Write("\nInvalid input, try again!");
                    Thread.Sleep(250);
                    return true;
            }
        }

        public bool Move(int newPositionRow, int newPositionCol)
        {
            int index = 0;
            for (int i = 0; i < boxList.Count; i++)
            {
                if (boxList[i].PositionX == player.PositionRow && boxList[i].PositionY == player.PositionCol)
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
            if (nextBox.IsBoxAvailable(player))
            {
                return (player.ChangePosition(nextBox, boxList, index));
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
                $"\nDuring the game you can press q at any time to exit game, but you lose all you life points." +
                $"\n\nGood luck!!");
        }
    }
}
