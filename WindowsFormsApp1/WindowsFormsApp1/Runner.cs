using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Runner
    {
        private const int resolution = 8;
        private const int organisms = 333;
        private const int organismsRange = 50;
        private const int plants = 60;
        private const int plantsGrowth = 5;


        Map map;
        Form1 form1;

        public Runner(Form1 form)
        {
            form1 = form;
            map = new Map(resolution, organisms, plants, plantsGrowth, organismsRange, ref form1);
        }


        public void FirstTick()
        {
            form1.InitGraphics();
            map.CreateWorld();
            form1.DrawCanvas(map.plants, map.organisms, resolution);
            form1.StartTimer();
        }

        public void NextTick()
        {
            map.UpdateWorld();
            form1.DrawCanvas(map.plants, map.organisms, resolution);
        }
    }
}
