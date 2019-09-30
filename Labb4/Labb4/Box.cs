namespace Labb4
{
    internal abstract class Box : IsAvailable
    {
        public char Symbol { get; set; }        

        public Box(char symbol)
        {
            this.Symbol = symbol;
        }

        public abstract bool IsBoxAvailable();
    }
}
