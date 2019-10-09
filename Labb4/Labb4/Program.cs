using System;

//Highscores
//--------------------------------
//| RANK |    PLAYER   |  SCORE  |
//--------------------------------
//|   1  | Tijana      |   87    |
//|   2  | Andreea     |   83    |
//|   3  | Magnus      |   77    |
//--------------------------------

// Labb 4: Andreea Nenciu och Tijana Ilic


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
