namespace Labb4
{
    class Wall : Box
    {        
        public Wall(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable()
        {
            return false;
        }
    }
}
