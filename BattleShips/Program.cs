using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    internal class Program 
    {

        static void Main(string[] args)
        {
            GameHandler GH = new GameHandler();
            while(true) GH.RenderAttack();

            
            Console.ReadKey();
        }
    }
}
