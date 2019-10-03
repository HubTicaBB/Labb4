using System;
using System.Threading;

namespace Labb4
{
    class Door : Box
    {
        public Door(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
            Name = "door";
        }

        public override bool IsBoxAvailable()

        {
            return true;
            if (CheckIfKeyIsAvailable())
            {
                return true;
            }
            else
            {
                Console.WriteLine("There is no key. \nYou have to go around and pick up a key.");
                Thread.Sleep(1000);
                return false;
            }
            
        }
    }
}
