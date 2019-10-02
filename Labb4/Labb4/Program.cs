using System;
using System.Collections.Generic;

namespace Labb4
{
    class Program
    {        
        static void Main(string[] args)
        {
            Game game = new Game();
            game.players.Add(new Player("Andreea", 100, 3, 3));
            game.players.Add(new Player("Tijana", 100, 3, 3));
            game.players.Add(new Player("Pontus", 100, 3, 3));
            NewGame(game.players);
        }

        public static void NewGame(List<Player> players)
        {
            bool play = true;
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            // TODO: Validera namnet
            players.Add(new Player(name, 100, 3, 3));
            players[players.Count - 1].CreateObjects();
            while (play)
            {
                players[players.Count - 1].Play();
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
                NewGame(players);
            }
        }
    }
}
