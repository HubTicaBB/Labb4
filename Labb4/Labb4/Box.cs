namespace Labb4
{
    internal abstract class Box : Player, IAvailable
    {
        public Symbols Symbol { get; set; }
        public Items Item { get; set; }
        public Monster Monster { get; set; }

        public Box(Symbols symbol) : base(symbol)
        {
            this.Symbol = symbol;
        }

        public Box(Symbols symbol, Items item) : base(symbol)
        {
            this.Symbol = symbol;
            this.Item = item;
        }

        public Box(Symbols symbol, Monster monster) : base(symbol)
        {
            this.Symbol = symbol;
            this.Monster = monster;
        }

        public abstract bool IsBoxAvailable();
    }
}
