using System;
using System.Threading;

namespace Labb4
{
    internal class Room : Box
    {
        public Room(Symbols symbol, int positionX, int positionY) : base(symbol, positionX, positionY)
        {
            this.Symbol = symbol;
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public Room(Symbols symbol, Monster monster, int positionX, int positionY) : base(symbol, monster, positionX, positionY)
        {
            this.Symbol = symbol;
            this.Monster = monster;
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public Room(Symbols symbol, Items items, int positionX, int positionY) : base(symbol, items, positionX, positionY)
        {
            this.Symbol = symbol;
            this.Item = items;
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public override bool IsBoxAvailable(Player player)
        {
            if (Monster != null)
            {
                if (player.HasWeapon())
                {
                    //player.FightMonster();
                    return true;
                }
                else
                {
                    Console.WriteLine("\nYou do not have a weapon!\nGo around and pick up a weapon.");
                    Thread.Sleep(1000);
                    return false;
                }
            }
            return true;
        }


    }
}
