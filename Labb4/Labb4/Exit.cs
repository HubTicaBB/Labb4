namespace Labb4
{
    class Exit : Box
    {
        public Exit(Symbols symbol, int positionX, int positionY) : base(symbol, positionX, positionY)
        {
            Symbol = symbol;
        }

        public override bool IsBoxAvailable(Player player)
        {
            return true;
        }
    }
}
