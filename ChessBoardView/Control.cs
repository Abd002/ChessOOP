using ChessBoardModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessBoardView
{
    public class Control
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

        public void Display(Board myBoard, Button[,]btnGrid)
        {
            for (int i = 0; i < myBoard.size; i++)
            {
                for (int j = 0; j < myBoard.size; j++)
                {
                    btnGrid[i, j].Text = "";
                    if (myBoard.TheGrid[i, j].IsBlank == 1)
                        btnGrid[i, j].BackColor = Color.White;
                    else if (myBoard.TheGrid[i, j].IsBlank == 2) btnGrid[i, j].BackColor = Color.Blue;
                    else btnGrid[i, j].BackColor = new Color();

                    if (myBoard.TheGrid[i, j].LegalNextMove == true)
                    {
                        btnGrid[i, j].BackColor = Color.Green;
                        btnGrid[i, j].Text = myBoard.TheGrid[i, j].CellName;
                    }
                    else
                    {
                        btnGrid[i, j].Text = myBoard.TheGrid[i, j].CellName;
                    }
                }
            }
        }

        public Point KingCheck(Board myBoard, int PlayerTurn)
        {

            int x=0, y=0;
            for (int i = 0; i < myBoard.size; i++)
            {
                for (int j = 0; j < myBoard.size; j++)
                {
                    if (myBoard.TheGrid[i,j].CellName=="King"&& myBoard.TheGrid[i, j].IsBlank == PlayerTurn)
                    {
                        x = i;y = j;
                    }
                }
            }


            for (int i = 0; i < myBoard.size; i++)
            {
                for(int j = 0; j < myBoard.size; j++)
                {
                    if(myBoard.TheGrid[i,j].IsBlank!=PlayerTurn&& myBoard.TheGrid[i, j].CellName != "King")
                    {
                        myBoard.MakeNextLegalMoves(myBoard.TheGrid[i, j]);
                    }
                }
            }


            if (myBoard.TheGrid[x, y].LegalNextMove)
            {
                myBoard.Clear();

                if (IsItLose(myBoard, x, y))
                {
                    MessageBox.Show("You Lost");
                }
                else MessageBox.Show("You should move ur King");

                
                return new Point (x,y);
            }
            myBoard.Clear();
            return new Point(-1,0);
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
