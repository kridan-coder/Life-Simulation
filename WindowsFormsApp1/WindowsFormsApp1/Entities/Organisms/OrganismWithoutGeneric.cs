using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public abstract class Organism : Entity
    {
        public Organism(int _x, int _y) : base(_x, _y)
        {

        }
        public abstract void NextMove();
    }
}
