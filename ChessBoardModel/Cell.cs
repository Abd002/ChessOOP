using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessBoardView
{
    public class Cell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }

        public string CellName { get; set; }

        public int IsBlank { get; set; }


        public bool LegalNextMove { get; set; }
        public Point LegalNextMovePastPoint { get; set; }

        public bool KingMove { get; set; }
        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
            IsBlank = 0;
            LegalNextMove = false;
        }
    }
}

