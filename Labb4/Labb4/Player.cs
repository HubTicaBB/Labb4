using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Labb4
{   
    internal class Player 
    {        
        public string Name { get; set; }
        public int MovesLeft { get; set; }
        public int PositionRow { get; set; }
        public int PositionCol { get; set; }        

        public Player(string name, int movesLeft, int positionRow, int positionCol)
        {
            Name = name;
            MovesLeft = movesLeft;
            PositionRow = positionRow;
            PositionCol = positionCol;
        }

        //public void NewGame()
        //{
        //    //Console.Write("Enter your name: ");
        //    //string name = Console.ReadLine();
        //    // TODO: Validera namnet
        //    //players.Add(new Player(name, 100, 3, 3));
        //    bool play = true;
        //    players[players.Count - 1].CreateObjects();
        //    while (play)
        //    {
        //        players[players.Count - 1].Play();
        //        if (players[players.Count - 1].MovesLeft == 0)
        //        {
        //            play = false;
        //        }
        //        // om objektet är Exit --> play = false
        //    }
        //
        //    Console.WriteLine("Does anyone else want to play? (yes/no)");
        //    string answer = Console.ReadLine();
        //    // TODO: Check if answer is yes eller no
        //    if (answer == "yes")
        //    {
        //        NewGame();
        //    }
        //}

        public void ChangePosition(int newRowPosition, int newColPosition, Box[,] mapWithObjects)
        {
            mapWithObjects[PositionRow, PositionCol].Symbol = Symbols.Room;            
            PositionRow = newRowPosition;
            PositionCol = newColPosition;
            Box currentBox = mapWithObjects[PositionRow, PositionCol];
            Items item;
            if (currentBox.Item != null)
            {
                item = currentBox.Item;
                PickUpItem(item, currentBox);
            }
            
            MovesLeft--;
        }

        public bool HasKey()
        {
            foreach (var item in itemsList)
            {
                if (item is Key || item is SuperKey)
                {
                    item.NumberUsageKey -= 1;
                    if (item.NumberUsageKey == 0)
                    {
                        itemsList.Remove(item);
                    }
                    return true;
                }
            }
            return false;
        }   

        public List<Items> itemsList = new List<Items>();
        internal virtual void PickUpItem(Items item, Box box)
        {
            itemsList.Add(item);
            box.Item = null;
        }
    }
}
