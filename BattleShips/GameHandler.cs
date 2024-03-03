using System;
using System.CodeDom;
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

        public void askRematch() {
            Console.WriteLine($"{player1.name}: {player1.wins} wygranych");
            Console.WriteLine($"{player2.name}: {player2.wins} wygranych");
            Console.WriteLine("Czy chcesz zagrać ponownie? (T/N)");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.T) {
                setRematch();
            } else {
                return;
            }
        }

        public void setRematch() {
            player1.resetBattleField();
            player1.resetShips();

            player2.resetBattleField();
            player2.resetShips();

            PlaceShips(player1);

            if(player2.isAI)
            {
                AiPlaceShips();
            }
            else
            {
                PlaceShips(player2);
            }

            StartBattle();
        }

        public void start() {
            Console.Clear();
            Console.Write("\x1b[3J");
            int selection = 0;
            do
            {
                Console.WriteLine($" Statki zrobione przez: Dawid\n");
                Console.WriteLine($"");

                Console.WriteLine($" {(selection == 0 ? '>' : ' ')} Gracz vs Komputer");
                Console.WriteLine($" {(selection == 1 ? '>' : ' ')} Gracz vs Gracz");
                Console.WriteLine($" {(selection == 2 ? '>' : ' ')} Wyjscie");

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
                            Console.WriteLine("Nie możesz tak");
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

            Console.Write("Imię Gracza: ");
            player1.name = Console.ReadLine();
            player1.wins = 0;
            player1.isAI = false;
            player1.resetBattleField();
            player1.resetShips();

            PlaceShips(player1);

            player2.name = "Komputer";
            player2.wins = 0;
            player2.isAI = true;
            player2.resetBattleField();
            player2.resetShips();

            AiPlaceShips();

            StartBattle();
        }

        public void setMultiplayer()
        { 
            Console.Write("Imię Gracza 1: ");
            player1.name = Console.ReadLine();
            player1.wins = 0;
            player1.isAI = false;
            player1.resetBattleField();
            player1.resetShips();

            Console.Write("Imię Gracza 2: ");
            player2.name = Console.ReadLine();
            player2.wins = 0;
            player2.isAI = false;
            player2.resetBattleField();
            player2.resetShips();


            PlaceShips(player1);
            PlaceShips(player2);

            StartBattle();
        }

        public void StartBattle() {
            bool isWin = false;
            do {
                RenderAttack();

                if (player1.isDefeated()) {
                    isWin = true;
                } else if (player2.isDefeated()) {
                    isWin = true;
                }

            } while (!isWin);

            Console.Clear();
            Console.Write("\x1b[3J");

            Console.WriteLine("Koniec gry!");
            Console.WriteLine($"{player1.name}: {player1.wins} wygranych");
            Console.WriteLine($"{player2.name}: {player2.wins} wygranych");
            Console.WriteLine("Nacisnij dowolny klawisz aby kontunuowac...");
            Console.ReadKey();

            Console.Clear();
            Console.Write("\x1b[3J");
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
                    Console.WriteLine();

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
                                        if(x + rx >= 0 && x + (r == 0 ? s : 0) + rx< 10 && y + ry >= 0 && y + (r == 0 ? 0 : s) + ry < 10)
                                            if(activePlayer.bf[x + (r == 0 ? s : 0) + rx, y + (r == 0 ? 0 : s) + ry].isShip)
                                                isOccupied = true;
                                    }
                                }
                            }
                            if(isOccupied)
                            {
                                Console.WriteLine("Nie możesz postawić tutaj ststku!");
                                continue;
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

        public void AiPlaceShips()
        {
            for(int i = 0; i < player2.ships.Length; i++)
            {
                bool correctPlacement = false;
                int x = 0, y = 0, r = 0; // r=0 ----, r=1 |
                
                do
                {
                    Random random = new Random();

                    r = random.Next(0, 2);
                    if(r == 0)
                    {
                        x = random.Next(0, 10 - player2.ships[i].holes);
                        y = random.Next(0, 10);
                    } else
                    {
                        x = random.Next(0, 10);
                        y = random.Next(0, 10 - player2.ships[i].holes);
                    }

                    bool isOccupied = false;
                    for(int s = 0; s < player2.ships[i].holes; s++)
                    {
                        for(int rx = -1; rx < 2; rx++)
                        {
                            for(int ry = -1; ry < 2; ry++)
                            {
                                if(x + rx >= 0 && x + (r == 0 ? s : 0) + rx< 10 && y + ry >= 0 && y + (r == 0 ? 0 : s) + ry < 10)
                                    if(player2.bf[x + (r == 0 ? s : 0) + rx, y + (r == 0 ? 0 : s) + ry].isShip)
                                        isOccupied = true;
                            }
                        }
                    }
                    if(isOccupied)
                    {
                        continue;
                    }

                    for(int s = 0; s < player2.ships[i].holes; s++)
                    {
                        player2.ships[i].cords[s] = (x + (r == 0 ? s : 0), y + (r == 0 ? 0 : s));
                    }

                    correctPlacement = !isOccupied;

                } while (!correctPlacement);
                for(int s = 0; s < player2.ships[i].holes; s++)
                {
                    int rx = x + (r == 0 ? s : 0);
                    int ry = y + (r == 0 ? 0 : s);
                    player2.bf[rx, ry].isShip = true;
                    player2.bf[rx, ry].shipOver = player2.ships[i];
                    player2.bf[rx, ry].segmentIndex = s;
                }
            }
        }

        public void AiShoot()
        {
            while (true) {
                
                Random random = new Random();
                int goodShotx = -1;
                int goodShoty = -1;
                bool up = false;
                bool down = false;
                bool left = false;
                bool right = false;
                int r = random.Next(1, 4);
                int x = random.Next(0, 10);
                int y = random.Next(0, 10);

                if (goodShotx != -1 && goodShoty != -1)
                {
                    x = goodShotx;
                    y = goodShoty;
                    if (r == 1 && x != 0 )
                    {
                        x -= 1;
                        up = true;
                    }
                    else if (r == 2 && x != 9)
                    {
                        x += 1;
                        down = true;
                    }
                    else if (r == 3 && y != 0)
                    {
                        y -= 1;
                        left = true;
                    }
                    else if (r == 4 && y != 9)
                    {
                        y += 1;
                        right = true;
                    }


                }


                Player.ShotInfo si = player1.getShot(x, y);
                if (si == Player.ShotInfo.MISSED) {
                    
                    break;
                }

                if (si == Player.ShotInfo.RESHOOT) {
                    continue;
                }

                if (si == Player.ShotInfo.HIT) {
                    goodShotx = x;
                    goodShoty = y;
                    continue;
                }

                if (si == Player.ShotInfo.SINKED) {
                    goodShotx = -1;
                    goodShoty = -1;
                  /*  up = false;
                    down = false;
                    left = false;
                    right = false;
*/
                    Ship hitShip = player1.bf[x, y].shipOver;

                    for (int i = 0; i < hitShip.holes; i++) {
                        player1.bf.markAround(hitShip.cords[i]);
                    }

                    if (player1.isDefeated()) 
                    {
                        Console.WriteLine("Gracz " + player2.name + " wygrywa!");
                        player2.wins++;
                        askRematch();
                        break;
                    } else {
                        continue;
                    }
                }
            }
            Console.Clear();
            Console.Write("\x1b[3J");
        }

        public void RenderAttack() {

            Player ActivePlayer = (turn % 2 == 0 ? ref player1 : ref player2);
            Player EnemyPlayer = (turn % 2 == 1 ? ref player1 : ref player2);

            if(ActivePlayer.isAI)
            {
                AiShoot();
                turn++;
                return;
            }
            
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.WriteLine($"Runda gracza {ActivePlayer.name}");

            while (true) {

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
                EnemyPlayer.bf.RenderForEnemy();

                Console.Write($"Statki Przeciwnika: ");
                EnemyPlayer.ships[0].RenderToUIForEnemy();
                for (int i = 1; i < EnemyPlayer.ships.Length; i++) {
                    Console.Write(", ");
                    EnemyPlayer.ships[i].RenderToUIForEnemy();
                }
                Console.WriteLine("");

                
                Console.WriteLine();
                int x = 0;
                int y = 0;
                string inputx = " ";
                string inputy = " ";
                while (inputx == " ") {

                    Console.WriteLine(
                    "Podaj x (Litery)");
                    inputx = Console.ReadLine();
                    if (inputx == "a" || inputx == "A") {
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

                Console.Clear();
                Console.Write("\x1b[3J");

                if (si == Player.ShotInfo.MISSED) {

                    EnemyPlayer.bf.markMissed(x, y);
                    turn++;
                    Console.WriteLine("Pudlo, oddaj klawiature drugiemu graczowi!");

                    if(EnemyPlayer.isAI) return;

                    Console.ReadKey();
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

                    Ship hitShip = EnemyPlayer.bf[x, y].shipOver;

                    for (int i = 0; i < hitShip.holes; i++) {
                          
                        EnemyPlayer.bf.markAround(hitShip.cords[i]);
                    }

                    if (EnemyPlayer.isDefeated()) 
                    {
                        Console.WriteLine("Gracz " + ActivePlayer.name + " wygrywa!");
                        ActivePlayer.wins++;
                        askRematch();
                        return;
                    } else {
                        Console.WriteLine("Trafiony zatopiony, strzelaj dalej!");
                        continue;
                    }
                }
            }

        }

    }
    
}
