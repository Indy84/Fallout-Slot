using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallout_Slot
{

    //class representing symbols
    class Symbol
    {
        private Figures name;
        private string nameToDisplayOnBoard = "";

        //array with potential prizes - depending on how many symbols are hit in line
        private int[] prizesForWinning = new int[3];

        public Figures Name { get { return name; } }
        public string NameToDisplayOnBoard { get { return nameToDisplayOnBoard; } }

        public int[] PrizesForWinning { get { return prizesForWinning; } }

        public Symbol(Figures figure, string name, int[] multipliers)
        {
            this.name = figure;
            this.nameToDisplayOnBoard = name;
            this.prizesForWinning = multipliers;
        }
    }

    enum Figures
    {
        Nuclear,
        Brotherhood,
        Vault,
        Stimpack,
        NukaCola,
        FreeSpin
    }



}
