using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    internal class Player
    {
        public string name = "";
        BattleField bf;
        public Ship[] ships = { new Ship(4), new Ship(3), new Ship(3), new Ship(2), new Ship(2), new Ship(2), new Ship(1), new Ship(1), new Ship(1), new Ship(1), };
    }
}
