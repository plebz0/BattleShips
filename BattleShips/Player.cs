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
        public string name = "";
        public BattleField bf = new BattleField();
        public Ship[] ships = { new Ship(4), new Ship(3), new Ship(3), new Ship(2), new Ship(2), new Ship(2), new Ship(1), new Ship(1), new Ship(1), new Ship(1), };

        public string SetName(string name)
        {
            Console.WriteLine("Podaj imie: ");
            return name;
        }
    }
}
