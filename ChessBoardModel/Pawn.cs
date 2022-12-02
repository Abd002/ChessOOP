using ChessBoardView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessBoardModel
{
    public class Pawn:Base
    {
        public void PawnLegalMovement(Cell currentCell, Cell[,]TheGrid, int size)
        {
            if (currentCell.IsBlank == 1)
            {
                if (valid(currentCell.RowNumber, currentCell.ColumnNumber - 1, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].IsBlank == 0)
                {
                    TheGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                    if (valid(currentCell.RowNumber, currentCell.ColumnNumber - 2, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && currentCell.ColumnNumber == 6 && TheGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].IsBlank == 0)
                    {
                        TheGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].LegalNextMove = true;
                        TheGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                    }
                }
                if (valid(currentCell.RowNumber - 1, currentCell.ColumnNumber - 1, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].IsBlank == 2)
                {
                    TheGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                }
                if (valid(currentCell.RowNumber + 1, currentCell.ColumnNumber - 1, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].IsBlank == 2)
                {
                    TheGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                }


            }
            else
            {
                if (valid(currentCell.RowNumber, currentCell.ColumnNumber + 1, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].IsBlank == 0)
                {
                    TheGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                    if (valid(currentCell.RowNumber, currentCell.ColumnNumber + 2, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && currentCell.ColumnNumber == 1 && TheGrid[currentCell.RowNumber, currentCell.ColumnNumber + 2].IsBlank == 0)
                    {
                        TheGrid[currentCell.RowNumber, currentCell.ColumnNumber + 2].LegalNextMove = true;
                        TheGrid[currentCell.RowNumber, currentCell.ColumnNumber + 2].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                    }
                }
                if (valid(currentCell.RowNumber - 1, currentCell.ColumnNumber + 1, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].IsBlank == 1)
                {
                    TheGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                }
                if (valid(currentCell.RowNumber + 1, currentCell.ColumnNumber + 1, currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].IsBlank == 1)
                {
                    TheGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);
                }
            }
        }
    }
}
