using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    internal class GameHandler
    {
        Player player1, player2;
        int turn = 0;
        public GameHandler() 
        { 
            player1 = new Player();
            player2 = new Player();
        }
        public void RenderAttack() {

            Player ActivePlayer = (turn % 2 == 0 ? ref player1 : ref player2);
            // if ActivePlayer jest kiomputer AI(); turn++; return; 

            //Console.WriteLine("Runda gracza " + ActivePlayer.name );
            Console.WriteLine($"Runda gracza {ActivePlayer.name}");

            Console.Write($"Statki: ");
            ActivePlayer.ships[0].RenderToUI();
            for ( int i = 1; i < ActivePlayer.ships.Length; i++ ) {
                Console.Write(", ");
                ActivePlayer.ships[i].RenderToUI();
            }




            turn++;
        }
        
    }
}
