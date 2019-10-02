using System;
using System.Threading;

namespace Labb4
{
    class Wall : Box
    {        
        public Wall(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable()
        {
            Console.WriteLine($"\nYou reached a wall. Try another command!");
            Thread.Sleep(1200);
            return false;
        }
    }
}
