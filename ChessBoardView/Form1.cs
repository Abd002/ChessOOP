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
        private Control controll=new Control();

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
                    btnGrid[i, j].Location=new Point(i*buttonSize, j*buttonSize);
                    btnGrid[i, j].Text = myBoard.TheGrid[i, j].CellName;
                    btnGrid[i, j].Tag = new Point(i, j);
                    btnGrid[i, j].Click += Grid_Button_Click;
                    panel1.Controls.Add(btnGrid[i, j]);
                }
            }

        }

        int PlayerTurn = 1;
        private void Grid_Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;
            int IsItMove = 0;



            if (myBoard.TheGrid[location.X, location.Y].LegalNextMove == true)
            {
                PlayerTurn = controll.MovingInChess(myBoard,location,PlayerTurn);
                IsItMove = 1;
            }

            myBoard.Clear();
            
            if (IsItMove == 0 && myBoard.TheGrid[location.X,location.Y].IsBlank==PlayerTurn)
            {
                if (controll.KingCheck(myBoard, PlayerTurn).X == -1)
                {
                    myBoard.MakeNextLegalMoves(myBoard.TheGrid[location.X, location.Y]);
                }
                else if(controll.KingCheck(myBoard, PlayerTurn)==location)
                {
                    
                    myBoard.MakeNextLegalMoves(myBoard.TheGrid[location.X, location.Y]);
                }
                
            }

            controll.Display(myBoard,btnGrid);
        }
    }
}
