using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Oat : Plant
    {
        public Oat(int _x, int _y, Random _random) : base(_x, _y, _random)
        {
        }

        public override Plant SetPlant(int x, int y)
        {
            return new Oat(x, y, random);
        }
    }
}
