namespace Labb4
{
    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        int StartPositionRow = 3;
        int StartPositionColumn = 3;

        public Player(string name)
        {
            this.Name = name;
        }



    }
}
