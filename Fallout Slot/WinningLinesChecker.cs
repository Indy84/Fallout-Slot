using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout_Slot
{
    class WinningLinesChecker
    {

        private Player player;
        private decimal betValue;
        
        //Collection to gather notifications of all winnings for displaying purposes.
        private List<string> victoriesNotifications;
       

        public WinningLinesChecker(Player player, List<string> victories, decimal betValue)
        {
            this.player = player;
            this.victoriesNotifications = victories;
            this.betValue = betValue;
        }

        //Normal symbols and Free Spins are checked separately due to the fact, that while playing on Free Spin - additional Free Spins cannot be awarded.

        //Checking all lines. When symbols match - the player is awarded.
        public void CheckLinesIgnoringFreeSpins(Symbol[,] board)
        {
            decimal prizes = 0;

            //first line
            prizes += CheckLine(board, "first line", new int[] { 0, 0, 0, 0, 0, });

            //second line
            prizes += CheckLine(board, "second line", new int[] { 1, 1, 1, 1, 1, });

            //third line
            prizes += CheckLine(board, "third line", new int[] { 2, 2, 2, 2, 2 });

            //pyramidal line
            prizes += CheckLine(board, "pyramidal line", new int[] { 2, 1, 0, 1, 2 });

            //reversed pyramidal line
            prizes += CheckLine(board, "reversed pyramidal line", new int[] { 0, 1, 2, 1, 0 });

            //downstairs line
            prizes += CheckLine(board, "downstairs line", new int[] { 0, 0, 1, 2, 2 });

            //upstairs line
            prizes += CheckLine(board, "upstairs line", new int[] { 2, 2, 1, 0, 0 });

            player.Credit += prizes;
        }

        private void CheckFreeSpins(Symbol[,] board)
        {

            int freeSpinsCounter = 0;

            //loops through entire board and counts Free Spins
            for (int k = 0; k < board.GetLength(0); k++)
            {
                for (int l = 0; l < board.GetLength(1); l++)
                {
                    if (board[k, l].Name == Figures.FreeSpin)
                    {
                        freeSpinsCounter++;
                    }
                }
            }

            if (freeSpinsCounter == 4)
            {
                victoriesNotifications.Add("For hitting 4 FreeSpin symbols you won 3 Free Spins with value 2x of your bet: " + betValue * 2);

                for (int i = 0; i < 3; i++)
                {
                    player.Freespins.Add(new Freespin(betValue * 2));
                }

            }

            if (freeSpinsCounter == 5)
            {

                victoriesNotifications.Add("For hitting 5 FreeSpin symbols you won 7 Free Spins with value 5x of your bet: " + betValue * 5);

                for (int i = 0; i < 5; i++)
                {
                    player.Freespins.Add(new Freespin(betValue * 7));
                }
            }

        }

        public void CheckAllLines(Symbol[,] board)
        {      
            CheckLinesIgnoringFreeSpins(board);
            CheckFreeSpins(board);
        }

        //The method checks column after column with given vertical coordinates and looks for matching symbols. Checking excludes Free Spins.
        private decimal CheckLine(Symbol[,] board, string nameOfLine, int[] verticalCoordinates)
        {

            //the prize of eventual win
            decimal prize = 0;

            //Checking if 1st, 2nd and 3rd symbol in line are the same, but different than 4th.
            if (board[0, verticalCoordinates[0]].Name == board[1, verticalCoordinates[1]].Name && board[0, verticalCoordinates[0]].Name == board[2, verticalCoordinates[2]].Name && board[0, verticalCoordinates[0]].Name != board[3, verticalCoordinates[3]].Name && board[0, verticalCoordinates[0]].Name != Figures.FreeSpin)
            {
                prize = prize + (board[0, verticalCoordinates[0]].PrizesForWinning[0]) * betValue;
                victoriesNotifications.Add("Victory at " + nameOfLine + ". Hitting 3 elements. Prize: " + prize);
            }

            //Checking if 1st, 2nd, 3rd and 4th symbol in line are the same, but different than 5th.
            if (board[0, verticalCoordinates[0]].Name == board[1, verticalCoordinates[1]].Name && board[0, verticalCoordinates[0]].Name == board[2, verticalCoordinates[2]].Name && board[0, verticalCoordinates[0]].Name == board[3, verticalCoordinates[3]].Name && board[0, verticalCoordinates[0]].Name != board[4, verticalCoordinates[4]].Name && board[0, verticalCoordinates[0]].Name != Figures.FreeSpin)
            {
                prize = prize + (board[0, verticalCoordinates[0]].PrizesForWinning[1]) * betValue;
                victoriesNotifications.Add("Victory at " + nameOfLine + ". Hitting 4 elements. Prize: " + prize);
            }

            //Checking if 1st, 2nd, 3rd, 4th and 5th symbol in line are the same.
            if (board[0, verticalCoordinates[0]].Name == board[1, verticalCoordinates[1]].Name && board[0, verticalCoordinates[0]].Name == board[2, verticalCoordinates[2]].Name && board[0, verticalCoordinates[0]].Name == board[3, verticalCoordinates[3]].Name && board[0, verticalCoordinates[0]].Name == board[4, verticalCoordinates[4]].Name && board[0, verticalCoordinates[0]].Name != Figures.FreeSpin)
            {
                prize = prize + (board[0, verticalCoordinates[0]].PrizesForWinning[2]) * betValue;
                victoriesNotifications.Add("Victory at " + nameOfLine + ". Hitting 5 elements. Prize: " + prize);
            }

            return prize;

        }



    }


}

