namespace Labb4
{
    class Room : Box
    {
        public Room(char symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }
        public override bool IsBoxAvailable()
        {
            return true;
        }
    }
}
