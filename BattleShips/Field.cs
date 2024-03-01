using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    internal class Field
    {
        public bool shoted = false;
        public bool isShip = false;
        public Ship shipOver;
        public int segmentIndex;
        public char toChar() { 
            if (!isShip)
            {
                if(shoted)
                {
                    return 'X';
                }
                else 
                {
                    return ' ';
                }
            }
            else
            {
                return shipOver.segmentToChar(segmentIndex);
            }
        }
         public char toCharForEnemy() { 
            if (!isShip)
            {
                if(shoted)
                {
                    return 'X';
                }
                else 
                {
                    return ' ';
                }
            }
            else
            {
                return shipOver.segmentToCharForEnemy(segmentIndex);
            }
        }
    }
}
