namespace Labb4
{
    class Bomb : Items
    {
        public Bomb(int numberOfUsages) : base(numberOfUsages)
        {
            NumberUsageItem = numberOfUsages;
        }

        public override string ToString()
        {
            return $"Bomb: ";
        }
    }
}
