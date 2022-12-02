using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    internal class Board
    {
        public int size { get; set; }
        public Cell [,] TheGrid { get; set; }

        public Board(int s)
        {
            size = s;
            TheGrid = new Cell[s, s];
            for(int i = 0; i < s; i++)
            {
                for(int j=0;j<s; j++)
                {
                    TheGrid[i,j] = new Cell(i, j);
                }
            }
        }
        public int[] DxKnight = { -2, -2, 2, 2 };
        public int[] DyKnight = { 1, -1, 1, -1 };

        public int[] DxKing = { -1, -1, -1, 0, 0, 1, 1, 1 };
        public int[] DyKing = { 1, 0, -1, 1, -1, 1, 0, -1 };


        public bool valid(int x, int y)
        {
            return (x < size && x >= 0 && y < size && y >= 0 && TheGrid[x,y].IsBlank==false);
        }
        public void MakeNextLegalMoves(Cell currentCell)
        {
            switch (currentCell.CellName)
            {
                case "Knight":
                    for(int i = 0; i < 4; i++)
                    {
                        if (valid(currentCell.RowNumber + DxKnight[i], currentCell.ColumnNumber + DyKnight[i]))
                        {
                            TheGrid[currentCell.RowNumber + DxKnight[i], currentCell.ColumnNumber + DyKnight[i]].LegalNextMove = true;
                        }
                    }
                    break;

                case "King":
                    for(int i = 0; i < 8; i++)
                    {
                        if (valid(currentCell.RowNumber + DxKing[i], currentCell.ColumnNumber + DyKing[i]))
                        {
                            TheGrid[currentCell.RowNumber + DxKing[i], currentCell.ColumnNumber + DyKing[i]].LegalNextMove = true;
                        }
                    }
                    break;


            }
        }
    }
}
