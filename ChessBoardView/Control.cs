using ChessBoardModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace ChessBoardView
{
    public class Control:Base
    {
        public int MovingInChess(Board myBoard, Point location, int PlayerTurn)
        {
            int x = (int)myBoard.TheGrid[location.X, location.Y].LegalNextMovePastPoint.X;
            int y = (int)myBoard.TheGrid[location.X, location.Y].LegalNextMovePastPoint.Y;

            myBoard.TheGrid[location.X, location.Y] = myBoard.TheGrid[x, y];
            myBoard.TheGrid[location.X, location.Y].RowNumber = location.X;
            myBoard.TheGrid[location.X, location.Y].ColumnNumber = location.Y;

            myBoard.TheGrid[x, y] = new Cell(x, y);
            PlayerTurn = (3 ^ myBoard.TheGrid[location.X, location.Y].IsBlank);
            return PlayerTurn;
        }

        public void Display(Board myBoard, Panel[,] pnlGrid, Dictionary<string, Bitmap> imageValue)
        {
            for (int i = 0; i < myBoard.size; i++)
            {
                for (int j = 0; j < myBoard.size; j++)
                {
                    if ((i + j) % 2 == 0) pnlGrid[i, j].BackColor = Color.White;
                    else pnlGrid[i, j].BackColor = Color.Gray;
                    if (myBoard.TheGrid[i, j].LegalNextMove == true)
                    {
                        pnlGrid[i, j].BackColor = Color.Green;
                    }
                    else
                    {
                        if (myBoard.TheGrid[i, j].imageKey != "NULL")
                        {
                            pnlGrid[i, j].BackgroundImage = imageValue[myBoard.TheGrid[i, j].imageKey];
                            pnlGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else
                        {
                            pnlGrid[i, j].BackgroundImage = Properties.Resources.Empty;
                        }
                    }
                }
            }
        }

        public Point KingCheck(Board Temp, int PlayerTurn)
        {

            int x=0, y=0;
            for (int i = 0; i < Temp.size; i++)
            {
                for (int j = 0; j < Temp.size; j++)
                {
                    if (Temp.TheGrid[i,j].CellName=="King"&& Temp.TheGrid[i, j].IsBlank == PlayerTurn)
                    {
                        x = i;y = j;
                    }
                }
            }


            for (int i = 0; i < Temp.size; i++)
            {
                for(int j = 0; j < Temp.size; j++)
                {
                    if (PlayerTurn != Temp.TheGrid[i, j].IsBlank && Temp.TheGrid[i, j].CellName == "Pawn")
                    {
                        if (Temp.TheGrid[i, j].IsBlank == 1)
                        {
                            if (valid(Temp.TheGrid[i, j].RowNumber - 1, Temp.TheGrid[i, j].ColumnNumber - 1, Temp.TheGrid[i, j].RowNumber, Temp.TheGrid[i, j].ColumnNumber, Temp.size, Temp.TheGrid))
                            {
                                Temp.TheGrid[Temp.TheGrid[i, j].RowNumber - 1, Temp.TheGrid[i, j].ColumnNumber - 1].LegalNextMove = true;
                            }
                            if (valid(Temp.TheGrid[i, j].RowNumber + 1, Temp.TheGrid[i, j].ColumnNumber - 1, Temp.TheGrid[i, j].RowNumber, Temp.TheGrid[i, j].ColumnNumber, Temp.size, Temp.TheGrid))
                            {
                                Temp.TheGrid[Temp.TheGrid[i, j].RowNumber + 1, Temp.TheGrid[i, j].ColumnNumber - 1].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            if (valid(Temp.TheGrid[i, j].RowNumber - 1, Temp.TheGrid[i, j].ColumnNumber + 1, Temp.TheGrid[i, j].RowNumber, Temp.TheGrid[i, j].ColumnNumber, Temp.size, Temp.TheGrid))
                            {
                                Temp.TheGrid[Temp.TheGrid[i, j].RowNumber - 1, Temp.TheGrid[i, j].ColumnNumber + 1].LegalNextMove = true;
                            }
                            if (valid(Temp.TheGrid[i, j].RowNumber + 1, Temp.TheGrid[i, j].ColumnNumber + 1, Temp.TheGrid[i, j].RowNumber, Temp.TheGrid[i, j].ColumnNumber, Temp.size, Temp.TheGrid))
                            {
                                Temp.TheGrid[Temp.TheGrid[i, j].RowNumber + 1, Temp.TheGrid[i, j].ColumnNumber + 1].LegalNextMove = true;
                            }
                        }
                    }
                    else if (Temp.TheGrid[i,j].IsBlank!=PlayerTurn&& Temp.TheGrid[i, j].CellName != "King")
                    {
                        Temp.MakeNextLegalMoves(Temp.TheGrid[i, j]);
                    }

                    if (Temp.TheGrid[x, y].LegalNextMove)
                    {
                        Temp.Clear(Temp);
                        return new Point(i, j);
                    }
                }
            }
            Temp.Clear(Temp);
            return new Point(-1, 0);

            
        }

        private bool IsItLose(Board myBoard, int x, int y)
        {
            myBoard.MakeNextLegalMoves(myBoard.TheGrid[x, y]);
            int cnt = 0;
            for(int i = 0; i < myBoard.size; i++)
            {
                for(int j = 0; j < myBoard.size; j++)
                {
                    if (myBoard.TheGrid[i, j].LegalNextMove)
                    {
                        cnt++;
                    }
                }
            }
            if (cnt == 0) return true;
            else return false;

        }


    }
}
