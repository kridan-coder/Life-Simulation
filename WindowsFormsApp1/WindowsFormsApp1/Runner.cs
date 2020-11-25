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
        // organisms
        private const int herbivores = 30;
        private const int predators = 30;

        // meteorite
        private const int chanceOfMeteoriteToFallOnMap = 100; // out of 100
        private const int chanceOfHumanToSpawnOnShard = 1; // out of 100
        private const int chanceOfPlantToSpawnOnShard = 0; // out of 100
        // chanceOfNothingToSpawnOnShard = 100 - chanceOfHumanToSpawnOnShard - chanceOfPlantToSpawnOnShard
        private const int howManyTicksFall = 5;
        private const int howManyTicksShards = 2;
        private const int howManyTicksBeforeDissolving = 15;

        private const int minOrgRange = 25;
        private const int orgRollBackReproduce = 150;
        private const int orgDeadBeforeBecomingGrass = 100;

        private const int plants = 100;
        private const int plantsGrowth = 0;

        private const int rows = 100;
        private const int cols = 100;

        private const int dayNightChange = 50;


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
            map = new Map(herbivores, predators, plants, plantsGrowth, rows, cols, dayNightChange, minOrgRange, orgRollBackReproduce, orgDeadBeforeBecomingGrass, chanceOfMeteoriteToFallOnMap, chanceOfHumanToSpawnOnShard, chanceOfPlantToSpawnOnShard, howManyTicksFall, howManyTicksShards, howManyTicksBeforeDissolving);
            form1.InitGraphics();
            map.CreateWorld();
            form1.DrawCanvas(map.plants, map.herbivores, map.predators, map.omnivores, (map.meteorite != null) ? map.meteorite.meteoriteShards : null, map.day);
            form1.StartTimer();
        }
        public void NextTick()
        {
            map.UpdateWorld();
            form1.DrawCanvas(map.plants, map.herbivores, map.predators, map.omnivores, (map.meteorite != null) ? map.meteorite.meteoriteShards : null, map.day);
        }
        public Herbivore TryToGetHerbivore(int x, int y)
        {
            return map.GetHerbivoreOnCell(x, y);
        }
        public Omnivore TryToGetOmnivore(int x, int y)
        {
            return map.GetOmnivoreOnCell(x, y);
        }
        public Predatory TryToGetPredatory(int x, int y)
        {
            return map.GetPredatoryOnCell(x, y);
        }
        public bool IsItDayToday()
        {
            return map.day;
        }
        public int HowMuchUntilChange()
        {
            return (map.untilDayOrNight - map.dayOrNightLastsFor);
        }

    }
}
