using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ChessBoardModel;

namespace ChessBoardView
{

    public partial class Form1 : Form
    {
        static Board myBoard = new Board(8);

        public Button[,] btnGrid = new Button[myBoard.size, myBoard.size];
        public Form1()
        {
            InitializeComponent();
            populateGrid();

        }
        private void populateGrid()
        {
            int buttonSize = panel1.Width / myBoard.size;

            panel1.Width = panel1.Height;

            for(int i = 0; i < myBoard.size; i++)
            {
                for(int j=0;j<myBoard.size; j++)
                {
                    btnGrid[i, j] = new Button();
                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;

                    btnGrid[i, j].Click += Grid_Button_Click;

                    panel1.Controls.Add(btnGrid[i, j]);
                    btnGrid[i, j].Location=new Point(i*buttonSize, j*buttonSize);

                    btnGrid[i, j].Text = myBoard.TheGrid[i, j].CellName;

                    btnGrid[i, j].Tag = new Point(i, j);


                }
            }

        }

        int PlayerTurn = 1;
        private void Grid_Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;
            int flag = 0;
            //process of moving...
            if (myBoard.TheGrid[location.X, location.Y].LegalNextMove == true)
            {
                
                int x = (int)myBoard.TheGrid[location.X, location.Y].LegalNextMovePastPoint.X;
                int y = (int)myBoard.TheGrid[location.X, location.Y].LegalNextMovePastPoint.Y;
                myBoard.TheGrid[location.X, location.Y] = myBoard.TheGrid[x,y];
                myBoard.TheGrid[location.X, location.Y].RowNumber = location.X;
                myBoard.TheGrid[location.X, location.Y].ColumnNumber = location.Y;

                myBoard.TheGrid[x, y] = new Cell(x,y);
                flag = 1;
                PlayerTurn = (3 ^ myBoard.TheGrid[location.X, location.Y].IsBlank);
            }

            for (int i = 0; i < myBoard.size; i++)
            {
                for (int j = 0; j < myBoard.size; j++)
                {
                    myBoard.TheGrid[i, j].LegalNextMove = false;
                }
            }
            if (flag == 0 && myBoard.TheGrid[location.X,location.Y].IsBlank==PlayerTurn)
            {
                myBoard.MakeNextLegalMoves(myBoard.TheGrid[location.X, location.Y]);
            }


            for (int i = 0; i < myBoard.size; i++)
            {
                for(int j = 0; j < myBoard.size; j++)
                {
                    btnGrid[i,j].Text = "";
                    if (myBoard.TheGrid[i, j].IsBlank == 1)
                        btnGrid[i, j].BackColor = Color.White;
                    else if (myBoard.TheGrid[i, j].IsBlank == 2) btnGrid[i, j].BackColor = Color.Blue;
                    else btnGrid[i, j].BackColor = new Color();

                    if (myBoard.TheGrid[i, j].LegalNextMove == true)
                    {
                        //btnGrid[i, j].Text = "Attack";
                        btnGrid[i,j].BackColor=Color.Green;
                        btnGrid[i, j].Text = myBoard.TheGrid[i, j].CellName;
                    }else
                    {
                        btnGrid[i, j].Text = myBoard.TheGrid[i, j].CellName;
                    }
                }
            }

        }
    }
}
