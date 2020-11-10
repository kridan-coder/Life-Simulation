using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    abstract public class Entity
    {

        public int x;
        public int y;
        public Map map;
        public Entity(int _x, int _y, Map _map)
        {
            x = _x;
            y = _y;
            map = _map;
        }

        public Direction randomDirection4()
        {
            Array values = Enum.GetValues(typeof(Direction));
            return (Direction)values.GetValue(map.random.Next(values.Length - 5));
        }

        // 9 actually. None is also an option (will stay)
        public Direction randomDirection8()
        {
            Array values = Enum.GetValues(typeof(Direction));
            return (Direction)values.GetValue(map.random.Next(values.Length));
        }
    }


}
