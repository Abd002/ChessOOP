using ChessBoardView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessBoardModel
{
    public class Rook:Base
    {
        public void RookLegalMovement(Cell currentCell, Cell[,]TheGrid, int size)
        {
            int a = currentCell.RowNumber + 1, b = currentCell.ColumnNumber;
            while (valid(a, b, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid))
            {
                TheGrid[a, b].LegalNextMove = true;
                TheGrid[a, b].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);

                if (TheGrid[a, b].IsBlank != 0) break;
                a++;
            }
            a = currentCell.RowNumber - 1; b = currentCell.ColumnNumber;
            while (valid(a, b, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid))
            {
                TheGrid[a, b].LegalNextMove = true;
                TheGrid[a, b].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);

                if (TheGrid[a, b].IsBlank != 0) break;
                a--;
            }
            a = currentCell.RowNumber; b = currentCell.ColumnNumber + 1;
            while (valid(a, b, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid))
            {
                TheGrid[a, b].LegalNextMove = true;
                TheGrid[a, b].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);

                if (TheGrid[a, b].IsBlank != 0) break;
                b++;
            }
            a = currentCell.RowNumber; b = currentCell.ColumnNumber - 1;
            while (valid(a, b, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid))
            {
                TheGrid[a, b].LegalNextMove = true;
                TheGrid[a, b].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);

                if (TheGrid[a, b].IsBlank != 0) break;
                b--;
            }
        }
    }
}
