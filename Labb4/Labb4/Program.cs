﻿using System;
using System.Collections.Generic;

namespace Labb4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            players.Add(new Player("Andreea"));
            players.Add(new Player("Tijana"));
            players.Add(new Player("Pontus"));

            bool play = true;
            while (play)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                // TODO: Validera namnet
                players.Add(new Player(name));
                players[players.Count - 1].Play();

                Console.WriteLine("Does anyone else want to play? (yes/no)");
                string answer = Console.ReadLine();
                // TODO: Check if answer is yes eller no
                if (answer == "no")
                {
                    play = false;
                }
            }
        }
    }
}
