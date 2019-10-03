namespace Labb4
{
    internal class Room : Box
    {
        public Room(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public Room(Symbols symbol, Monster monster) : base(symbol, monster)
        {
            this.Symbol = symbol;
            this.Monster = monster;
        }

        public Room(Symbols symbol, Items items) : base(symbol, items)
        {
            this.Symbol = symbol;
            this.Item = items;
        }

        public override bool IsBoxAvailable(Player player)
        {           
            return true;
        }
    }
}
