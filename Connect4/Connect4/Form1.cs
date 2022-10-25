namespace Connect4
{
    public partial class Form1 : Form
    {
        private int nbBoxesEmpty;
        private int nbBoxes;
        private int isRed = 0;

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
            if (isRed == 0)
            {
                //change to red
                change.Image = Image.FromFile("red.png");
                change.ImageLocation = "red.png";
                isRed = 1;
            }
            else {
                //change to yellow
                change.Image = Image.FromFile("yellow.png");
                change.ImageLocation = "yellow.png";
                isRed = 0;
            }
        }

        private PictureBox[] getCol(int col)
        {
            PictureBox[] colArray = new PictureBox[6];
            for (int i = 0; i < 6; i++)
            {
                colArray[i] = (PictureBox)this.Controls.Find("space" + col + "-" + i, true)[0];
            }
            return colArray;
        }

        private void ConnectCheck(int numCol, int numRow)
        {
           
            
        }

        private void win()
        {
            for (int i = 0; i < 7; i++)
            {
                PictureBox[] col = getCol(i);
                for (int j = 0; j < 6; j++)
                {
                    col[j].Image = Image.FromFile("error.png");
                }
            }
        }
            private void col_Click(object sender, EventArgs e)
        {
            PictureBox col = (PictureBox)sender;
            string name = col.Name;
            int colNumber = Convert.ToInt32(name.Substring(5, 1));
            int rowNumber = 0;
            PictureBox[] verif = getCol(colNumber);

            if (verif[0].ImageLocation != "none.png")
            {
                return;
            }
            else if (verif[5].ImageLocation == "none.png")
            {
                displaySpaceColor(colNumber, 5);
                rowNumber = 5;
                ConnectCheck(colNumber, rowNumber);
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                 if (verif[i+1].ImageLocation != "none.png")
                 {
                    displaySpaceColor(colNumber, i);
                    rowNumber = i;
                    break;
                 }
            }
            ConnectCheck(colNumber, rowNumber);
        }
    }
}