namespace Labb4
{
    class Bomb : Items
    {
        public Bomb(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageKey = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Bomb: ";
        }
    }
}
