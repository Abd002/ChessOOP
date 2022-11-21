using ChessBoardModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessBoardView
{
    public class Board:Base
    {
        public int size { get; set; }
        public Cell[,] TheGrid { get; set; }

        public Knight knight = new Knight();
        public Bishop bishop = new Bishop();
        public Rook rook=new Rook();
        public Queen queen = new Queen();
        public King king = new King();
        public Pawn pawn = new Pawn();


        public Board(int s)
        {
            size = s;
            TheGrid = new Cell[s, s];
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    TheGrid[i, j] = new Cell(i, j);
                }
            }

            placeAllPieces();

        }


        public void MakeNextLegalMoves(Cell currentCell)
        {
            switch (currentCell.CellName)
            {
                case "Knight":
                    knight.KnightLegalMovement(currentCell, TheGrid, size);
                    break;

                case "King":
                    KingLegalMovement(currentCell);
                    break;
                case "Bishop":
                    bishop.BishopLegalMovement(currentCell, TheGrid, size);

                    break;
                case "Rook":
                    rook.RookLegalMovement(currentCell, TheGrid, size);

                    break;
                case "Queen":
                    queen.LegalQueenMovement(currentCell, TheGrid, size);
                    KingLegalMovement(currentCell);
                    break;

                case "Pawn":
                    pawn.PawnLegalMovement(currentCell,TheGrid, size);
                    break;

            }
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TheGrid[i, j].LegalNextMove = false;
                }
            }
        }




        private void KingCheckFunction(Cell currentCell)
        {

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (currentCell.IsBlank != TheGrid[i, j].IsBlank && TheGrid[i, j].CellName == "Pawn")
                    {
                        if (TheGrid[i, j].IsBlank == 1)
                        {
                            if (valid(TheGrid[i, j].RowNumber - 1, TheGrid[i, j].ColumnNumber - 1, TheGrid[i, j].RowNumber, TheGrid[i, j].ColumnNumber, size, TheGrid))
                            {
                                TheGrid[TheGrid[i, j].RowNumber - 1, TheGrid[i, j].ColumnNumber - 1].LegalNextMove = true;
                            }
                            if (valid(TheGrid[i, j].RowNumber + 1, TheGrid[i, j].ColumnNumber - 1, TheGrid[i, j].RowNumber, TheGrid[i, j].ColumnNumber, size, TheGrid))
                            {
                                TheGrid[TheGrid[i, j].RowNumber + 1, TheGrid[i, j].ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            if (valid(TheGrid[i, j].RowNumber - 1, TheGrid[i, j].ColumnNumber + 1, TheGrid[i, j].RowNumber, TheGrid[i, j].ColumnNumber, size, TheGrid))
                            {
                                TheGrid[TheGrid[i, j].RowNumber - 1, TheGrid[i, j].ColumnNumber + 1].LegalNextMove = true;
                            }
                            if (valid(TheGrid[i, j].RowNumber + 1, TheGrid[i, j].ColumnNumber + 1, TheGrid[i, j].RowNumber, TheGrid[i, j].ColumnNumber, size, TheGrid))
                            {
                                TheGrid[TheGrid[i, j].RowNumber + 1, TheGrid[i, j].ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    else if (currentCell.IsBlank != TheGrid[i, j].IsBlank && TheGrid[i, j].CellName != "King")
                    {
                        MakeNextLegalMoves(TheGrid[i, j]);
                    }
                }
            }

        }
        private int[] DyKing = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private int[] DxKing = { 1, 0, -1, 1, -1, 1, 0, -1 };

        private void KingLegalMovement(Cell currentCell)
        {
            if (currentCell.CellName != "Queen")
            {
                KingCheckFunction(currentCell);


                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        TheGrid[i, j].KingMove = false;
                        TheGrid[i, j].KingMove = TheGrid[i, j].LegalNextMove;
                        TheGrid[i, j].LegalNextMove = false;
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (valid(currentCell.RowNumber + DxKing[i], currentCell.ColumnNumber + DyKing[i], currentCell.RowNumber, currentCell.ColumnNumber, size, TheGrid) && TheGrid[currentCell.RowNumber + DxKing[i], currentCell.ColumnNumber + DyKing[i]].KingMove == false)
                {
                    TheGrid[currentCell.RowNumber + DxKing[i], currentCell.ColumnNumber + DyKing[i]].LegalNextMove = true;
                    TheGrid[currentCell.RowNumber + DxKing[i], currentCell.ColumnNumber + DyKing[i]].LegalNextMovePastPoint = new Point(currentCell.RowNumber, currentCell.ColumnNumber);

                }
            }
        }

        private void placeAllPieces()
        {
            for (int i = 0; i < size; i++)
            {
                TheGrid[i, 6].CellName = "Pawn";
                TheGrid[i, 6].IsBlank = 1;
            }

            for (int i = 0; i < size; i++)
            {
                TheGrid[i, 1].CellName = "Pawn";
                TheGrid[i, 1].IsBlank = 2;
            }

            TheGrid[2, 7].CellName = "Bishop";
            TheGrid[2, 7].IsBlank = 1;
            TheGrid[5, 7].CellName = "Bishop";
            TheGrid[5, 7].IsBlank = 1;
            TheGrid[2, 0].CellName = "Bishop";
            TheGrid[2, 0].IsBlank = 2;
            TheGrid[5, 0].CellName = "Bishop";
            TheGrid[5, 0].IsBlank = 2;

            TheGrid[1, 7].CellName = "Knight";
            TheGrid[1, 7].IsBlank = 1;
            TheGrid[6, 7].CellName = "Knight";
            TheGrid[6, 7].IsBlank = 1;
            TheGrid[1, 0].CellName = "Knight";
            TheGrid[1, 0].IsBlank = 2;
            TheGrid[6, 0].CellName = "Knight";
            TheGrid[6, 0].IsBlank = 2;

            TheGrid[0, 7].CellName = "Rook";
            TheGrid[0, 7].IsBlank = 1;
            TheGrid[7, 7].CellName = "Rook";
            TheGrid[7, 7].IsBlank = 1;
            TheGrid[0, 0].CellName = "Rook";
            TheGrid[0, 0].IsBlank = 2;
            TheGrid[7, 0].CellName = "Rook";
            TheGrid[7, 0].IsBlank = 2;

            TheGrid[4, 7].CellName = "King";
            TheGrid[4, 7].IsBlank = 1;
            TheGrid[4, 0].CellName = "King";
            TheGrid[4, 0].IsBlank = 2;

            TheGrid[3, 7].CellName = "Queen";
            TheGrid[3, 7].IsBlank = 1;
            TheGrid[3, 0].CellName = "Queen";
            TheGrid[3, 0].IsBlank = 2;
        }


    }
}
