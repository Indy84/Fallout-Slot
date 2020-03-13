using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout_Slot
{
    class DrawController
    {
        // Declaring array of collections of symbols. Each collection represents one reel.
        private List<Symbol>[] reels = new List<Symbol>[5];

        //Constructor fills all reels with given number of symbols.
        public DrawController()
        {
            reels[0] = CreateReel(1, 2, 2, 5, 6, 1);
            reels[1] = CreateReel(1, 1, 3, 5, 8, 2);
            reels[2] = CreateReel(1, 2, 3, 6, 7, 1);
            reels[3] = CreateReel(1, 2, 3, 5, 9, 1);
            reels[4] = CreateReel(1, 3, 2, 5, 6, 1);
        }

        // Method for creating one reel with chosen amount of symbols.
        private List<Symbol> CreateReel(int nuclearBomb, int brotherhoodArmor, int vault13, int stimpack, int nukaCola, int freeSpin)
        {
            List<Symbol> reel = new List<Symbol>();

            for (int i = 0; i < nuclearBomb; i++)
                reel.Add(new Symbol(Figures.Nuclear, "     Nuclear Bomb     ", new int[] { 20, 200, 2000 }));

            for (int i = 0; i < brotherhoodArmor; i++)
                reel.Add(new Symbol(Figures.Brotherhood, " Brotherhood of Steel ", new int[] { 10, 20, 60 }));

            for (int i = 0; i < vault13; i++)
                reel.Add(new Symbol(Figures.Vault, "       Vault 13       ", new int[] { 3, 6, 15 }));

            for (int i = 0; i < stimpack; i++)
                reel.Add(new Symbol(Figures.Stimpack, "       Stimpack       ", new int[] { 1, 3, 6 }));

            for (int i = 0; i < nukaCola; i++)
                reel.Add(new Symbol(Figures.NukaCola, "       NukaCola       ", new int[] { 1, 2, 3 }));

            for (int i = 0; i < freeSpin; i++)
                reel.Add(new Symbol(Figures.FreeSpin, "       FreeSpin       ", new int[] { 0, 0, 0 }));

            Shuffler.ShuffleList<Symbol>(reel);

            return reel;

        }

        //Generetaes 2-dimensional array 5x3 representing board and fills it with randomly chosen symbols from reels.
        private Symbol[,] GenerateNewBoard(List<Symbol>[] reels)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Symbol[,] board = new Symbol[5, 3];

            //looping through columns
            for (int j = 0; j < board.GetLength(0); j++)
            {

                //looping through rows
                for (int i = 0; i < board.GetLength(1); i++)
                {
                    int r = random.Next(reels[j].Count);

                    //choosing random element from reel and putting it on board
                    board[j, i] = reels[j][r];
                    
                    //eliminating from reel all symbols of type which was drawn
                    reels[j].RemoveAll(t => t.Name == board[j, i].Name);
                }
            }

            return board;
        }

        //public method for generating complete board
        public Symbol[,] Spin()
        {
            Symbol[,] board = new Symbol[5, 3];
            board = GenerateNewBoard(reels);
            return board;
        }




    }



}


