namespace Labb4
{
    class Room : Box
    {
        Monster monster;

        public Room(char symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public Room(char symbol, Monster monster) : base(symbol)
        {
            this.Symbol = symbol;
            this.monster = new Monster();
        }

        public override bool IsBoxAvailable()
        {
            return true;
        }
    }
}
