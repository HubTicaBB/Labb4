namespace Labb4
{
    class Sword : Items
    {
        public Sword(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageKey = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Sword: ";
        }
    }
}
