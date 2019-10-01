namespace Labb4
{
    class Door : Box
    {
        public Door(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public override bool IsBoxAvailable()
        {
            //if key available then true 
            return false;
        }
    }
}
