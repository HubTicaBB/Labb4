using System;
using System.Threading;

namespace Labb4
{
    internal class Room : Box
    {
        public Room(Symbols symbol, int positionX, int positionY) : base(symbol, positionX, positionY)
        {
            Symbol = symbol;
            PositionX = positionX;
            PositionY = positionY;
        }

        public Room(Symbols symbol, Monster monster, int positionX, int positionY) : base(symbol, monster, positionX, positionY)
        {
            Symbol = symbol;
            Monster = monster;
            PositionX = positionX;
            PositionY = positionY;
        }

        public Room(Symbols symbol, Items items, int positionX, int positionY) : base(symbol, items, positionX, positionY)
        {
            Symbol = symbol;
            Item = items;
            PositionX = positionX;
            PositionY = positionY;
        }

        public override bool IsBoxAvailable(Player player)
        {
            if (Monster != null)
            {
                if (player.HasWeapon())
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nYou do not have a weapon!\nGo around and pick up a weapon.");
                    Thread.Sleep(1500);
                    return false;
                }
            }
            return true;
        }
    }
}
