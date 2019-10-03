using System;
using System.Threading;

namespace Labb4
{
    class Wall : Box
    {
        public Wall(Symbols symbol, int positionX, int positionY) : base(symbol, positionX, positionY)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable(Player player)
        {
            Console.WriteLine($"\nYou reached a wall. Try another command!");
            Thread.Sleep(1200);
            return false;
        }
    }
}
