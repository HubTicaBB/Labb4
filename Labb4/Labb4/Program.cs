using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            players.Add(new Player("Andreea"));
            players.Add(new Player("Tijana"));
            players.Add(new Player("Pontus"));
        }
    }
}
