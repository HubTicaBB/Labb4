namespace Labb4
{
    internal abstract class Box
    {
        public char Symbol { get; set; }

        public Box(char symbol)
        {
            this.Symbol = symbol;
        }

        public override string ToString()
        {
            return $"{Symbol}";
        }
    }
}