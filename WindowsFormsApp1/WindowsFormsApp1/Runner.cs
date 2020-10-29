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
        private const int organisms = 333;
        private const int minOrgRange = 50;
        private const int orgRollBackReproduce = 150;
        private const int orgDeadBeforeBecomingGrass = 100;

        private const int plants = 60;
        private const int plantsGrowth = 5;

        private const int rows = 1000;
        private const int cols = 1000;

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
            map = new Map(organisms, plants, plantsGrowth, rows, cols, dayNightChange, minOrgRange, orgRollBackReproduce, orgDeadBeforeBecomingGrass);
            form1.InitGraphics();
            map.CreateWorld();
            form1.DrawCanvas(map.plants, map.organisms, map.day);
            form1.StartTimer();
        }
        public void NextTick()
        {
            map.UpdateWorld();
            form1.DrawCanvas(map.plants, map.organisms, map.day);
        }
        public Organism GetOrganism(int x, int y)
        {
            return map.GetOrganismOnCell(x, y);
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
