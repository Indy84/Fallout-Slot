using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout_Slot
{
    //class representing Free Spins
    class Freespin
    {
        private decimal multiplier;
        public decimal Multiplier
        {
            get
            {
                return multiplier;
            }
            set
            {
                multiplier = value;
            }
        }

        //The argument determines the value of Free Spin - if Free Spins are won while playing with higher credit, their value is adequate.
        public Freespin(decimal multiplier)
        {
            this.multiplier = multiplier;
        }
    }
}
