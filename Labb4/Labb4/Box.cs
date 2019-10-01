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

        public Box(Symbols symbol)
        {
            this.Symbol = symbol;
        }

        public abstract bool IsBoxAvailable();
    }
}
