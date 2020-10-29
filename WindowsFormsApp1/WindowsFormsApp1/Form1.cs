using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Life simulation";
            runner = new Runner(this);
            continueButton.Enabled = false;
            stopButton.Enabled = false;
        }

        Runner runner;
        Organism observedOrganism;
        private Graphics graphics;
        int resolution;


        public void StartTimer()
        {
            timer1.Start();
        }
        private void getInfoAboutObservedOrganism(int x, int y)
        {
            // no need for old one
            if (observedOrganism != null)
            {
                if (observedOrganism.is_alive)
                {
                    if (observedOrganism.male)
                        graphics.FillEllipse(Brushes.SlateBlue, observedOrganism.x * (int)resolutionUpDown.Value, observedOrganism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    else
                        graphics.FillEllipse(Brushes.MediumVioletRed, observedOrganism.x * (int)resolutionUpDown.Value, observedOrganism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                }
                else
                    graphics.FillEllipse(Brushes.Gray, observedOrganism.x * (int)resolutionUpDown.Value, observedOrganism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
            }

            observedOrganism = runner.GetOrganism(x, y);
            if (observedOrganism != null)
            {
                sexLabel.Text = $"{((observedOrganism.male == true) ? "Male" : "Female") }";
                deadOrAliveLabel.Text = "Dead";
                hungerLabel.Text = $"{observedOrganism.fullness.ToString()}";
                positionXLabel.Text = $"{observedOrganism.x.ToString()}";
                positionYLabel.Text = $"{observedOrganism.y.ToString()}";
                orgIDLabel.Text = $"{observedOrganism.orgID.ToString()}";
                if (observedOrganism.is_alive)
                {
                    labelReproduce.Text = $"{observedOrganism.wantReproduce.ToString()}";
                    deadOrAliveLabel.Text = "Alive";
                    labelInfoBecomeGrass.Text = "";
                    labelBecomeGrass.Text = "";
                    organismVisionLabel.Text = $"{((runner.IsItDayToday()) ? observedOrganism.organismRange.ToString() : (observedOrganism.organismRange / 2).ToString()) }";
                }
                else
                {
                    organismVisionLabel.Text = "0";
                    labelReproduce.Text = "Bruh";
                    deadOrAliveLabel.Text = "Dead";
                    labelInfoBecomeGrass.Text = "Become grass in: ";
                    labelBecomeGrass.Text = $"{(observedOrganism.deadUntil - observedOrganism.deadFor).ToString()}";
                }

                graphics.FillEllipse(Brushes.DarkOrange, observedOrganism.x * (int)resolutionUpDown.Value, observedOrganism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                pictureBox1.Refresh();

            }
        }
        private void refreshInfoAboutObservedOrganism()
        {
            if (observedOrganism != null)
            {
                sexLabel.Text = $"{((observedOrganism.male == true) ? "Male" : "Female") }";
                deadOrAliveLabel.Text = "Dead";
                hungerLabel.Text = $"{observedOrganism.fullness.ToString()}";
                positionXLabel.Text = $"{observedOrganism.x.ToString()}";
                positionYLabel.Text = $"{observedOrganism.y.ToString()}";
                orgIDLabel.Text = $"{observedOrganism.orgID.ToString()}";
                if (observedOrganism.is_alive)
                {
                    labelReproduce.Text = $"{observedOrganism.wantReproduce.ToString()}";
                    deadOrAliveLabel.Text = "Alive";
                    labelInfoBecomeGrass.Text = "";
                    labelBecomeGrass.Text = "";
                    organismVisionLabel.Text = $"{((runner.IsItDayToday()) ? observedOrganism.organismRange.ToString() : (observedOrganism.organismRange / 2).ToString()) }";
                }
                else
                {
                    organismVisionLabel.Text = "0";
                    labelReproduce.Text = "Bruh";
                    deadOrAliveLabel.Text = "Dead";
                    labelInfoBecomeGrass.Text = "Become grass in: ";
                    labelBecomeGrass.Text = $"{(observedOrganism.deadUntil - observedOrganism.deadFor).ToString()}";
                }

                graphics.FillEllipse(Brushes.DarkOrange, observedOrganism.x * (int)resolutionUpDown.Value, observedOrganism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
            }
        }
        private void refreshInfoAboutDay()
        {
            if (runner.IsItDayToday())
                DayOrNightLabel.Text = $"Day (Night in: {runner.HowMuchUntilChange().ToString()})";
            else
                DayOrNightLabel.Text = $"Night (Day in: {runner.HowMuchUntilChange().ToString()})";
        }
        private void startGame()
        {
            if (timer1.Enabled)
                return;
            startButton.Enabled = false;
            continueButton.Enabled = false;
            stopButton.Enabled = true;
            continueButton.BackColor = Color.LightGray;
            runner.FirstTick();
            timer1.Start();
        }
        private void stopGame()
        {
            if (!timer1.Enabled)
                return;
            continueButton.Enabled = true;
            startButton.Enabled = true;
            startButton.Text = "RESTART";
            stopButton.Enabled = false;
            continueButton.BackColor = Color.DarkCyan;
            timer1.Stop();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            runner.NextTick();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            startGame();
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            stopGame();
        }
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;
            timer1.Start();
            continueButton.Enabled = false;
            stopButton.Enabled = true;
            startButton.Enabled = false;
            continueButton.BackColor = Color.LightGray;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (runner != null)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                getInfoAboutObservedOrganism(x, y);
            }
        }
        public void InitGraphics()
        {
            resolution = (int)resolutionUpDown.Value;
            pictureBox1.Image = new Bitmap(runner.Cols() * (int)resolutionUpDown.Value, runner.Rows() * (int)resolutionUpDown.Value);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }
        public void DrawCanvas(List<Plant> plants, List<Organism> organisms, bool day)
        {

            if (resolution != (int)resolutionUpDown.Value)
                // resolution changed
                InitGraphics();

            if (day)
                graphics.Clear(Color.Cornsilk);
            else
                graphics.Clear(Color.DimGray);

            // show plants
            foreach (var plant in plants)
                graphics.FillRectangle(Brushes.YellowGreen, plant.x * (int)resolutionUpDown.Value, plant.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);

            // show orgs
            foreach (var organism in organisms)
            {
                if (organism.is_alive)
                {
                    if (organism.male)
                        graphics.FillEllipse(Brushes.SlateBlue, organism.x * (int)resolutionUpDown.Value, organism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    else
                        graphics.FillEllipse(Brushes.MediumVioletRed, organism.x * (int)resolutionUpDown.Value, organism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                }
                else
                    graphics.FillEllipse(Brushes.Gray, organism.x * (int)resolutionUpDown.Value, organism.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);

            }

            refreshInfoAboutObservedOrganism();
            refreshInfoAboutDay();
            pictureBox1.Refresh();
        }


    }
}
