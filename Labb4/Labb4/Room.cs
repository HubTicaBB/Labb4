namespace Labb4
{
    class Room : Box
    {
        Monster monster;
        Key key;

        public Room(char symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public Room(char symbol, Monster monster) : base(symbol)
        {
            this.Symbol = symbol;
            this.monster = new Monster();
        }

        public Room(char symbol, Key key) : base(symbol)
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
