using System;
using System.Threading;

namespace Labb4
{
    class Door : Box
    {
        public Door(Symbols symbol, int positionX, int positionY) : base(symbol, positionX, positionY)
        {
            this.Symbol = symbol;

        }

        public override bool IsBoxAvailable(Player player)
        {
            if (player.HasKey()) //kolla om spelaren håller i en nyckel
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
