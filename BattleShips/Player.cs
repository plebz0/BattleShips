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
        public enum ShotInfo
        {
            MISSED, SINKED, HIT, RESHOOT
        }

        public string name = "";
        public bool isAi = false;
        public BattleField bf = new BattleField();
        public Ship[] ships = { new Ship(4), new Ship(3), new Ship(3), new Ship(2), new Ship(2), new Ship(2), new Ship(1), new Ship(1), new Ship(1), new Ship(1), };

        public string SetName(string name)
        {
            return name;
        }

        public bool PlaceInfo(int x, int y, string shipType) {
            if(shipType == "1") {
                if (bf[x, y].isShip) {
                    Console.WriteLine("Niestety jest tu statek");
                    return false;
                } 
                else {
                    Console.WriteLine("Wstawianie statku");
                    return true;
                }
            }

            return false;
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
            if (bf[x, y].shoted)
            {
                return ShotInfo.RESHOOT;
            }
            else if (!(bf[x, y].isShip)) 
            {
                return ShotInfo.MISSED;
            }
            else
            {
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
