namespace Labb4
{
    class Potion : Items
    {
        public Potion(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageItem = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Potion: ";
        }
    }
}
