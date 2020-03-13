using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout_Slot
{
    class Program
    {

        static void Main(string[] args)
        {
            Symbol[,] board = new Symbol[5, 3];
            Player player = new Player();
            bool UserWantsToExit = false;
        

            DisplayOpening();
            DisplayPlayerStatsOnScreen(player);

            Console.ReadKey();


            while (UserWantsToExit == false)
            {
                Console.Clear();

                //collects informations about prizes - for displaying purposes
                List<string> victories = new List<string>();


                //distinction if playing with normal credit or Free Spin to avoid triggering of Free Spins 
                if (player.Freespins.Count == 0)
                {
                    board = PlayWithCredit(player, victories);
                }

                else
                {
                    board = PlayWithFreeSpin(player, victories);
                }

                DisplayBoardOnScreen(board);

                DisplayVictoriesOnScreen(victories);

                DisplayPlayerStatsOnScreen(player);

                char input = Console.ReadKey().KeyChar;

                if (input == 's')
                {
                    ChoosingStake(player, board);
                }

                if (input == 'q')
                {
                    UserWantsToExit = true;
                }         
            }
        }


        static Symbol[,] PlayWithCredit(Player player, List<string> victories)
        {
            player.Credit -= player.Stake;

            Symbol[,] board = new Symbol[5, 3];
            DrawController dc = new DrawController();
            board = dc.Spin();

            WinningLinesChecker linesChecker = new WinningLinesChecker(player, victories, player.Stake);
            linesChecker.CheckAllLines(board);

            player.SpinsSoFar++;

            return board;

        }

        static Symbol[,] PlayWithFreeSpin(Player player, List<string> victories)
        {

            Symbol[,] board = new Symbol[5, 3];
            DrawController dc = new DrawController();
            board = dc.Spin();

            WinningLinesChecker linesChecker = new WinningLinesChecker(player, victories, player.Freespins[0].Multiplier);
            linesChecker.CheckLinesIgnoringFreeSpins(board);

            player.Freespins.RemoveAt(0);
            player.SpinsSoFar++;

            return board;

        }

        static void DisplayOpening()
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to Fallout Slot Game.");
            Console.WriteLine("In any moment press 's' to choose stake, 'q' to exit, or any other key to play.");
            Console.WriteLine("Good luck to you Chosen One.");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("");
            }
        }

        static void DisplayBoardOnScreen(Symbol[,] board)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(board[0, 0].NameToDisplayOnBoard + "|" + board[1, 0].NameToDisplayOnBoard + "|" + board[2, 0].NameToDisplayOnBoard + "|" + board[3, 0].NameToDisplayOnBoard + "|" + board[4, 0].NameToDisplayOnBoard);
            Console.WriteLine(board[0, 1].NameToDisplayOnBoard + "|" + board[1, 1].NameToDisplayOnBoard + "|" + board[2, 1].NameToDisplayOnBoard + "|" + board[3, 1].NameToDisplayOnBoard + "|" + board[4, 1].NameToDisplayOnBoard);
            Console.WriteLine(board[0, 2].NameToDisplayOnBoard + "|" + board[1, 2].NameToDisplayOnBoard + "|" + board[2, 2].NameToDisplayOnBoard + "|" + board[3, 2].NameToDisplayOnBoard + "|" + board[4, 2].NameToDisplayOnBoard);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        static void DisplayVictoriesOnScreen(List<string> victories)
        {

            foreach (var item in victories)
            {
                Console.WriteLine(item);
            }

            for (int i = 0; i < 5 - victories.Count; i++)
            {
                Console.WriteLine("");
            }
        }

        static void DisplayPlayerStatsOnScreen(Player player)
        {
            Console.WriteLine("Credit: " + player.Credit);
            Console.WriteLine("Stake: " + player.Stake);
            Console.WriteLine("Free Spins: " + player.Freespins.Count);

            if (player.Freespins.Count == 0)
            {
                Console.WriteLine("Next Free Spin stake: ");
            }

            else
            {
                Console.WriteLine("Next Free Spin stake: " + player.Freespins[0].Multiplier);
            }

            Console.WriteLine("Spins so far: " + player.SpinsSoFar);
        }
        static void ChoosingStake(Player player, Symbol[,] actualBoard)
        {
            decimal newStake;
            decimal one = 1;
            decimal ten = 10;
            bool conditionsMatched = false;
            

            Console.Clear();     
            Console.WriteLine("Type the number from 1 to 10 and play the next game with the new stake.");


            while (conditionsMatched == false)
            {               
                string input = Console.ReadLine();
   
                bool correctFormat = decimal.TryParse(input, out newStake);

                if (correctFormat == true && newStake >= one && newStake <= ten)
                {
                    player.Stake = newStake;
                    conditionsMatched = true;
                }

                else
                {
                    Console.WriteLine("Invalid format. Try again.");
                }
            }
        }
    }







}
