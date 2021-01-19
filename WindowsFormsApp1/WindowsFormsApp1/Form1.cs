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
                return (Brushes.Goldenrod, Brushes.Gold, Brushes.Gray);
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

        private Brush choosePlantColor(Plant plant)
        {
            if (plant is Apple)
                return Brushes.Green;
            else if (plant is Carrot)
                return Brushes.Red;
            else if (plant is Oat)
                return Brushes.DarkOliveGreen;
            return Brushes.Purple;
        }
        private void paintOrg(int x, int y, bool isAlive, Sex sex, Brush ifMale, Brush ifFemale, Brush ifDead)
        {
            if (isAlive)
            {
                if (sex == Sex.Male)
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
                paintOrg(observedOrganism.X, observedOrganism.Y, observedOrganism.GetIsAlive(), observedOrganism.GetSex(), orgColors.Item1, orgColors.Item2, orgColors.Item3);
            }
        }

        private void refreshOrgShowingInfo(bool alive, Sex sex, bool wantReproduce, bool wantFood, int fullness, int x, int y, int ID, int visionRange, int beforeBecomingPlant, string animalType)
        {
            sexLabel.Text = $"{((sex == Sex.Male) ? "Male" : "Female") }";
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
            paintObservedOrg((x,y));

        }


        private void getInfoAboutObservedOrganism((int, int) XY)
        {
            // no need for old ones
            clearOldObservedOrganism();
            observedOrganism = runner.TryToGetOrganism(XY);
            if (observedOrganism != null)
            {
                refreshOrgShowingInfo(observedOrganism.GetIsAlive(), observedOrganism.GetSex(), observedOrganism.GetReproduceWish(), observedOrganism.GetFoodEatingWish(), observedOrganism.GetFullness(), observedOrganism.X, observedOrganism.Y, observedOrganism.ID, observedOrganism.GetOrganismRange(), observedOrganism.GetBeforeBecomingPlant(), observedOrganism.GetOrganismType());
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

            if (map.mainSentry.dayNightSentry.Day)
                graphics.Clear(Color.Cornsilk);
            else
                graphics.Clear(Color.DarkSlateBlue);

            // show shards

            foreach (var meteorite in map.mainSentry.meteoriteSentry.Meteorites) {
                foreach (var shard in meteorite.MeteoriteShards)
                {
                    if (!shard.meteorite.HasFallen)
                    {
                        //graphics.DrawImage(ImageIcon.GetIcon("Shard"),
                        //new RectangleF
                        //(
                        //            shard.X * (int)resolutionUpDown.Value,
                        //            shard.Y * (int)resolutionUpDown.Value,
                        //            (int)resolutionUpDown.Value,
                        //            (int)resolutionUpDown.Value)
                        //);
                        graphics.FillRectangle(Brushes.Black, shard.X * (int)resolutionUpDown.Value, shard.Y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    }
                    else if (shard.meteorite.HasFallen && !shard.meteorite.BecameCold)
                    {
                        graphics.DrawImage(ImageIcon.GetIcon("Shard"),
                        new RectangleF
                        (
                              shard.X * (int)resolutionUpDown.Value,
                              shard.Y * (int)resolutionUpDown.Value,
                              (int)resolutionUpDown.Value,
                              (int)resolutionUpDown.Value)
                        );

                        //graphics.FillRectangle(Brushes.Orange, shard.X * (int)resolutionUpDown.Value, shard.Y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    }
                    else if (shard.meteorite.BecameCold)
                    {
                        graphics.DrawImage(ImageIcon.GetIcon("Shard"),
                        new RectangleF
                        (
                              shard.X * (int)resolutionUpDown.Value,
                              shard.Y * (int)resolutionUpDown.Value,
                              (int)resolutionUpDown.Value,
                              (int)resolutionUpDown.Value)
                        );
                        //graphics.FillRectangle(Brushes.DarkCyan, shard.X * (int)resolutionUpDown.Value, shard.Y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                    }
                }
            }

            // show houses
            foreach (var house in map.mainSentry.houseSentry.Houses)
            {
                foreach (var housePart in house.HouseParts)
                {
                    graphics.DrawImage(ImageIcon.GetIcon("HousePart"),
                    new RectangleF
                    (
                          housePart.X * (int)resolutionUpDown.Value,
                          housePart.Y * (int)resolutionUpDown.Value,
                          (int)resolutionUpDown.Value,
                          (int)resolutionUpDown.Value)
                    );
                    //graphics.FillRectangle(Brushes.Brown, housePart.X * (int)resolutionUpDown.Value, housePart.Y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                }
            }


            // show barns
            foreach (var barn in map.mainSentry.barnSentry.Barns)
            {
                foreach (var barnPart in barn.BarnParts)
                {
                    graphics.DrawImage(ImageIcon.GetIcon("BarnPart"),
                    new RectangleF
                    (
                          barnPart.X * (int)resolutionUpDown.Value,
                          barnPart.Y * (int)resolutionUpDown.Value,
                          (int)resolutionUpDown.Value,
                          (int)resolutionUpDown.Value)
                    );
                    //graphics.FillRectangle(Brushes.Black, barnPart.X * (int)resolutionUpDown.Value, barnPart.Y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);
                }
            }

            // show plants
            foreach (var plant in map.mainSentry.plantSentry.Plants)
                graphics.DrawImage(ImageIcon.GetIcon(plant.GetType().Name),
                new RectangleF
                (
                      plant.X * (int)resolutionUpDown.Value,
                      plant.Y * (int)resolutionUpDown.Value,
                      (int)resolutionUpDown.Value,
                      (int)resolutionUpDown.Value)
                );
            //graphics.FillRectangle(choosePlantColor(plant), plant.X * (int)resolutionUpDown.Value, plant.Y * (int)resolutionUpDown.Value, (int)resolutionUpDown.Value, (int)resolutionUpDown.Value);



            // show orgs
            foreach (var organism in map.mainSentry.organismSentry.Organisms)
                paintOrg(organism.X, organism.Y, organism.GetIsAlive(), organism.GetSex(), chooseOrganismColor(organism).Item1, chooseOrganismColor(organism).Item2, chooseOrganismColor(organism).Item3);
            

            if (observedOrganism != null)
                refreshOrgShowingInfo(observedOrganism.GetIsAlive(), observedOrganism.GetSex(), observedOrganism.GetReproduceWish(), observedOrganism.GetFoodEatingWish(), observedOrganism.GetFullness(), observedOrganism.X, observedOrganism.Y, observedOrganism.ID, observedOrganism.GetOrganismRange(), observedOrganism.GetBeforeBecomingPlant(), observedOrganism.GetOrganismType());


            refreshInfoAboutDay();
            pictureBox1.Refresh();
        }


    }
}
