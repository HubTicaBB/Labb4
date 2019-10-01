namespace Labb4
{
    enum Symbols 
    { 
        Room = '-', 
        Wall = '#', 
        Door = 'D', 
        Exit = 'E',
        Key = 'k',
        Monster = 'M',
        Surprise = '?',
        Player = '@'
    }

    internal abstract class Box : IsAvailable
    {
        public Symbols Symbol { get; set; }
        public Items Item { get; set; }
        public Monster Monster { get; set; }

        public Box(Symbols symbol)
        {
            this.Symbol = symbol;
        }

        public Box(Symbols symbol, Items item)
        {
            this.Symbol = symbol;
            this.Item = item;
        }

        public Box(Symbols symbol, Monster monster)
        {
            this.Symbol = symbol;
            this.Monster = monster;
        }

        public abstract bool IsBoxAvailable();
    }
}
