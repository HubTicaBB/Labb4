namespace Labb4
{
    class Key : Items
    {
        public Key(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageItem = numberUsageKey;
        }

        //public void ReduceNumberUsage()
        //{
        //    NumberUsageKey--;
        //}

        public override string ToString()
        {
            return $"Key: ";
        }
    }
}
