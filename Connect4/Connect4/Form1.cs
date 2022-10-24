namespace Connect4
{
    public partial class Form1 : Form
    {
        private int nbBoxesEmpty;
        private int nbBoxes;

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
            for (int i = 0; i < 7; i++)
            {
                //create 7 PictureBoxe[]
                PictureBox[] col = new PictureBox[6];
                for (int j = 0; j < 6; j++)
                {
                    col[j] = new PictureBox();
                    col[j].Name = "space" + i + "-" + j;
                    col[j].Size = new Size(100, 100);
                    col[j].Location = new Point((i * 100),(j * 100));
                    //col[j].Location = new Point(50 + (i * 50), 50 + (j * 50));
                    col[j].Image = Image.FromFile("none.png");
                    col[j].SizeMode = PictureBoxSizeMode.Zoom;
                    col[j].Click += new EventHandler(col_Click);
                    this.Controls.Add(col[j]);
                }
            }
        }

        private void col_Click(object sender, EventArgs e)
        {
            PictureBox col = (PictureBox)sender;
            string name = col.Name;
            int colNumber = Convert.ToInt32(name.Substring(5, 1));
            int rowNumber = Convert.ToInt32(name.Substring(7, 1));
            PictureBox[] verif = new PictureBox[6];
            for (int i = 0; i < 6; i++)
            {
                verif[i] = (PictureBox)this.Controls.Find("space" + colNumber + "-" + i, true)[0];
            }
            
            for (int i = 0; i < 6; i++)
            {
                if (verif[5].Image == Image.FromFile("none.png"))
                {
                    verif[5].Image = Image.FromFile("red.png");
                    return;
                }
                 else if (verif[i+1].Image != Image.FromFile("none.png"))
                 {
                     verif[i].Image = Image.FromFile("red.png");
                     break;
                 }
            }
           
        }
    }
}