namespace Labb4
{
    class Room : Box
    {
        Monster monster;
        Key key;

        public Room(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public Room(Symbols symbol, Monster monster) : base(symbol)
        {
            this.Symbol = symbol;
            this.monster = new Monster();
        }       

        public Room(Symbols symbol, Key key) : base(symbol)
        {
            this.Symbol = symbol;
            this.key = new Key();
        }

        public override bool IsBoxAvailable()
        {
            return true;
        }
    }
}
