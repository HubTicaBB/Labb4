namespace Labb4
{
    class Room : Box
    {
        Monster monster;
        Items items;

        public Room(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public Room(Symbols symbol, Monster monster) : base(symbol)
        {
            this.Symbol = symbol;
            this.monster = new Monster();
        }

        public Room(Symbols symbol, Items items) : base(symbol)
        {
            this.Symbol = symbol;
            this.items = new Items();
        }

        public override bool IsBoxAvailable()
        {
            return true;
        }
    }
}
