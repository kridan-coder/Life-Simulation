using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Carrot : Plant
    {
        public Carrot(int _x, int _y, Random _random) : base(_x, _y, _random)
        {
        }

        public override Plant SetPlant(int x, int y)
        {
            return new Carrot(x, y, random);
        }
    }
}
