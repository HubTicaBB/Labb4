using System;
using System.Collections.Generic;

namespace Labb4
{
    class Program
    {        
        static void Main(string[] args)
        {
            Game game = new Game();
            game.NewGame();

            Console.WriteLine("\n\n\n\nPress any key to close the console...");
            Console.ReadKey(true);
        }
    }
}
