﻿namespace Labb4
{
    public class Items
    {
        public virtual int NumberUsageItem { get; set; }
        public Items(int numberUsageItem)
        {
            NumberUsageItem = numberUsageItem;
        }
    }
}
