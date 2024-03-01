using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BattleShips
{
    internal class Player
    {
        static int[] templateShips = new int[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
        
        public enum ShotInfo
        {
            MISSED, SINKED, HIT, RESHOOT
        }

        public string name = "";
        public bool isAI = false;
        public int wins = 0;
        public BattleField bf = new BattleField();

        public Ship[] ships = new Ship[templateShips.Length];

        public Player()
        {
            for(int i = 0; i < templateShips.Length; i++) {
                ships[i] = new Ship(templateShips[i]);
            }
        }

        public string SetName(string name)
        {
            return name;
        }

        public void resetShips()
        {
            for(int i = 0; i < templateShips.Length; i++) {
                ships[i] = new Ship(templateShips[i]);
            }
        }

        public void resetBattleField()
        {
            bf = new BattleField();
        }   

        public bool isDefeated()    {
            foreach (Ship ship in ships) {
                if (!ship.isSinked()) {
                    return false;
                }
            }
            return true;
        }

        public ShotInfo getShot(int x, int y) {
            if (bf[x, y].shoted) // already shoted
            {
                return ShotInfo.RESHOOT;
            }
            else if (!(bf[x, y].isShip))  // missed
            {
                bf[x, y].shoted = true;
                return ShotInfo.MISSED;
            }
            else // hit
            {
                bf[x, y].shoted = true;

                Ship hitShip = bf[x, y].shipOver;
                hitShip[bf[x, y].segmentIndex] = Ship.SegmentState.DAMAGED;

                if (hitShip.isSinked()) {
                    return ShotInfo.SINKED;
                }
                else { 
                    return ShotInfo.HIT;
                }
            }
        }
    }
}
