using System;
using System.Threading;

namespace Labb4
{
    class Door : Box
    {
        public Door(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable()
        {
            foreach (var item in itemsList)
            {
                if (item.GetType() == typeof(Key) || item.GetType() == typeof(SuperKey))
                {
                    item.NumberUsageKey--;
                    if (item.NumberUsageKey == 0)
                    {
                        itemsList.Remove(item);
                    }
                    return true;
                }
            }
            System.Console.WriteLine("There is no key. \nYou have to go around and pick up a key.");
            Thread.Sleep(1000);
            return false;
        }
    }
}
