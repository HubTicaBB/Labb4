namespace Labb4
{
    class Sword : Items
    {
        public Sword(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageItem = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Sword: ";
        }
    }
}
