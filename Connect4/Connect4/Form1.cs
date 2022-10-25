using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Connect4
{
    public partial class Form1 : Form
    {
        private int nbBoxesEmpty;
        private int nbBoxes;
        private string player = "red";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displaySpacesStart();
        }

        private void displaySpacesStart()
        {
            //create a grid with 7 columns et 6 rows
            PictureBox[,] col = new PictureBox[7,6];

            //for each column
            for (int i = 0; i < 7; i++)
            {
                //for each row
                for (int j = 0; j < 6; j++)
                {
                    //create a new space
                    col[i,j] = new PictureBox();
                    col[i, j].Name = "space" + i + "-" + j;
                    col[i, j].Size = new Size(100, 100);
                    col[i, j].Location = new Point((i * 100),(j * 100));
                    col[i, j].Image = Image.FromFile("none.png");
                    col[i, j].ImageLocation = "none.png";
                    col[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    col[i, j].Click += new EventHandler(col_Click);
                    this.Controls.Add(col[i, j]);
                }
            }
        }

        private void displaySpaceColor(int col, int row)
        {
            PictureBox change = (PictureBox)this.Controls.Find("space" + col + "-" + row, true)[0];

            //if last change was red, change to yellow
            if (player == "red")
            {
                //change to red
                change.Image = Image.FromFile("red.png");
                change.ImageLocation = "red.png";
                player = "yellow";
            }
            else {
                //change to yellow
                change.Image = Image.FromFile("yellow.png");
                change.ImageLocation = "yellow.png";
                player = "red";
            }
        }

        private PictureBox[,] getGrid()
        {
            PictureBox[,] Array = new PictureBox[7,6];
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Array[i,j] = (PictureBox)this.Controls.Find("space" + i + "-" + j, true)[0];
                }
            }
            return Array;
        }

        private void IsWinning(int numCol, int numRow)
        {
           int count=1;
            //horizontal
            if (numCol-1<= 0)count += checkConnect(player,numCol-1,numRow, -1, 0);
            if (numCol + 1 <= 6) count += checkConnect(player, numCol + 1, numRow, 1, 0);
            if (count >= 4) win();
            else count = 1;

            //diagonal up-left/down-right
            if (numRow - 1 >= 0 && numRow-1 >=0) count += checkConnect(player, numCol-1, numRow - 1, -1, -1);
            if (numRow + 1 <= 6 && numCol + 1 <= 5) count += checkConnect(player, numCol + 1, numRow + 1, 1, 1);
            if (count >= 4) win();
            else count = 1;

            //vertical
            if (numRow - 1 >= 0) count += checkConnect(player, numCol, numRow - 1, 0, -1);
            if (numRow + 1 <= 5) count += checkConnect(player, numCol, numRow + 1, 0, 1);
            if (count >= 4) win();
            else count = 1;

            //diagonal up-right/down-left
            if (numRow - 1 >= 0 && numCol + 1 < 6) count += checkConnect(player, numCol - 1, numRow + 1, -1, 1);
            if (numRow + 1 < 6 && numCol - 1 <= 0) count += checkConnect(player, numCol + 1, numRow - 1, 1, -1);
            if (count >= 4) win();
            else count = 1;
        }

        private int checkConnect(string player, int numCol, int numRow, int dirCol, int dirRow)
        {
            PictureBox[,] grid = getGrid();
            return grid[numCol, numRow].ImageLocation == (player + ".png") ? (((numCol+dirCol>=0 && numCol+dirCol<7)&&(numRow+dirRow>=0 && numRow+dirRow<7)))?
                1+checkConnect(player, numCol, numRow, dirCol, dirRow):1:0;
        }


        private void win()
        {
            PictureBox[,] grid = getGrid();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    grid[i,j].Image = Image.FromFile("error.png");
                }
            }
        }
            private void col_Click(object sender, EventArgs e)
        {
            PictureBox col = (PictureBox)sender;
            string name = col.Name;
            int colNumber = Convert.ToInt32(name.Substring(5, 1));
            int rowNumber = 0;
            
            PictureBox[,] verif = getGrid();

            if (verif[colNumber,0].ImageLocation != "none.png")
            {
                return;
            }
            else if (verif[colNumber,5].ImageLocation == "none.png")
            {
                displaySpaceColor(colNumber, 5);
                rowNumber = 5;
                IsWinning(colNumber, rowNumber);
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                 if (verif[colNumber, i +1].ImageLocation != "none.png")
                 {
                    displaySpaceColor(colNumber, i);
                    rowNumber = i;
                    break;
                 }
            }
            IsWinning(colNumber, rowNumber);
        }
    }
}