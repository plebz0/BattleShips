using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
   
    internal class Ship
    {
        public enum SegmentState {FINE, DAMAGED, SINKED};
        public char[] SegmentStateSymbols = { '#', '%', '*' };


        public (int, int)[] cords = { (-1, -1), (-1, -1), (-1, -1), (-1, -1)};
        public SegmentState[] segments = { SegmentState.FINE, SegmentState.FINE, SegmentState.FINE, SegmentState.FINE};
        int holes = 0;

        public Ship(int holes)
        {
            this.holes = holes;
            for(int i = 0; i < holes; i++)
            {
                cords[i] = (-1, -1);
                segments[i] = SegmentState.FINE;
            }
        }

        public void RenderToUI() 
        {
            for(int i = 0; i<holes; i++)
            {
                Console.Write( $"{SegmentStateSymbols[ ((int)segments[i]) ]}" );
            }
        }
    }
}
