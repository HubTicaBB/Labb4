namespace Labb4
{
    class SuperKey : Items
    {
        public SuperKey(int numberUsageKey) : base(numberUsageKey)
        {
            this.NumberUsageItem = numberUsageKey;
        }

        public override string ToString()
        {
            return $"Super key: ";
        }
    }
}
