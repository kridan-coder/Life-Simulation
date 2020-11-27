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

        private (Brush, Brush, Brush) chooseOrganismColor(Organism organism)
        {
            if (organism is Human)
            {
                return (Brushes.Yellow, Brushes.Yellow, Brushes.Yellow);
            }
            else if (organism is Bear)
            {
                return (Brushes.Brown, Brushes.Brown, Brushes.Brown);
            }
            else if (organism is Pig)
            {
                return (Brushes.Pink, Brushes.Pink, Brushes.Pink);
            }
            else if (organism is Raccoon)
            {
                return (Brushes.Gray, Brushes.Gray, Brushes.Gray);
            }
            else if (organism is Deer)
            {
                return (Brushes.RosyBrown, Brushes.RosyBrown, Brushes.RosyBrown);
            }
            else if (organism is Mouse)
            {
                return (Brushes.White, Brushes.White, Brushes.White);
            }
            else if (organism is Rabbit)
            {
                return (Brushes.FloralWhite, Brushes.FloralWhite, Brushes.FloralWhite);
            }
            else if (organism is Fox)
            {
                return (Brushes.Orange, Brushes.Orange, Brushes.Orange);
            }
            else if (organism is Lion)
            {
                return (Brushes.LightYellow, Brushes.LightYellow, Brushes.LightYellow);
            }
            else if (organism is Wolf)
            {
                return (Brushes.LightSlateGray, Brushes.LightSlateGray, Brushes.LightSlateGray);
            }
            return (Brushes.Blue, Brushes.Blue, Brushes.Blue);
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
        private void paintObservedOrg((int, int) XY)
        {
            graphics.FillEllipse(Brushes.DarkOrange, XY.Item1 * (int)resolutionUpDown.Value, XY.Item2 * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
            pictureBox1.Refresh();
        }

        private void clearOldObservedOrganism()
        {
            if (observedOrganism != null)
            {
                (Brush, Brush, Brush) orgColors = chooseOrganismColor(observedOrganism);
                paintOrg(observedOrganism.X, observedOrganism.Y, observedOrganism.GetIsAlive(), observedOrganism.GetGender(), orgColors.Item1, orgColors.Item2, orgColors.Item3);
            }
        }

        private void refreshOrgShowingInfo(bool alive, bool male, bool wantReproduce, bool wantFood, int fullness, int x, int y, int ID, int visionRange, int beforeBecomingPlant, string animalType)
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
                labelInfoBecomeGrass.Text = "Become plant in: ";
                labelBecomeGrass.Text = $"{(beforeBecomingPlant).ToString()}";
            }
            paintObservedOrg(x,y);

        }


        private void getInfoAboutObservedOrganism((int, int) XY)
        {
            // no need for old ones
            clearOldObservedOrganism();
            observedOrganism = runner.TryToGetOrganism(XY);
            if (observedOrganism != null)
            {
                refreshOrgShowingInfo(observedOrganism.GetIsAlive(), observedOrganism.GetGender(), observedHerbivore.wantReproduce, observedHerbivore.wantFood, observedHerbivore.fullness, observedOrganism.X, observedOrganism.Y, observedOrganism.ID, observedHerbivore.organismRange, observedHerbivore.beforeBecomingPlant, "Herbivore");
                paintObservedOrg(XY);
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
                (int, int) XY;
                XY.Item1 = e.Location.X / resolution;
                XY.Item2 = e.Location.Y / resolution;
                getInfoAboutObservedOrganism(XY);
            }
        }
        public void InitGraphics()
        {
            resolution = (int)resolutionUpDown.Value;
            pictureBox1.Image = new Bitmap(runner.Cols() * (int)resolutionUpDown.Value, runner.Rows() * (int)resolutionUpDown.Value);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }
        public void DrawCanvas(Map map)
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
