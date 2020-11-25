using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    abstract public class Entity
    {
        private static int lastID;
        public int X;
        public int Y;
        public int ID;
        public Entity(int _x, int _y)
        {
            ID = lastID++;
            X = _x;
            Y = _y;
        }

        public static Direction RandomDirection4(Random random)
        {
            Array values = Enum.GetValues(typeof(Direction));
            return (Direction)values.GetValue(random.Next(values.Length - 5));
        }

        // 9 actually. None is also an option (will stay)
        public static Direction RandomDirection8(Random random)
        {
            Array values = Enum.GetValues(typeof(Direction));
            return (Direction)values.GetValue(random.Next(values.Length));
        }
    }


}
