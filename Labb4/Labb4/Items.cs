using System.Collections.Generic;

namespace Labb4
{
    public class Items
    {
        public virtual int NumberUsageKey { get; set; }
        public Items(int numberUsage)
        {
            NumberUsageKey = numberUsage;
        }
    }
}
