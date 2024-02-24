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
            player1.name = "jeden";
            player2 = new Player();
            player2.name = "dwa";

        }
        public void RenderAttack() {
            Console.Clear();
            Console.WriteLine("\x1b[3J");


            Player ActivePlayer = (turn % 2 == 0 ? ref player1 : ref player2);
            Player EnemyPlayer = (turn % 2 == 1 ? ref player1 : ref player2);
            // if ActivePlayer jest kiomputer AI(); turn++; return; 

            //Console.WriteLine("Runda gracza " + ActivePlayer.name );
            Console.WriteLine($"Runda gracza {ActivePlayer.name}");
            Console.WriteLine();
            Console.WriteLine($"Twoja plansza:");
            ActivePlayer.bf.Render();
            Console.Write($"Twoje Statki: ");
            ActivePlayer.ships[0].RenderToUI();
            for ( int i = 1; i < ActivePlayer.ships.Length; i++ ) {
                Console.Write(", ");
                ActivePlayer.ships[i].RenderToUI();
            }
            Console.WriteLine("");

            Console.WriteLine();
            Console.WriteLine($"Plansza Przeciwnika:");
            EnemyPlayer.bf.Render();
            Console.WriteLine("");

            while (true)
            {

                int x = 0;
                int y = 0;
                string inputx = " ";
                string inputy = " ";
                while (inputx == " ")
                {

                    Console.WriteLine(
                    "Podaj x (Litery)");
                    inputx = Console.ReadLine();
                    if (inputx == "A" || inputx == "a")
                    {
                        x = 0;
                    }
                    else if (inputx == "b" || inputx == "B")
                    {
                        x = 1;
                    }
                    else if (inputx == "c" || inputx == "C")
                    {
                        x = 2;
                    }
                    else if (inputx == "d" || inputx == "D")
                    {
                        x = 3;
                    }
                    else if (inputx == "e" || inputx == "E")
                    {
                        x = 4;
                    }
                    else if (inputx == "f" || inputx == "F")
                    {
                        x = 5;
                    }
                    else if (inputx == "g" || inputx == "G")
                    {
                        x = 6;
                    }
                    else if (inputx == "h" || inputx == "H")
                    {
                        x = 7;
                    }
                    else if (inputx == "i" || inputx == "I")
                    {
                        x = 8;
                    }
                    else if (inputx == "j" || inputx == "J")
                    {
                        x = 9;
                    }
                    else
                    {
                        inputx = " ";
                        Console.WriteLine("Podano złą wartość spróbuj ponownie");
                    }
                }
                while (true)
                {

                    Console.WriteLine("Podaj y (Liczby)");
                    inputy = Console.ReadLine();
                    try
                    {
                        y = Int32.Parse(inputy);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Podano złą wartość spróbuj ponownie");
                        //Console.WriteLine(e.Message);
                        continue;
                    }
                    if (y < 1 || y > 10)
                    {
                        Console.WriteLine("Podano wratość spoza zakresu");
                        continue;
                    }

                    break;
                }
                y--;

                Player.ShotInfo si = EnemyPlayer.getShot(x, y);

                if (si == Player.ShotInfo.MISSED)
                {
                    Console.Clear();
                    Console.WriteLine("\x1b[3J");
                    Console.WriteLine("Pudlo, oddaj klawiature drugiemu graczowi!");
                    Console.ReadKey();
                    turn++;
                    return;
                }

                if (si == Player.ShotInfo.RESHOOT)
                {
                    Console.WriteLine("Juz tutaj strzelales!");
                    continue;
                }

                if (si == Player.ShotInfo.HIT)
                {
                    Console.WriteLine("Trafiony, strzelaj dalej!");
                    continue;
                }

                if (si == Player.ShotInfo.SINKED)
                {
                    Console.WriteLine("Trafiony Zatopiony! Strzelaj Dalej!");
                    Ship hitShip = EnemyPlayer.bf[x, y].shipOver;
                    for (int i = 0; i < hitShip.holes; i++)
                    {
                        EnemyPlayer.bf.mark(hitShip.cords[i]);
                    }
                    continue;
                }
            }

        }
        
    }
}
