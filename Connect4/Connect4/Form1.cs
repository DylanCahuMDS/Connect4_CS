using System.Data.Common;
using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    public partial class Form1 : Form
    {
        private string player = "red";
        private int largeur = 6;
        private int hauteur = 5;
        private PictureBox[,] grid;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            displaySpacesStart();
            grid = getGrid();
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
             for (int row = 0; row < hauteur; row++)
             {
                 for (int column = 0; column < largeur; column++)
                 {
                     if (CheckVertically(row, column)) { win(); }
                     if (CheckHorizontally(row, column)) { win(); }
                     if (CheckDiagonallyDown(row, column)) { win(); }
                     if (CheckDiagonallyUp(row, column)) { win(); }
                 }
             }
        }
        /// <summary>
        /// Looks to see if the given row and column is the starting point for
        /// four in a row, horizontally.
        /// </summary>
        private bool CheckHorizontally(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (column + 3 >= largeur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[row, column + distance].ImageLocation != player + ".png") { return false; }
            }

            return true;
        }

        /// <summary>
        /// Looks to see if the given row and column is the starting point for
        /// four in a row, vertically.
        /// </summary>
        private bool CheckVertically(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (row + 3 >= hauteur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[row + distance, column].ImageLocation != player +".png") { return false; }
            }

            return true;
        }

        /// <summary>
        /// Looks to see if the given row and column is the starting point for
        /// four in a row, diagonally down.
        /// </summary>
        private bool CheckDiagonallyDown(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (row + 3 >= hauteur) { return false; }
            if (column + 3 >= largeur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[row + distance, column + distance].ImageLocation != player + ".png") { return false; }
            }

            return true;
        }

        /// <summary>
        /// Looks to see if the given row and column is the starting point for
        /// four in a row, diagonally up.
        /// </summary>
        private bool CheckDiagonallyUp(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (row - 3 < 0) { return false; }
            if (column + 3 >= largeur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[row - distance, column + distance].ImageLocation != player +".png") { return false; }
            }

            return true;
        }
        
        private void win()
        {
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
            
            if (grid[colNumber,0].ImageLocation != "none.png")
            {
                return;
            }
            else if (grid[colNumber,5].ImageLocation == "none.png")
            {
                displaySpaceColor(colNumber, 5);
                rowNumber = 5;
                IsWinning(colNumber, rowNumber);
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                 if (grid[colNumber, i +1].ImageLocation != "none.png")
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