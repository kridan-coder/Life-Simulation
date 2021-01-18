using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Carrot : Plant,
         EdibleForHuman, EdibleForDeer, EdibleForMouse, EdibleForRabbit, EdibleForPig, EdibleForRaccoon, SuitableForGathering
    {
        public Carrot(int _x, int _y, PlantSentry plantSentry) : base(_x, _y, plantSentry)
        {
        }
    }
}
