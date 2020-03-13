using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout_Slot
{
    //class representing player.
    class Player
    {

        //amount of money of Player
        private decimal credit;
        public decimal Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        //value of normal spins
        private decimal stake;
        public decimal Stake
        {
            get { return stake; }
            set { stake = value; }
        }

        //Free Spins possesed by player
        private List<Freespin> freespins;
        public List<Freespin> Freespins
        {
            get { return freespins; }
            set { freespins = value; }
        }

        //counter for number of spins played during the session - for developer's purpose
        private int spinsSoFar;
        public int SpinsSoFar
        {
            get { return spinsSoFar; }
            set { spinsSoFar = value; }
        }

        public Player()
        {
            credit = 0;
            freespins = new List<Freespin>();
            stake = 1;
            spinsSoFar = 0;
        }

    }
}
