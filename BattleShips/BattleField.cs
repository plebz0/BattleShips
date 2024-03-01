using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    internal class BattleField
    {
        public Field[,] board = new Field[10, 10];
        public void Render() {

            Console.WriteLine("   A|B|C|D|E|F|G|H|I|J|");
            for (int j = 0; j < 10; j++)
            {
                if (j == 9)
                {
                    Console.Write((j + 1) + "|");
                }
                else {
                    Console.Write(" " + (j + 1) + "|");
                }

                for (int i = 0; i < 10; i++)
                {
                    Console.Write(board[i, j].toChar() + "|");
                }
                Console.WriteLine("");
            }
        }
        public void RenderForEnemy() {

            Console.WriteLine("   A|B|C|D|E|F|G|H|I|J|");
            for (int j = 0; j < 10; j++)
            {
                if (j == 9)
                {
                    Console.Write((j + 1) + "|");
                }
                else {
                    Console.Write(" " + (j + 1) + "|");
                }

                for (int i = 0; i < 10; i++)
                {
                    Console.Write(board[i, j].toCharForEnemy() + "|");
                }
                Console.WriteLine("");
            }
        }
        public BattleField() {
            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = new Field();
                }

            }
        }
        public Field this[int x, int y]
        {
            get { return board[x, y]; }
        }
        public void markAround( (int, int) spot )
        { 
            for(int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    if(spot.Item1 + i >= 0 && spot.Item1 + i < 10 && spot.Item2 + j >= 0 && spot.Item2 + j < 10)
                        board[spot.Item1 + i, spot.Item2 + j].shoted = true;
                }
            }
        }
        public void markMissed( int x ,int y) {
            board[x, y].shoted = true;
        }
    }
}
