namespace Labb4
{
    class Trap : Items
    {
        public Trap(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageItem = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Trap: ";
        }
    }
}
