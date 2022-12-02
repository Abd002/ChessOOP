using ChessBoardView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Queen
    {
        public Bishop bishop = new Bishop();
        public Rook rook = new Rook();

        public void LegalQueenMovement(Cell currentCell, Cell[,] TheGrid, int size)
        {
            rook.RookLegalMovement(currentCell, TheGrid, size);
            bishop.BishopLegalMovement(currentCell, TheGrid, size);
        }
    }
}
