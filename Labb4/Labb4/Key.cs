﻿namespace Labb4
{
    class Key : Items
    {
        public Key(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageItem = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Key: ";
        }
    }
}
