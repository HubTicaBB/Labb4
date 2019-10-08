namespace Labb4
{
    class Potion : Items
    {
        public Potion(int numberUsageItem) : base(numberUsageItem)
        {
            this.NumberUsageItem = numberUsageItem;
        }

        public override string ToString()
        {
            return $"Potion: ";
        }
    }
}
