using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4
{
    class Room : Box
    {
        public Room(char symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }
    }
}
