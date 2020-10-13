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
        public Form1()
        {
            InitializeComponent();
            runner = new Runner(this);
        }

        Runner runner;
        private Graphics graphics;

        public int CountRows(int resolution)
        {
            return pictureBox1.Height / resolution;
        }

        public int CountCols(int resolution)
        {
            return pictureBox1.Width / resolution;
        }

        public void InitGraphics()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        public void StartTimer()
        {
            timer1.Start();
        }


        public void DrawCanvas(List<Plant> plants, List<Organism> organisms, int resolution)
        {
            graphics.Clear(Color.Cornsilk);

            foreach (var plant in plants)
            {
                if (plant.is_alive)
                    graphics.FillRectangle(Brushes.YellowGreen, plant.x * resolution, plant.y * resolution, resolution, resolution);

            }

            foreach (var organism in organisms)
            {
                if (organism.is_alive)
                    graphics.FillRectangle(Brushes.MediumVioletRed, organism.x * resolution, organism.y * resolution, resolution, resolution);

            }
            pictureBox1.Refresh();


        }

        private void startGame()
        {
            if (timer1.Enabled)
                return;
            runner.FirstTick();
            timer1.Start();
        }

        private void stopGame()
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            runner.NextTick();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                stopGame();
            else
                startGame();
        }
    }
}
