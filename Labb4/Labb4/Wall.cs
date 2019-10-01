namespace Labb4
{
    class Wall : Box
    {
        public Wall(char symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable()
        {
            return false;
        }
    }
}
