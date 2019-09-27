using System;

namespace Labb4
{
    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        int StartPositionRow = 3;
        int StartPositionColumn = 3;

        public Player(string name, int score = 100)
        {
            this.Name = name;
        }

        internal void Play()
        {
            throw new NotImplementedException();
        }
    }
}
