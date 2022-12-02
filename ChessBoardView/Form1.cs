using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ChessBoardModel;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ChessBoardView
{

    public partial class Form1 : Form
    {
        public Board myBoard = new Board(8);
        private Control controll = new Control();
        Dictionary<string, Bitmap> imageValue = new Dictionary<string, Bitmap>();
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private string sound;

        public Panel[,] pnlGrid = new Panel[8, 8];
        public Form1()
        {
            InitializeComponent();
            InitializeImageValue();
            populateGrid();
            
        }

        
        private void populateGrid()
        {
            int panelSize = panel1.Width / myBoard.size;
            for (int i = 0; i < myBoard.size; i++)
            {
                for(int j = 0; j < myBoard.size; j++)
                {
                    pnlGrid[i, j] = new Panel();
                    pnlGrid[i, j].Height = panelSize;
                    pnlGrid[i, j].Width = panelSize;
                    pnlGrid[i, j].Location=new Point(i * panelSize, j * panelSize);
                    pnlGrid[i, j].Click += Grid_Button_Click;
                    pnlGrid[i, j].Tag = new Point(i, j);
                    if (myBoard.TheGrid[i, j].imageKey != "NULL")
                    {
                        pnlGrid[i, j].BackgroundImage = imageValue[myBoard.TheGrid[i, j].imageKey];
                        pnlGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if ((i + j) % 2 == 0) pnlGrid[i, j].BackColor=Color.White;
                    else pnlGrid[i, j].BackColor=Color.Gray;

                    panel1.Controls.Add(pnlGrid[i, j]);

                }
            }

        }

        int PlayerTurn = 1;
        private void Grid_Button_Click(object sender, EventArgs e)
        {
            Point location = (Point)((Panel)sender).Tag;
            
            if (myBoard.TheGrid[location.X, location.Y].LegalNextMove == true)
            {
                int x = (int)myBoard.TheGrid[location.X, location.Y].LegalNextMovePastPoint.X;
                int y = (int)myBoard.TheGrid[location.X, location.Y].LegalNextMovePastPoint.Y;

                if (myBoard.TheGrid[location.X, location.Y].IsBlank == 0)
                {
                    sound = "Move.wav";
                } else sound = "Capture.wav";

                Cell temp1 = myBoard.TheGrid[location.X, location.Y];
                Cell temp2 = myBoard.TheGrid[x, y];

                myBoard.TheGrid[location.X, location.Y] = myBoard.TheGrid[x, y];
                myBoard.TheGrid[location.X, location.Y].RowNumber = location.X;
                myBoard.TheGrid[location.X, location.Y].ColumnNumber = location.Y;

                myBoard.TheGrid[x, y] = new Cell(x, y);

                if (controll.KingCheck(myBoard, PlayerTurn).X != -1)
                {
                    //

                    myBoard.TheGrid[location.X, location.Y] = temp1;
                    myBoard.TheGrid[x, y] = temp2;
                    myBoard.TheGrid[x, y].RowNumber = x;
                    myBoard.TheGrid[x, y].ColumnNumber = y;

                }
                else
                {
                    
                    if (PlayerTurn == 1 && myBoard.TheGrid[location.X,location.Y].CellName=="Pawn"&&location.Y==0)
                    {
                        if (comboBox1.Text.Length < 2)
                        {
                            myBoard.TheGrid[location.X, location.Y] = temp1;
                            myBoard.TheGrid[x, y] = temp2;
                            myBoard.TheGrid[x, y].RowNumber = x;
                            myBoard.TheGrid[x, y].ColumnNumber = y;
                            MessageBox.Show("Choose piece");
                            return;
                        }
                        else
                        {
                            myBoard.TheGrid[location.X, location.Y].CellName = comboBox1.Text;
                            myBoard.TheGrid[location.X, location.Y].imageKey = "White" + comboBox1.Text;
                        }
                    }else if(PlayerTurn == 2 && myBoard.TheGrid[location.X, location.Y].CellName == "Pawn" && location.Y == 7)
                    {
                        if (comboBox1.Text.Length < 2)
                        {
                            myBoard.TheGrid[location.X, location.Y] = temp1;
                            myBoard.TheGrid[x, y] = temp2;
                            myBoard.TheGrid[x, y].RowNumber = x;
                            myBoard.TheGrid[x, y].ColumnNumber = y;
                            MessageBox.Show("Choose piece");
                            return;
                        }
                        else
                        {
                            myBoard.TheGrid[location.X, location.Y].CellName = comboBox1.Text;
                            myBoard.TheGrid[location.X, location.Y].imageKey = "Black" + comboBox1.Text;
                        }
                    }
                    player.URL = sound;
                    player.controls.play();
                    PlayerTurn = (3 ^ myBoard.TheGrid[location.X, location.Y].IsBlank);
                    myBoard.Clear(myBoard);

                }
            }

            Point enemy = controll.KingCheck(myBoard, PlayerTurn);
            if (myBoard.TheGrid[location.X, location.Y].IsBlank != 0)
            {
                if (myBoard.TheGrid[location.X, location.Y].IsBlank == PlayerTurn)
                {
                    myBoard.Clear(myBoard);
                    if (enemy.X != -1)
                    {
                        myBoard.MakeNextLegalMoves(myBoard.TheGrid[location.X, location.Y]);
                        /*
                        if (myBoard.TheGrid[enemy.X, enemy.Y].LegalNextMove == false)
                        {
                            int x = 0, y = 0;
                            for (int i = 0; i < myBoard.size; i++)
                            {
                                for (int j = 0; j < myBoard.size; j++)
                                {
                                    if (myBoard.TheGrid[i, j].CellName == "King" && myBoard.TheGrid[i, j].IsBlank == PlayerTurn)
                                    {
                                        x = i; y = j;
                                    }
                                }
                            }

                            if (location.X != x || location.Y != y)
                            {
                                myBoard.Clear(myBoard);
                            }
                        }
                        */
                        //else king legal move;
                    }
                    else
                    {
                        myBoard.MakeNextLegalMoves(myBoard.TheGrid[location.X, location.Y]);
                    }
                }
            }
            
            controll.Display(myBoard, pnlGrid, imageValue);
        }
        private void InitializeImageValue()
        {
            imageValue["BlackBishop"] = Properties.Resources.BlackBishop;
            imageValue["WhiteBishop"] = Properties.Resources.WhiteBishop;

            imageValue["BlackKing"] = Properties.Resources.BlackKing;
            imageValue["WhiteKing"] = Properties.Resources.WhiteKing;

            imageValue["BlackKnight"] = Properties.Resources.BlackKnight;
            imageValue["WhiteKnight"] = Properties.Resources.WhiteKnight;

            imageValue["BlackPawn"] = Properties.Resources.BlackPawn;
            imageValue["WhitePawn"] = Properties.Resources.WhitePawn;

            imageValue["BlackQueen"] = Properties.Resources.BlackQueen;
            imageValue["WhiteQueen"] = Properties.Resources.WhiteQueen;

            imageValue["BlackRook"] = Properties.Resources.BlackRook;
            imageValue["WhiteRook"] = Properties.Resources.WhiteRook;

            imageValue["BlackKingChecked"] = Properties.Resources.WhiteKingChecked;
            imageValue["WhiteKingChecked"] = Properties.Resources.WhiteKingChecked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
