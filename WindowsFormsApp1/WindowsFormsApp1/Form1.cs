using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution = 1;
        private int density = 10;
        private bool [,] is_alive;
        private int rows = 1000;
        private int cols = 1000;
        
        // to start the game u should click on the screen
        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            rows = pictureBox1.Height / resolution;
            cols = pictureBox1.Width / resolution;
            is_alive = new bool[cols, rows];


            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            Random random = new Random();
            for(int x = 0; x < cols; x++)
            {
                for(int y=0; y < rows; y++)
                {
                     if(random.Next(density) == 0)
                    {
                        is_alive[x, y] = true;
                        graphics.FillRectangle(Brushes.Black, x, y, resolution, resolution);
                    }
                }
            }

            


            
            timer1.Start();
        }

        private void StopGame()
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
        }

        private bool CheckNeighbours(int x, int y, int rows, int cols)
        {
            if (x - 1 >= 0 && !is_alive[x - 1, y])
                return true;
            if (x + 1 < rows && !is_alive[x + 1, y])
                return true;
            if (y + 1 < cols && !is_alive[x, y + 1])
                return true;
            if (y - 1 >= 0 && !is_alive[x, y - 1])
                return true;

            return false;
        }
        private bool CheckBorders(int x, int y, int rows, int cols)
        {
            if (x >= 0 && y >= 0 && x < rows && y < cols)
                return true;
            return false;
        }


        private void NextGenerationMove(int rows, int cols)
        {
            for (int x = 0; x < rows; x++)
                for (int y = 0; y < cols; y++)
                {
                    if (is_alive[x, y])
                        NextGenerationUnit(x, y, rows, cols);
                }
        }
        private void NextGenerationUnit(int x, int y, int rows, int cols)
        {


            if (CheckNeighbours(x, y, rows, cols))
            {
                Random random = new Random();
                int next_x = x;
                int next_y = y;
                int direction;
                bool clear = false;

                direction = random.Next(4);
                switch (direction)
                {
                    case 0:
                        next_y -= 1;
                        break;
                    case 1:
                        next_x += 1;
                        break;
                    case 2:
                        next_y += 1;
                        break;
                    case 3:
                        next_x -= 1;
                        break;
                }
                if (CheckBorders(next_x, next_y, rows, cols))
                    clear = true;
                if (clear)
                { 
                    is_alive[x, y] = false;
                    graphics.FillRectangle(Brushes.AliceBlue, x, y, resolution, resolution);
                    is_alive[next_x, next_y] = true;
                    graphics.FillRectangle(Brushes.Black, next_x, next_y, resolution, resolution);
                    pictureBox1.Refresh();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGenerationMove(rows, cols);


        }
        
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                StopGame();
            else
                StartGame();
        }
    }
}
