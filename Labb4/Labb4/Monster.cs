using System;
using System.Threading;

namespace Labb4
{
    class Monster : IAvailable
    {
        public int MonsterPower { get; set; }
        static Game game = new Game();
        static Player player = game.players[game.players.Count - 1];
        public Monster()
        {
            MonsterPower = 10;
        }
        public void FightMonster(Items weapon)
        {
            Console.WriteLine("You reached a room with a terryfing monster!! Use your weapons to fight the beast!");
            string input = Console.ReadLine();

            if (weapon.GetType().Name.ToString() == input)
            {
                MonsterPower -= 5;
                player.itemsList.Remove(weapon);
            }
            else if (weapon.GetType() == typeof(Sword))
            {
                MonsterPower -= 2;
            }
        }
        public bool IsBoxAvailable()
        {
            foreach (var item in player.itemsList)
            {
                if (item.GetType() == typeof(Bomb) || item.GetType() == typeof(Sword))
                {
                    FightMonster(item);
                    item.NumberUsageKey--;
                    if (item.NumberUsageKey == 0)
                    {
                        player.itemsList.Remove(item);
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
