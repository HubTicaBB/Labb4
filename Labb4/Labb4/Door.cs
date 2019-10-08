using System;
using System.Threading;

namespace Labb4
{
    class Door : Box
    {
        public Door(Symbols symbol, int positionX, int positionY) : base(symbol, positionX, positionY)
        {
            Symbol = symbol;
        }

        public override bool IsBoxAvailable(Player player)
        {
            if (player.HasKey())
            {
                return true;
            }
            else
            {
                Console.WriteLine("\nThere is no key. \nYou have to go around and pick up a key.");
                Thread.Sleep(1500);
                return false;
            }
        }
    }
}
