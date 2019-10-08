namespace Labb4
{
    class Sword : Items
    {
        public Sword(int numberUsageItem) : base(numberUsageItem)
        {
            this.NumberUsageItem = numberUsageItem;
        }

        public override string ToString()
        {
            return $"Sword: ";
        }
    }
}
