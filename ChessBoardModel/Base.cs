using ChessBoardView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessBoardModel
{
    public class Base
    {
        public bool valid(int x, int y, int i, int j,int size, Cell[,] TheGrid)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                if (TheGrid[x, y].IsBlank != TheGrid[i, j].IsBlank) return true;
                else return false;
            }
            else return false;
        }
    }
}
