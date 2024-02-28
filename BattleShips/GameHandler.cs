using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips {
    internal class GameHandler {
        Player player1, player2;
        int turn = 0;

        public GameHandler() {
            player1 = new Player();
            player2 = new Player();
        }

        public void start() {
            Console.Clear();
            Console.Write("\x1b[3J");
            int selection = 0;
            do
            {
                Console.WriteLine($"BattleShipts by: Dawid\n");
                Console.WriteLine($"");

                Console.WriteLine($" {(selection == 0 ? '>' : ' ')} Singleplayer");
                Console.WriteLine($" {(selection == 1 ? '>' : ' ')} Multiplater");
                Console.WriteLine($" {(selection == 2 ? '>' : ' ')} Exit");

                ConsoleKeyInfo key = Console.ReadKey();

                Console.Clear();
                Console.Write("\x1b[3J");

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                } else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    switch(selection)
                    {
                        case 0:
                            setSingleplayer();
                            selection = 0;
                            break;
                        case 1:
                            setMultiplayer();
                            selection = 0;
                            break;
                        case 2:
                            return;
                        default:
                            Console.WriteLine("Unidentified selection?!?");
                        break;
                    }
                } else if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    selection--;
                    if (selection < 0)
                    {
                        selection = 2;
                    }
                } else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    selection = (++selection) % 3;
                }
            } while (true);
        }

        public void setSingleplayer() 
        { 
            Console.WriteLine("Singleplayer jeszcze nie wspierany lol");
            return;

            
            Console.Write("Imię Gracza: ");
            player1.name = Console.ReadLine();
            PlaceShips(player1);

            player2.name = "Komputer";
            //player2.isAI = true;
            //player2.AiShipPlace();
        }

        public void setMultiplayer()
        { 
            Console.Write("Imię Gracza 1: ");
            player1.name = Console.ReadLine();

            Console.Write("Imię Gracza 2: ");
            player2.name = Console.ReadLine();

            PlaceShips(player1);
            PlaceShips(player2);

            StartBattle();
        }

        public void StartBattle() {
            bool isWin = false;
            do {
                RenderAttack();

                if (player1.isDefeated()) {
                    Console.WriteLine("Gracz " + player2.name + " wygrywa!");
                    isWin = true;
                } else if (player2.isDefeated()) {
                    Console.WriteLine("Gracz " + player1.name + " wygrywa!");
                    isWin = true;
                }

            } while (!isWin);
        }

        public void PlaceShips(Player activePlayer)
        {
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.WriteLine($"Gracz {activePlayer.name} ustawia swoje statki!\nNacisnij dowonly klawisz aby kontunuowac...");

            Console.ReadKey();

            for(int i = 0; i < activePlayer.ships.Length; i++)
            {
                bool correctPlacement = false;
                int x = 0, y = 0, r = 0; // r=0 ----, r=1 |

                for(int s = 0; s < activePlayer.ships[i].holes; s++)
                {
                    activePlayer.ships[i].cords[s] = (s, 0);
                }

                do
                {
                    Console.Clear();
                    Console.Write("\x1b[3J");

                    Console.WriteLine("   A|B|C|D|E|F|G|H|I|J|");
                    for(int ry = 0; ry < 10; ry++)
                    {
                        if (ry == 9)
                        {
                            Console.Write((ry + 1) + "|");
                        }
                        else {
                            Console.Write(" " + (ry + 1) + "|");
                        }

                        for(int rx = 0; rx < 10; rx++)
                        {
                            bool isShip = false;
                            for(int s = 0; s < activePlayer.ships[i].holes; s++)
                            {
                                if(activePlayer.ships[i].cords[s] == (rx, ry))
                                {
                                    isShip = true;
                                    break;
                                }
                            }

                            if(isShip)
                            {
                                Console.Write("$|");
                            } else
                            {
                                Console.Write(activePlayer.bf[rx, ry].toChar() + "|");
                            }
                        }
                        Console.WriteLine("");
                    }

                    ConsoleKeyInfo key = Console.ReadKey(); 

                    switch(key.Key)
                    {
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            if(y > 0)  y--;
                            break;

                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            if(y < 9 - (r == 0 ? 0 : activePlayer.ships[i].holes - 1)) 
                                y++;
                            break;

                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            if(x > 0) x--;
                            break;
                            
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            if(x < 9 - (r == 0 ? activePlayer.ships[i].holes - 1 : 0))
                                x++;
                            break;

                        case ConsoleKey.R:
                            r = ++r % 2;
                            x = Math.Min(x, 9 - (r == 1 ? 0 : activePlayer.ships[i].holes - 1));
                            y = Math.Min(y, 9 - (r == 1 ? activePlayer.ships[i].holes - 1 : 0));
                            break;

                        case ConsoleKey.Enter:
                            bool isOccupied = false;
                            for(int s = 0; s < activePlayer.ships[i].holes; s++)
                            {
                                for(int rx = -1; rx < 2; rx++)
                                {
                                    for(int ry = -1; ry < 2; ry++)
                                    {
                                        if(x + rx >= 0 && x + rx < 10 && y + ry >= 0 && y + ry < 10)
                                            if(activePlayer.bf[x + (r == 0 ? s : 0) + rx, y + (r == 0 ? 0 : s) + ry].isShip)
                                                isOccupied = true;
                                    }
                                }
                            }
                            correctPlacement = !isOccupied;
                            break;
                    }

                    for(int s = 0; s < activePlayer.ships[i].holes; s++)
                    {
                        activePlayer.ships[i].cords[s] = (x + (r == 0 ? s : 0), y + (r == 0 ? 0 : s));
                    }

                } while (!correctPlacement);

                for(int s = 0; s < activePlayer.ships[i].holes; s++)
                {
                    int rx = x + (r == 0 ? s : 0);
                    int ry = y + (r == 0 ? 0 : s);
                    activePlayer.bf[rx, ry].isShip = true;
                    activePlayer.bf[rx, ry].shipOver = activePlayer.ships[i];
                    activePlayer.bf[rx, ry].segmentIndex = s;
                }
            }
        }

        public void RenderAttack() {
            Console.Clear();
            Console.Write("\x1b[3J");


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
            for (int i = 1; i < ActivePlayer.ships.Length; i++) {
                Console.Write(", ");
                ActivePlayer.ships[i].RenderToUI();
            }
            Console.WriteLine("");

            Console.WriteLine();
            Console.WriteLine($"Plansza Przeciwnika:");
            EnemyPlayer.bf.Render();
            Console.WriteLine("");

            while (true) {

                int x = 0;
                int y = 0;
                string inputx = " ";
                string inputy = " ";
                while (inputx == " ") {

                    Console.WriteLine(
                    "Podaj x (Litery)");
                    inputx = Console.ReadLine();
                    if (inputx == "A" || inputx == "a") {
                        x = 0;
                    } else if (inputx == "b" || inputx == "B") {
                        x = 1;
                    } else if (inputx == "c" || inputx == "C") {
                        x = 2;
                    } else if (inputx == "d" || inputx == "D") {
                        x = 3;
                    } else if (inputx == "e" || inputx == "E") {
                        x = 4;
                    } else if (inputx == "f" || inputx == "F") {
                        x = 5;
                    } else if (inputx == "g" || inputx == "G") {
                        x = 6;
                    } else if (inputx == "h" || inputx == "H") {
                        x = 7;
                    } else if (inputx == "i" || inputx == "I") {
                        x = 8;
                    } else if (inputx == "j" || inputx == "J") {
                        x = 9;
                    } else {
                        inputx = " ";
                        Console.WriteLine("Podano złą wartość spróbuj ponownie");
                    }
                }
                while (true) {

                    Console.WriteLine("Podaj y (Liczby)");
                    inputy = Console.ReadLine();
                    try {
                        y = Int32.Parse(inputy);
                    } catch (Exception e) {
                        Console.WriteLine("Podano złą wartość spróbuj ponownie");
                        //Console.WriteLine(e.Message);
                        continue;
                    }
                    if (y < 1 || y > 10) {
                        Console.WriteLine("Podano wratość spoza zakresu");
                        continue;
                    }

                    break;
                }
                y--;

                Player.ShotInfo si = EnemyPlayer.getShot(x, y);

                if (si == Player.ShotInfo.MISSED) {
                    Console.Clear();
                    Console.WriteLine("\x1b[3J");
                    EnemyPlayer.bf.markMissed(x, y);
                    Console.WriteLine("Pudlo, oddaj klawiature drugiemu graczowi!");
                    
                    Console.ReadKey();
                    turn++;
                    return;
                }

                if (si == Player.ShotInfo.RESHOOT) {
                    Console.WriteLine("Juz tutaj strzelales!");
                    continue;
                }

                if (si == Player.ShotInfo.HIT) {
                    Console.WriteLine("Trafiony, strzelaj dalej!");
                    continue;
                }

                if (si == Player.ShotInfo.SINKED) {
                    Console.WriteLine("Trafiony Zatopiony! Strzelaj Dalej!");
                    Ship hitShip = EnemyPlayer.bf[x, y].shipOver;
                    for (int i = 0; i < hitShip.holes; i++) {
                        EnemyPlayer.bf.markAround(hitShip.cords[i]);
                    }
                    continue;
                }
            }

        }

    }
    
}
