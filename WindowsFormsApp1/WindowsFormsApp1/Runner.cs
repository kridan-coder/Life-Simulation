using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Runner
    {
        // organisms set
        private const int humans = 100; private const int ticksHumanStutter = 0;
        private const int deers = 50; private const int ticksDeerStutter = 2;
        private const int mice = 100; private const int ticksMouseStutter = 0;
        private const int rabbits = 40; private const int ticksRabbitStutter = 1;
        private const int bears = 40; private const int ticksBearStutter = 2;
        private const int pigs = 30; private const int ticksPigStutter = 1;
        private const int raccoons = 70; private const int ticksRaccoonStutter = 1;
        private const int foxes = 20; private const int ticksFoxStutter = 1;
        private const int lions = 90; private const int ticksLionStutter = 1;
        private const int wolves = 100; private const int ticksWolfStutter = 1;

        // organisms options
        private const int maxOrgVisionRange = 50;
        private const int maxOrgTicksBeforeReproducing = 100;
        private const int maxOrgTicksBeforeBecomingGrass = 200;

        // plants set
        private const int apples = 20; private const int applesGrowth = 20;
        private const int carrots = 50; private const int carrotsGrowth = 15;
        private const int oats = 150; private const int oatsGrowth = 5;

        // meteorite
        private const int chanceOfMeteoriteToFallOnMap = 5; // out of 100
        private const int chanceOfHumanToSpawnOnShard = 3; // out of 100
        private const int chanceOfPlantToSpawnOnShard = 3; // out of 100
        // chanceOfNothingToSpawnOnShard = 100 - chanceOfHumanToSpawnOnShard - chanceOfPlantToSpawnOnShard
        private const int maxAmountOfMeteoritesFallingSimultaneously = 3;
        private const int maxTicksMeteoriteFalling = 15;
        private const int maxTicksMeteoriteCracking = 10;
        private const int maxTicksMeteoriteBeforeDissolving = 15;

        private const int rows = 1000;
        private const int cols = 1000;

        private const int dayNightChange = 300;

        Map map;
        Form1 form1;

        public Runner(Form1 form)
        {
            form1 = form;
        }

        public int Rows()
        {
            return rows;
        }
        public int Cols()
        {
            return cols;
        }
        public void FirstTick()
        {
            map = new Map(rows, cols, apples, applesGrowth, carrots, carrotsGrowth, oats, oatsGrowth, humans, ticksHumanStutter, deers, ticksDeerStutter, mice, ticksMouseStutter, rabbits, ticksRabbitStutter, bears, ticksBearStutter, pigs, ticksPigStutter, raccoons, ticksRaccoonStutter, foxes, ticksFoxStutter, lions, ticksLionStutter, wolves, ticksWolfStutter, maxOrgVisionRange, maxOrgTicksBeforeReproducing, maxOrgTicksBeforeBecomingGrass, dayNightChange, maxAmountOfMeteoritesFallingSimultaneously, maxTicksMeteoriteFalling, maxTicksMeteoriteCracking, maxTicksMeteoriteBeforeDissolving, chanceOfMeteoriteToFallOnMap, chanceOfHumanToSpawnOnShard, chanceOfPlantToSpawnOnShard);
            form1.InitGraphics();
            map.CreateWorld();
            form1.DrawCanvas(map);
            form1.StartTimer();
        }
        public void NextTick()
        {
            map.UpdateWorld();
            form1.DrawCanvas(map);
        }

        public Organism TryToGetOrganism((int, int) XY)
        {
            return map.GetOrganismOnCell(XY);
        }

        public bool IsItDayToday()
        {
            return map.mainSentry.dayNightSentry.Day;
        }
        public int HowMuchUntilChange()
        {
            return (map.mainSentry.dayNightSentry.UntilDayOrNight - map.mainSentry.dayNightSentry.DayOrNightLastsFor);
        }

    }
}
