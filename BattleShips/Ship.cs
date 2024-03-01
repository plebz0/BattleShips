using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
   
    internal class Ship
    {
        public enum SegmentState {FINE, DAMAGED, SINKED};


        public (int, int)[] cords = { (-1, -1), (-1, -1), (-1, -1), (-1, -1)};
        public SegmentState[] segments = { SegmentState.FINE, SegmentState.FINE, SegmentState.FINE, SegmentState.FINE};
        public int holes = 0;

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
                Console.Write( segmentToChar(i) );
            }
        }

        public void RenderToUIForEnemy() 
        {
            for(int i = 0; i<holes; i++)
            {
                switch(segments[i])
                {
                    case SegmentState.DAMAGED:
                    case SegmentState.FINE:
                        Console.Write('#');
                        break;
                    case SegmentState.SINKED:
                        Console.Write('o');
                        break;
                    default:
                        Console.Write('E');
                        break;
                }
            }
        }

        public SegmentState this[int i]
        { 
            set { segments[i] = value; }
            get { return segments[i]; }
        }

        public bool isSinked()
        {
            if (segments[0] == SegmentState.SINKED) return true;

            for( int i = 0; i < holes; i++)
            {
                if (segments[i] != SegmentState.DAMAGED)
                {
                    return false;
                }
            }

            for (int i = 0; i < holes; i++)
            {
                segments[i] = SegmentState.SINKED;
            }
            return true;
        }

        public char segmentToChar(int index)
        {
            switch(segments[index])
            {
                case SegmentState.FINE:
                    return '#';
                case SegmentState.DAMAGED:
                    return '%';
                case SegmentState.SINKED:
                    return 'o';
                default:
                    return 'E';
                
            }
        }
        public char segmentToCharForEnemy(int index)
        {
            switch(segments[index])
            {
                case SegmentState.FINE:
                    return ' ';
                case SegmentState.DAMAGED:
                    return '%';
                case SegmentState.SINKED:
                    return 'o';
                default:
                    return 'E';
                
            }
        }
    }
}
