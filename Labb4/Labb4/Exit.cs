namespace Labb4
{
    class Exit : Box
    {
        public Exit(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable()
        {
            return true;
        }
    }
}
