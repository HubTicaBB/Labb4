using System;
using System.Threading;

namespace Labb4
{
    class Monster : Player, IAvailable
    {
        public int MonsterPower { get; set; }
        public Monster(int monsterPower)
        {
            MonsterPower = monsterPower;
        }
       

        public void ReduceMonstersPower(Items weapon)
        {
            if (weapon.GetType() == typeof(Bomb))
            {
                MonsterPower -= 5;                
            }
            else if (weapon.GetType() == typeof(Sword))
            {
                MonsterPower -= 2;
            }
            weapon.NumberUsageKey--;
            if (weapon.NumberUsageKey < 1)
            {
                itemsList.Remove(weapon);
            }
            if (MonsterPower > 0)
            {
                Console.WriteLine($"The beast is hurt, its current power is {MonsterPower}.\nYou have to fight igen.");
            }
            else
            {
                Console.WriteLine("The bastard is dead, Congratulations");
            }
            Thread.Sleep(1200);            
        }

       

        public bool IsBoxAvailable()
        {
            Game game = new Game();
            foreach (var item in itemsList)
            {
                if (item.GetType() == typeof(Bomb) || item.GetType() == typeof(Sword))
                {
                    item.NumberUsageKey--;
                    if (item.NumberUsageKey == 0)
                    {
                        itemsList.Remove(item);
                    }
                    return true;
                }
            }
            Console.WriteLine("You have no weapons to fight the monster. Go around and pick up some!");
            Thread.Sleep(1000);
            return false;
        }
    }
}
