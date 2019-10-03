using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb4
{
    class SuperKey : Items
    {
        public SuperKey(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageKey = numberUsageKey;
        }

        //public void ReduceNumberUsage()
        //{
        //    NumberUsageKey--;
        //}

        public override string ToString()
        {
            return $"Super key: ";
        }
    }
}
