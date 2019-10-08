namespace Labb4
{
    class Trap : Items
    {
        public Trap(int numberUsageItem) : base(numberUsageItem)
        {
            this.NumberUsageItem = numberUsageItem;
        }

        public override string ToString()
        {
            return $"Trap: ";
        }
    }
}
