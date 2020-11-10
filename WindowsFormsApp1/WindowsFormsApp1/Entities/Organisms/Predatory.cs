using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Predatory : Organism
    {
        public Predatory(int _x, int _y, bool _male, int _range, int _rollBack, int _deadUntil, Map _map) : base(_x, _y, _male, _range, _rollBack, _deadUntil, _map)
        {
        }
    }
}
