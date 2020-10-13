using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace WindowsFormsApp1
{
    public class Organism : Entity
    {

        public Organism(int _x, int _y) : base(_x, _y)
        {
        }


        public int fullness = 100;
        public bool wantFood = false;
        public bool foundFood = false;


        public void MakeMove()
        {
            if (fullness <= 0)
            {
                is_alive = false;
            }
            else if (fullness < 50)
            {
                wantFood = true;
                fullness--;
            }
            else
            {
                fullness--;
            }
        }

        public void EatPlant()
        {
            fullness = 100;
            wantFood = false;
            foundFood = false;
        }
    }
}
