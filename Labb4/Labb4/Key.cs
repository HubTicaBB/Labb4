namespace Labb4
{
    class Key : Items
    {
        public int NumberUsageKey { get; set; }
        public Key(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageKey = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Key: ";
        }
    }
}
