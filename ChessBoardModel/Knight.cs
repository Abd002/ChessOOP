using ChessBoardView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessBoardModel
{
    public class Knight:Base
    {
        public int[] DxKnight = { 1, -1, -2, -2, 1, -1, 2, 2 };
        public int[] DyKnight = { -2, -2, 1, -1, 2, 2, 1, -1 };
        public void KnightLegalMovement(Cell currentCell, Cell[,]TheGrid, int size)
        {
            for (int i = 0; i < 8; i++)
            {
                if (valid(currentCell.RowNumber + DxKnight[i], currentCell.ColumnNumber + DyKnight[i], currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid))
                {
                    int x = (currentCell.RowNumber + DxKnight[i]);
                    int y = (currentCell.ColumnNumber + DyKnight[i]);
                    TheGrid[x, y].LegalNextMove = true;
                    TheGrid[x, y].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                }
            }
        }
    }
}
