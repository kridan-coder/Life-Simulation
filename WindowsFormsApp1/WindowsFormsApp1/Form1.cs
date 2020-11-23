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
        Herbivore observedHerbivore = null;
        Omnivore observedOmnivore = null;
        Predatory observedPredatory = null;
        
        private Graphics graphics;
        int resolution;


        public void StartTimer()
        {
            timer1.Start();
        }

        private void paintOrg(int x, int y, bool isAlive, bool male, Brush ifMale, Brush ifFemale, Brush ifDead)
        {
            if (isAlive)
            {
                if (male)
                    graphics.FillEllipse(ifMale, x * (int)resolutionUpDown.Value, y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                else
                    graphics.FillEllipse(ifFemale, x * (int)resolutionUpDown.Value, y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
            }
            else
                graphics.FillEllipse(ifDead, x * (int)resolutionUpDown.Value, y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
        }
        private void paintObservedOrg(int x, int y)
        {
            graphics.FillEllipse(Brushes.DarkOrange, x * (int)resolutionUpDown.Value, y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
            pictureBox1.Refresh();
        }

        private void clearOldObservedOrganisms()
        {
            if (observedHerbivore != null)
                paintOrg(observedHerbivore.x, observedHerbivore.y, observedHerbivore.isAlive, observedHerbivore.male, Brushes.DarkBlue, Brushes.SlateBlue, Brushes.LightGray);
            if (observedOmnivore != null)
                paintOrg(observedOmnivore.x, observedOmnivore.y, observedOmnivore.isAlive, observedOmnivore.male, Brushes.DarkViolet, Brushes.Violet, Brushes.Gray);
            if (observedPredatory != null)
                paintOrg(observedPredatory.x, observedPredatory.y, observedPredatory.isAlive, observedPredatory.male, Brushes.DarkRed, Brushes.Red, Brushes.DarkGray);

        }

        private void refreshOrgShowingInfo(bool alive, bool male, bool wantReproduce, bool wantFood, int fullness, int x, int y, int ID, int visionRange, int deadUntil, int deadFor, string animalType)
        {
            sexLabel.Text = $"{((male) ? "Male" : "Female") }";
            hungerLabel.Text = $"{fullness.ToString()}";
            labelAnimal.Text = animalType;
            positionXLabel.Text = $"{x.ToString()}";
            positionYLabel.Text = $"{y.ToString()}";
            orgIDLabel.Text = $"{ID.ToString()}";
            if (alive)
            {
                labelReproduce.Text = $"{wantReproduce.ToString()}";
                labelWantEat.Text = $"{wantFood.ToString()}";
                deadOrAliveLabel.Text = "Alive";
                labelInfoBecomeGrass.Text = "";
                labelBecomeGrass.Text = "";
                organismVisionLabel.Text = $"{((runner.IsItDayToday()) ? visionRange.ToString() : (visionRange / 2).ToString()) }";
            }
            else
            {
                organismVisionLabel.Text = "0";
                labelReproduce.Text = "Bruh";
                labelWantEat.Text = "Bruh";
                deadOrAliveLabel.Text = "Dead";
                labelInfoBecomeGrass.Text = "Become grass in: ";
                labelBecomeGrass.Text = $"{(deadUntil - deadFor).ToString()}";
            }
            paintObservedOrg(x,y);

        }


        private void getInfoAboutObservedHerbivore(int x, int y)
        {
            // no need for old ones
            clearOldObservedOrganisms();
            observedHerbivore = runner.TryToGetHerbivore(x, y);
            if (observedHerbivore != null)
            {
                observedOmnivore = null;
                observedPredatory = null;
                refreshOrgShowingInfo(observedHerbivore.isAlive, observedHerbivore.male, observedHerbivore.wantReproduce, observedHerbivore.wantFood, observedHerbivore.fullness, observedHerbivore.x, observedHerbivore.y, observedHerbivore.orgID, observedHerbivore.organismRange, observedHerbivore.deadUntil, observedHerbivore.deadFor, "Herbivore");
                paintObservedOrg(x, y);
            }
        }
        private void getInfoAboutObservedOmnivore(int x, int y)
        {
            // no need for old ones
            clearOldObservedOrganisms();
            observedOmnivore = runner.TryToGetOmnivore(x, y);
            if (observedOmnivore != null)
            {
                observedHerbivore = null;
                observedPredatory = null;
                refreshOrgShowingInfo(observedOmnivore.isAlive, observedOmnivore.male, observedOmnivore.wantReproduce, observedOmnivore.wantFood, observedOmnivore.fullness, observedOmnivore.x, observedOmnivore.y, observedOmnivore.orgID, observedOmnivore.organismRange, observedOmnivore.deadUntil, observedOmnivore.deadFor, "Omnivore");
                paintObservedOrg(x, y);
            }
        }
        private void getInfoAboutObservedPredatory(int x, int y)
        {
            // no need for old ones
            clearOldObservedOrganisms();
            observedPredatory = runner.TryToGetPredatory(x, y);
            if (observedPredatory != null)
            {
                observedHerbivore = null;
                observedOmnivore = null;
                refreshOrgShowingInfo(observedPredatory.isAlive, observedPredatory.male, observedPredatory.wantReproduce, observedPredatory.wantFood, observedPredatory.fullness, observedPredatory.x, observedPredatory.y, observedPredatory.orgID, observedPredatory.organismRange, observedPredatory.deadUntil, observedPredatory.deadFor, "Predatory");
                paintObservedOrg(x, y);
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
                getInfoAboutObservedHerbivore(x, y);
                getInfoAboutObservedOmnivore(x, y);
                getInfoAboutObservedPredatory(x, y);

            }
        }
        public void InitGraphics()
        {
            resolution = (int)resolutionUpDown.Value;
            pictureBox1.Image = new Bitmap(runner.Cols() * (int)resolutionUpDown.Value, runner.Rows() * (int)resolutionUpDown.Value);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }
        public void DrawCanvas(List<Plant> plants, List<Herbivore> herbivores, List<Predatory> predators, List<Omnivore> omnivores, List<MeteoriteShard> shards, bool day)
        {

            if (resolution != (int)resolutionUpDown.Value)
                // resolution changed
                InitGraphics();

            if (day)
                graphics.Clear(Color.Cornsilk);
            else
                graphics.Clear(Color.DarkSlateBlue);

            // show shards
            if (shards != null)
            {
                foreach (var shard in shards)
                {
                    if (!shard.meteorite.hasFallen)
                    {
                        graphics.FillRectangle(Brushes.Black, shard.x * (int)resolutionUpDown.Value, shard.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    }
                    else if (shard.meteorite.hasFallen && !shard.meteorite.becameCold)
                    {
                        graphics.FillRectangle(Brushes.Orange, shard.x * (int)resolutionUpDown.Value, shard.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    }
                    else if (shard.meteorite.becameCold)
                    {
                        graphics.FillRectangle(Brushes.DarkCyan, shard.x * (int)resolutionUpDown.Value, shard.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    }
                }
            }

            // show plants
            foreach (var plant in plants)
                graphics.FillRectangle(Brushes.YellowGreen, plant.x * (int)resolutionUpDown.Value, plant.y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);



            // show herbivores
            foreach (var herbivore in herbivores)
                paintOrg(herbivore.x, herbivore.y, herbivore.isAlive, herbivore.male, Brushes.DarkBlue, Brushes.SlateBlue, Brushes.LightGray);


            // show predators
            foreach (var predator in predators)
                paintOrg(predator.x, predator.y, predator.isAlive, predator.male, Brushes.DarkRed, Brushes.Red, Brushes.DarkGray);


            // show omnivores
            foreach (var omnivore in omnivores)
                paintOrg(omnivore.x, omnivore.y, omnivore.isAlive, omnivore.male, Brushes.DarkViolet, Brushes.Violet, Brushes.Gray);


            if (observedHerbivore != null)
                refreshOrgShowingInfo(observedHerbivore.isAlive, observedHerbivore.male, observedHerbivore.wantReproduce, observedHerbivore.wantFood, observedHerbivore.fullness, observedHerbivore.x, observedHerbivore.y, observedHerbivore.orgID, observedHerbivore.organismRange, observedHerbivore.deadUntil, observedHerbivore.deadFor, "Herbivore");
            else if (observedOmnivore != null)
                refreshOrgShowingInfo(observedOmnivore.isAlive, observedOmnivore.male, observedOmnivore.wantReproduce, observedOmnivore.wantFood, observedOmnivore.fullness, observedOmnivore.x, observedOmnivore.y, observedOmnivore.orgID, observedOmnivore.organismRange, observedOmnivore.deadUntil, observedOmnivore.deadFor, "Omnivore");
            else if (observedPredatory != null)
                refreshOrgShowingInfo(observedPredatory.isAlive, observedPredatory.male, observedPredatory.wantReproduce, observedPredatory.wantFood, observedPredatory.fullness, observedPredatory.x, observedPredatory.y, observedPredatory.orgID, observedPredatory.organismRange, observedPredatory.deadUntil, observedPredatory.deadFor, "Predatory");

            refreshInfoAboutDay();
            pictureBox1.Refresh();
        }


    }
}
