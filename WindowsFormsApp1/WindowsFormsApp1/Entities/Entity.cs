using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    abstract public class Entity
    {
        public Entity(int _x, int _y)
        {
            x = _x;
            y = _y;
            is_alive = true;
        }

        public bool is_alive;
        public int x;
        public int y;

    }


}
