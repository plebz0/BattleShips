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
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.Write((i + 1) + "|");
                }
                else {
                    Console.Write(" " + (i + 1) + "|");
                }

                for (int j = 0; j < 10; j++)
                {
                    Console.Write(board[i, j].toChar() + "|");
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
        public void mark( (int, int) spot )
        { 
            for(int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    board[spot.Item1 + i, spot.Item2 + j].shoted = true;
                }
            }
        }
    }
}
